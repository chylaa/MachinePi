'''
    Script communicates with connected to RespberryPI SenseHat device using provided by producent sense_hat package.
    In endless loop, it outputs, into standard output, string indicating direction of joysitck press. 

    Usage: JoysitckHanlder.py 
'''

import sys
from sense_hat import SenseHat
sense = SenseHat()
while True:
    for e in sense.stick.get_events():       
        if e.action=="pressed":
            print(str(e.direction))
            sys.stdout.flush()