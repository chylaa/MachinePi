loop:
	LOAD a
	ADD one
	STOR a
	SUB max
	BLZ loop
STP

a: RES
one: DEF 1
max: DEF 16