JMP main
JMP down
JMP up
JMP right
JMP left

left:
LOAD l
JMP print

right:
LOAD r
JMP print

up:
LOAD u
JMP print

down:
LOAD d
JMP print

print:
ADD white
IOWR 6
JMP main

main:
nop
jmp main


u: DEF 'u'
d: DEF 'd'
l: DEF 'l'
r: DEF 'r'
white: DEF 1024


