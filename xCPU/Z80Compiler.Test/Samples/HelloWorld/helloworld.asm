;test display 16x2 "hello world!"

lcd_control_A equ 0x00 ; 0000 0000
lcd_write_A   equ 0x08 ; 0000 1000

org 0x00
call main

org 0x10
label1
    defm "hello world!."

org 0x30
main
    ld a, 0x01       ;clear
    out (lcd_control_A), a
    ld a, 0x0f       ;on
    out (lcd_control_A), a

    ld hl, label1

    jp compare

next
    inc hl

compare
    ld a, 0x2e          ; dot "."
    cp (hl)             ; compare hl == a.
    jp z, main

send_message
    ld b, (hl)
    ld a, b'
    out (lcd_write_A), a
    
    jp next
