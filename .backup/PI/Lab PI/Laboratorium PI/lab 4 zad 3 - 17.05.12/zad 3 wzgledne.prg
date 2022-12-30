                SOB Start
                SOB PR1
                SOB PR2
                SOB PR3
                SOB PR4              
              
Start:          POB ST0
                £AD L1
                £AD L2
                £AD L3
                £AD L4
                SOB Kwadracik                
                

Kwadracik:      POB ST10
                £AD X
                £AD Y

Wiersz:         POB X
                ODE ST1
                £AD X
                POB Znak
                WYP 2
                POB X
                SOZ Kolumna
                SOB Wiersz
                
Kolumna:        POB ST10
                WYP 2
                POB Y
                ODE ST1
                £AD Y
                SOM Koniec
                POB ST10
                £AD X
                SOB Wiersz         

PR1:            CZM Maska
                DNS
                MSK Maska1
                POB L1
                DOD ST1
                £AD L1
                POB M1
                WYP 2
                PZS
                MSK Maska
                PWR 

PR2:            CZM Maska
                DNS
                MSK Maska1
                POB L2
                DOD ST1
                £AD L2
                POB M2
                WYP 2
                PZS
                MSK Maska
                PWR 

PR3:            CZM Maska
                DNS
                MSK Maska1
                POB L3
                DOD ST1
                £AD L3
                POB M3
                WYP 2
                PZS
                MSK Maska
                PWR  

PR4:            CZM Maska
                DNS
                MSK Maska1
                POB L4
                DOD ST1
                £AD L4
                POB M4
                WYP 2
                PZS
                MSK Maska
                PWR              
                
Koniec:         POB L1
                DOD ST48
                WYP 2
                POB L2
                DOD ST48
                WYP 2
                POB L3
                DOD ST48
                WYP 2
                POB L4
                DOD ST48
                WYP 2                 
                STP

Znak: RST 45
X: RPA
Y: RPA
ST10: RST 10
ST1: RST 1
ST0: RST 0
Maska: RPA
L1: RPA
L2: RPA
L3: RPA
L4: RPA
M1: RST 'C'
M2: RST 'c'
M3: RST '3'
M4: RST '4'
Maska1: RST 15 
ST48: RST 48