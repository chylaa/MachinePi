JMP main
JMP down  // INT 1
JMP up    // INT 2
JMP right // INT 3
JMP left  // INT 4

left: LOAD l
JMP print

right: LOAD r
JMP print

up: LOAD u
JMP print

down: LOAD d
JMP print

print: ADD red  // set color bits to 0b100 (red)
IOWR 6          // write accumulator value to matrix

main: nop
jmp main

u: DEF 'u'
d: DEF 'd'
l: DEF 'l'
r: DEF 'r'
red: DEF 1024


