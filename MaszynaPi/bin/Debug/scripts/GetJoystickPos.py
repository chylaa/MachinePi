import sys
from sense_hat import SenseHat
sense = SenseHat()
for e in sense.stick.get_events():       
    if e.action!="released":
        print(str(e.direction))
        sys.stdout.flush()