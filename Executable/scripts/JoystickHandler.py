import sys
from sense_hat import SenseHat
sense = SenseHat()
while True:
    for e in sense.stick.get_events():       
        if e.action=="pressed":
            print(str(e.direction))
            sys.stdout.flush()