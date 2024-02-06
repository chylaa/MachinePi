JMP main
JMP down  // INT 1
JMP up    // INT 2
JMP loop  // INT 3
JMP loop  // INT 4


up: ADD one
MAS 15  //mask with immediate addressing
STOR letter 
SUB end //check if greater 
BGZ main
LOAD letter
JMP print

down: SUB one
MAS 15
STOR letter
SUB begin
BLZ 
LOAD letter
MAS 0
JMP print

overflow:
LOAD end 
MAS 0
JMP print


main:
	MAS 0
	LOAD begin
	STOR letter
print: 
	ADD white  // set color bits to 0b111 (white)
	IOWR 6     // write accumulator value to matrix
loop:
	NOP
JMP loop


white: DEF 1792
one:   DEF 1
begin: DEF 65 //'A'
end:   DEF 90 //'Z'


letter: RES