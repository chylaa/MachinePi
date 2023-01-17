JMP init
JMP down  // INT 1
JMP up    // INT 2
JMP right // INT 3
JMP left  // INT 4

init:
LOAD start
STOR pixel
JMP print

left: 
LOAD pixel
IOWR 6
SUB column
JMP print

right:
LOAD pixel
IOWR 6
ADD column
JMP print

up:
LOAD pixel
IOWR 6
SUB row
JMP print

down: 
LOAD pixel
IOWR 6
ADD row

print: STOR pixel 
ADD blue  // set color bits to 0b001 (blue)
IOWR 6          // write accumulator value to matrix

main:
nop
jmp main

row: DEF 8
column: DEF 1 
blue: DEF 256
start: DEF 31

pixel: RES
