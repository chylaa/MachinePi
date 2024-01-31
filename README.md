# Implementation of the Machine W simulator on the ARM processor platform.

### Engineering degree project 2022/2023

---
The following implementation of the W Machine simulator, in addition to running under Windows, allows for optional integration of the application into the RaspberryPi microcomputer using the [Mono runtime environment](https://www.mono-project.com/docs/advanced/runtime/). 

The simulator offers the possibility of defining own instructions. Project includes predefined instruction sets, including [Stack](https://github.com/chylaa/maszynaPi/blob/main/src/.instructions/Stack.lst), [Interruptions](https://github.com/chylaa/maszynaPi/blob/main/src/.instructions/Interruptions.lst) and [Input/Output](https://github.com/chylaa/maszynaPi/blob/main/src/.instructions/IO.lst) sets, allowing extensive use of the available architecture extensions.

In addition, based on the IO instruction set, the solution allows communication with interactive
external devices, available via the [SenseHat](https://www.raspberrypi.com/products/sense-hat/) extension module for RaspberryPi - implementing the new *PI Machine* architecture.

Implemetation supports polish and english version of control signals in instruction sets, however the user interface was created only in english.

---

#### Simplified educational computer - background information on the basics of the software simulator: 
 
 - Article "*W Machine - How to design a simple instruction*" in the journal **Minut** of the Silesian University of Technology ([Polish version](https://minut.polsl.pl/articles/C-19-004.pdf 'Maszyna W - jak zaprojektować prosty rozkaz')).
