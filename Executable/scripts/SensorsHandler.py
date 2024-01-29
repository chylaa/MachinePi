import sys
from sense_hat import SenseHat
sense = SenseHat()
sensor = sys.argv[1]
value = ''
if sensor == 'temperature': value = sense.get_temperature()
elif sensor == 'pressure': value = sense.get_pressure()
elif sensor == 'humidity': value = sense.get_humidity()
else: value = ('Invalid argument: '+sensor)
print(str(value))
sys.stdout.flush()