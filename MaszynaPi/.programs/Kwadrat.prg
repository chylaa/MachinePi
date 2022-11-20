sprL: WPR 1 //Sprawdz czy liczba (sprL) <start>  
ODE stala48

SOM sprL
£AD liczba   //<--W tym miejscu bo potrzebujemy liczbe a nie jej ascii
ODE stala10

SOM wyp
SOB sprL        // sprL <end>

wyp: POB liczba
£AD osX           //<--Ustalenie zmiennych wielkoœci kwadratu
£AD osY

nowaGwiazdka: SOZ nowaLinia //Dodanie nowej gwiazdki w danym wierszu <start>
ODE stala1
£AD osX

POB gwiazdka
WYP 2
POB osX
SOB nowaGwiazdka // <stop>

nowaLinia: POB osY //Zejscie do nowej linii i organizacja zmiennych <start>
ODE stala1
£AD osY    
SOZ koniec

POB stala10
WYP 2

POB liczba
£AD osX

SOB nowaGwiazdka  // <stop>
 

koniec: STP

liczba: RPA
osX: RPA
osY: RPA

gwiazdka: RST 42
stala48: RST 48
stala10: RST 10
stala1:  RST 1