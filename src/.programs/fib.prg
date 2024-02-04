// Program calculates consecutive (0th to 9th supproted) 
// Calculates 0th to 9th Fibonacci number given in character input (Default IO address: 1)
// and puts result into memory address of "result" variable (should be at address ). 
// Note: Requires "\src\.instructions\IO.lst" instruction set loaded or [IORD.rzk]


fib:
	LOAD one
	STOR next		// initialize fib next num with 1 

convert:
	IORD 1			// read single ASCII character from input
	SUB ascii		// convert to number - will be our loop indexe
	BEZ end  		// if 0 end
	STOR i			// store as iterator
					// now flow falls into loop
loop:	
	LOAD next
	ADD prev
	STOR temp		// m = f + f'
	LOAD next
	STOR prev		// f' = f
	LOAD temp
	STOR next		// f = m
	
	LOAD i
	SUB one
	BEZ end
	STOR i
	JMP loop
	
end:
	STP

// _______________________
// Defines and allocations 

one: DEF 1
zero: DEF 0
ascii: DEF '0' // char ASCII 48

prev: RES  	// f'
next: RES	// f
temp: RES	// m

i: RES		// loop iter
