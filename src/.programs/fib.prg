// Calculates 0th to 10th Fibonacci number - selected by given N from character input (Default IO address: 1).
// Program puts result of Nth number into memory address of "prev" variable (should be at address 22),
// and result of (N+1) at "next" (addr. 23). 
// Note: Requires "\src\.instructions\IO.lst" instruction set loaded or at least working IORD.rzk

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
	SUB one			// iterator decrementation
	BEZ end			// loop condition check
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
