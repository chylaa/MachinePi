'''
    Script communicates with connected to RespberryPI SenseHat device using provided by producent sense_hat package.
    It allows to display characters into SenseHat's matrix ('Letter' mode) or turn ON/OFF specific pixels ('Pixel' mode)
    using values provided in script's argumets.

    Usage: MatrixHandler.py [value] [mode]
'''


#Paint mode
RGB_BACK          = [0,0,0]
MATRIX_ADDR_MASK  = 0b111111
MATRIX_COLUMS     = 8
MATRIX_ROWS       = 8  

RGB_FRONT_MASK    = 0b11100000000
ASCII_BYTES       = 8
BYTE_MAX          = 255

def get_rgb_front(val:int)->list:
    rgb = value & RGB_FRONT_MASK 
    rgb = (rgb >> ASCII_BYTES)
    #if rgb == 0: rgb = 7 #prevent no backlight if colors not set
    rgb = [(rgb & (1<<2))>>2, (rgb&(1<<1))>>1, (rgb&(1<<0))>>0]
    return [i * BYTE_MAX for i in rgb]

def update_matrix(value:int, colour:list, matrix:list)->list:
    pixel_addr = value & MATRIX_ADDR_MASK
    matrix[pixel_addr] = colour
    return matrix
    
import sys
from sense_hat import SenseHat
sense = SenseHat()
value = int(sys.argv[1])
mode = sys.argv[2]
if mode == 'Letter':
    sense.show_letter(str(chr(value&BYTE_MAX)), text_colour=get_rgb_front(value))
elif mode == 'Paint':
    colour = get_rgb_front(value)
    matrix = update_matrix(value, colour, sense.get_pixels())
    sense.set_pixels(matrix)
else: 
    pass
