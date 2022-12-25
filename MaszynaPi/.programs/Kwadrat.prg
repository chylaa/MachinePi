sprL: WPR 1 //Sprawdz czy liczba (sprL) <start>  
ODE stala48
SOM sprL
LAD liczba   //<--W tym miejscu bo potrzebujemy liczbe a nie jej ascii
ODE stala10
SOM wyp
SOB sprL        // sprL <end>
wyp: POB liczba
LAD osX           //<--Ustalenie zmiennych wielkosci kwadratu
LAD osY
nowaGwiazdka: SOZ nowaLinia //Dodanie nowej gwiazdki w danym wierszu <start>
ODE stala1
LAD osX
POB gwiazdka
WYP 2
POB osX
SOB nowaGwiazdka // <stop>
nowaLinia: POB osY //Zejscie do nowej linii i organizacja zmiennych <start>
ODE stala1
LAD osY    
SOZ koniec
POB stala13
WYP 2
POB stala10
WYP 2
POB liczba
LAD osX
SOB nowaGwiazdka  // <stop>
 
koniec: STP
liczba: RPA
osX: RPA
osY: RPA
gwiazdka: RST 42
stala48: RST 48
stala10: RST 10
stala13: RST 13
stala1:  RST 1
