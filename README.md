# Overview 
The objective of this project was to develop an engaging racing simulation that utilizes a custom physical input interface and is deployable via a standard web browser. This approach demands a robust and low-latency solution for real-time data
transfer across multiple platforms.
The game utilizes the established physics and rendering
of the Unity Racing Starter Kit. The core innovation lies
in the architecture of the system, which maps the physical
driving actions–steering, accelerating, and braking–to bespoke
electronic components. Unlike traditional wired systems, this
project employs a server-client architecture using a Raspberry Pi and Web-socket communication to deliver the controls to a Unity WebGL instance running in a browser.

# Built with
- C#
- Arduino 
- Raspyberry Pi
- HTML

# Controller 
### Wiring Diagram
This layer captures user input and converts it into a digital
format.
- Hardware Controller (Player Input): Comprises the
Joystick, Tilt Switch, and Keypad. These components
were sourced from the standard sensor kit configuration.

- Arduino UNO: The Arduino UNO acts as the primary
micro-controller, interfacing directly with the physical
components. It reads the analog and digital sensor data,
aggregates it, and transmits them serially.

  <img src="images/ControllerWiringDiagram.png" width="70%">

### Set Up
- Here can be seen the final setup, once the controller is connected to the Raspeberry Pi.
  <img src="images/ControllerSetUp.png" width="70%">

# Contributors
- [Alejandro Adorno](https://github.com/vawms): Designed and programmed the hardware controller. 
