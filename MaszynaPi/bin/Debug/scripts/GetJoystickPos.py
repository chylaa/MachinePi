import sys
from time import sleep
from sense_hat import SenseHat
sense = SenseHat()
for e in sense.stick.get_events():       
    if e.action=="pressed":
        print(str(e.direction))
        sys.stdout.flush()
sleep(0.3)