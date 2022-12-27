RGB_FRONT_MASK =   0b11100000000
RGB_BACK_MASK = 0b11100000000000
ASCII_BYTES = 8
BYTE_MAX = 255

def get_rgb_front(val):
    rgb = value & RGB_FRONT_MASK 
    rgb = (rgb >> ASCII_BYTES)
    if rgb == 0: rgb = 7 #prevent no backlight if colors not set
    rgb = [(rgb & (1<<2))>>2, rgb&(1<<1)>>1, rgb&(1<<0)>>0]
    return [i * BYTE_MAX for i in rgb]
    
import sys
from sense_hat import SenseHat
sense = SenseHat()
value = int(sys.argv[1])
mode = sys.argv[2]
if mode == 'Letter':
    sense.show_letter(str(chr(value&BYTE_MAX)), text_colour=get_rgb_front(value))
elif mode == 'Paint':
    pass
else: 
    pass
