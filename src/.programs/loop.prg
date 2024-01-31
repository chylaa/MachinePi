loop:
LOAD a
ADD one
STOR a
BEZ end
JMP loop
end:
STP
a: RES
one: DEF 1
