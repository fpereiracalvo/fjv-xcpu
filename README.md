# General

Fjv.xCPU is an experimental Assembly compiler library that provides architecture and methods to customize your own cpu compiler.

# Getting started

## How to define instructions

You can define CPU and Assembly instruction and its logic using this library.

### CPU instruction definition sample

You need to select a CPU to define it. For this example we will use a Z84. This is a vintage CPU, and is perfect to teach how use this library. Even in the sources you can see a extend sample about that.

You can donwload the chip datasheet from https://pdf1.alldatasheet.com/datasheet-pdf/view/600133/ZILOG/Z84C00.html

The Z84C00 CPU has a 8 BIT LOAD GROUP. You can see its two first instructions:

* **LD r, r'** has the opcode "01 r r'" where the base value is *01000000* (*0x40* in hexadecimal). You must put the left operand (register) at third position, and the right operand (prime register) at zero position of the byte.
* **LD r, n** has the opcode "00 r 110" where the value is *00000110* (0x06 in hexadecimal). You must put the lef operand (register) at the third position again and the right operand (byte) in a new byte.

Example code:
```csharp
//removed code for brevity.

// 8 bit load group.
var mnemonic = "ld";

group.AddInstruction(new CpuInstructionType() {
    // indicate the the command mnemonic.
    Mnemonic = mnemonic,
    // set the types of operands supported.
    Operand = new Type[] { typeof(Z80Register), typeof(Z80PrimeRegister) },
    // the large in bytes of the instruction result.
    Size = new Func<IInstructionParam, int>((x) => { return 1; }),
    // custom compute instruction.
    Instruction = new Func<IInstructionParam, byte[]>((x) => {
        // ld r r' (01 rrr r'r'r')
        byte hex = 0x40;

        // take register addresses.
        var left = (byte)((Z80Register)x.LeftValue).Address;
        var right = (byte)((Z80PrimeRegister)x.RightValue).Address;

        // calculation of the byte.
        left = (byte)(left << 3);
        hex = (byte)(hex | left | right);

        // return the result.
        return new byte[1] { hex };
    })
}).AddInstruction(new CpuInstructionType() {
    Mnemonic = mnemonic,
    Operand = new Type[] { typeof(Z80Register), typeof(byte) },
    Size = new Func<IInstructionParam, int>((x) => { return 2; }),
    Instruction = new Func<IInstructionParam, byte[]>((x) => {
        // ld r, n (00 rrr 110)
        byte hex = 0x06;

        // take register and value.
        var register = (Z80Register)x.LeftValue;
        var right = (byte)x.RightValue;

        // calculation of the byte.
        var left = (byte)(register.Address << 3);
        hex = (byte)(hex | left);

        // return array of two bytes.
        return new byte[2] { hex, right };
    })
})
```
The CPU instructions result always be an array of bytes.

As you can see, we put logic into Instruction property to process the data. The operands can be customized as *Z80Register* or predefined like *byte*, *Uint16*, *string*, etc. The library resolve wich instruction must be use to process the line of assembly code and wich types match with handling them. You don't need put code to read or recognize structured text in your C# code.

In this example *Z80Register* represent a register in the Z80 CPU, and they are: a, b, c, and others. In our code, we define it something like this:
```csharp
//removed code for brevity.

public class Z80Register : RegisterType
{
    public static Z80Register B = new Z80Register()
    {
        Mnemonic = "b",
        Address = 0x00
    };

    public static Z80Register C = new Z80Register()
    {
        Mnemonic = "c",
        Address = 0x01
    };

    //removed code for brevity.
}
```

Following this pattern allow to the library recognize our custom operands like registers, prime registers, index registers, and so.

#### Assembly language definition sample

The mode to define the language is very similar with CPU definition, the difference resides in the return type that is **AssemblyDataTypeResult**, because the languaje has more functions to do.

The code below shows us how we would define the EQU command for assembly languaje.

```csharp
//equ equate
Instructions.Add(new AssemblyInstructionType()
{
    Mnemonic = "equ",
    Operand = new Type[] { typeof(UInt16) },
    Instruction = new Func<IInstructionArgument, AssemblyDataTypeResult[]>((x) => {
        var value = x.Left;

        return new AssemblyDataTypeResult[] {
            new AssemblyDataTypeResult {
                Object = Bytes.GetUint16(value),
                DataType = DataType.Information
            }
        };
    })
});
```

The definition is used to tell to the compiler service what process to do by **DataType** property and passing the data throught the **Object** property.

You must passing a type of value to Object property for each DataType:
* DataType.Data: byte[] (array of bytes). Can be code for the CPU model.
* DataType.Code: string[] (array of strings). Would be lines of assembly codes.
* DataType.Information: int (integer value). It is a information about index.

The other enum is Internal, but it is not implemeted yet inside the SinglePassCompiler.

The next sample shows how pattern by ** RegexPattern** to recognize special strings on the command. In this case take another source file from folder or a remote resource to compile and put it inside your program.

```csharp
//ext "<external or remote file>"
Instructions.Add(new AssemblyInstructionType()
{
    Mnemonic = "ext",
    RegexPattern = "\"(.*?)\"",
    Instruction = new Func<IInstructionArgument, AssemblyDataTypeResult[]>((x) => {
        var source = x.Left?.Replace("\"", "");
        var sourcepath = x.Source;

        Uri.TryCreate(source, UriKind.Absolute, out Uri uri);

        var file = System.IO.Path.Combine(sourcepath, source);

        if (System.IO.File.Exists(source))
        {
            return new AssemblyDataTypeResult[] {
                new AssemblyDataTypeResult {
                    Object = System.IO.File.ReadAllLines(source).ToArray(),
                    DataType = DataType.Code
                }
            };
        }
        else if (System.IO.File.Exists(file))
        {
            return new AssemblyDataTypeResult[] {
                new AssemblyDataTypeResult {
                    Object = System.IO.File.ReadAllLines(file).ToArray(),
                    DataType = DataType.Code
                }
            };
        }
        else if (uri.IsAbsoluteUri)
        {
            OnBeginLongProcess?.Invoke(this, $"Load {uri.AbsoluteUri}...");

            var client = new System.Net.WebClient();
            string downloadString = client.DownloadString(uri);

            var temp = System.IO.Path.GetTempFileName();
            System.IO.File.WriteAllText(temp, downloadString);

            OnEndLongProcess?.Invoke(this, $"{uri.AbsoluteUri} ok.");

            return new AssemblyDataTypeResult[] {
                new AssemblyDataTypeResult {
                    Object = System.IO.File.ReadAllLines(temp),
                    DataType = DataType.Code
                }
            };
        }
        else
        {
            throw new Exception($"{file} does not exist.");
        }
    })
});
```

You can see more about this inside Z80Assembly sample project.

## SinglePassCompiler

The **SinglePassCompiler** is a class that define a sealed single pass compiler. It is an implementation about a compiler that only passing once time throught the source code. All the labels are saved or resolved on the way.

The constructor require two parameters:
* IInstructionResolverProvider\<byte\>, for cpu instruction set.
* IInstructionResolverProvider\<AssemblyDataTypeResult\>, for assembly instruction set.

## IProvider

IProvider is an interface that offering a method to get an instance of InstructionResolverProvider class which is an abstraction layer to use with a compiler. I recomend use it inside your instruction library that correspond.

The code below shows how implement this interface.
```csharp
public class Provider : IProvider<CpuInstructionType, byte>
{
    public InstructionResolverProvider<CpuInstructionType, byte> GetInstructionResolverProvider()
    {
        return new InstructionResolverProvider<CpuInstructionType, byte>(new InstructionResolver<CpuInstructionType, byte>(new MyCpuInstructionSet()));
    }
}
```

**MyCpuInstructionSet** is the class name that you must change when implement this code snippet for your cpu instruction library.

```csharp
public class Provider : IProvider<AssemblyInstructionType, AssemblyDataTypeResult>
{
    public InstructionResolverProvider<AssemblyInstructionType, AssemblyDataTypeResult> GetInstructionResolverProvider()
    {
        return new InstructionResolverProvider<AssemblyInstructionType, AssemblyDataTypeResult> (new InstructionResolver<AssemblyInstructionType, AssemblyDataTypeResult>(new MyAssemblyInstructionSet()));
    }
}
```

**MyAssemblyInstructionSet** is the class name that you must change when implement this code snippet for you assembly instruction library.


## How to compile

Compile is a process to transform strings code to machine code. In other words, is the process to get bytes from each intruction on the code source. All intructions you define inside your CPU or Assembly libraries would be converted to byte array or other things would you like, puting values from columns left or right used as entries.

If you like get bytes from a cpu instruction, you must invoke the GetInstruction passing the the line of code as a parameter.

```csharp
IProvider<CpuInstructionType, byte> cpuCodeProvider = new Fjv.Z80.Provider();
IInstructionResolverProvider<byte> _cpuCodeResolver = cpuCodeProvider.GetInstructionResolverProvider();

//remember the z80 LD instruction defined.
var line = "ld a, 0x01";
var instruction = _cpuCodeResolver.GetInstruction(line);
```

*SinglePassCompiler* has an implementations that automatize this process to use with source files. Obviously you can create your own compiler.

You can see more on samples proyects prepared to you.

Enjoy!