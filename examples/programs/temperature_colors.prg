JMP check

printG: LOAD green
ADD toChar
STOR color
JMP print

printR: LOAD red
ADD toChar
STOR color

print: LOAD tenths
ADD color
IOWR 6
LOAD ones
ADD color
IOWR 6

main: LOAD sep1
IOWR 6
LOAD sep2
IOWR 6
check:
IORD 3
STOR temp

DIV TEN 
STOR tenths
MUL TEN
STOR ones
LOAD temp
SUB ones
STOR ones

LOAD temp
SUB limit
BLZ printG
JMP printR

red: DEF 1024
green: DEF 512
TEN: DEF 10
toChar: DEF 48
limit: DEF 23
sep1: DEF 1831//"'" white
sep2: DEF 1859//'C' white
temp: RES
tenths: RES
ones: RES
color: RES
