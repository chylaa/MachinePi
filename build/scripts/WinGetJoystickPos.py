import random
import time
try:
    time.sleep(15)
    directions = ['up', 'down', 'left', 'right']
    while True:
        print(random.choice(directions))
except Exception as e:
    print("Error: "+ str(e))
    