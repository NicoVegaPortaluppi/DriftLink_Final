#include <Arduino.h>
#include <Keypad.h>

const int tiltPin = 2;
const int touchPin = 4;
const int joystickVRX = A0;
const int joystickVRY = A1;
const int joystickSW = 13;


const byte ROWS = 4;
const byte COLS = 3;

char keys[ROWS][COLS] = {
  {'1','2','3'},
  {'4','5','6'},
  {'7','8','9'},
  {'*','0','#'}
};

byte rowPins[ROWS] = {9, 8, 7, 6};     
byte colPins[COLS] = {5, 3, 10};       

Keypad keypad = Keypad(makeKeymap(keys), rowPins, colPins, ROWS, COLS);


unsigned long lastPing = 0;

void setup() {
  Serial.begin(9600);

  pinMode(tiltPin, INPUT);
  pinMode(touchPin, INPUT);
  pinMode(joystickSW, INPUT_PULLUP);
}

void loop() {

  int tiltState = digitalRead(tiltPin);
  int touchState = digitalRead(touchPin);
  int joystickX = analogRead(joystickVRX);
  int joystickY = analogRead(joystickVRY);
  int joystickButton = digitalRead(joystickSW);

  char key = keypad.getKey();
  unsigned long now = millis();

  Serial.print(tiltState);
  Serial.print(",");
  Serial.print(touchState);
  Serial.print(",");
  Serial.print(joystickX);
  Serial.print(",");
  Serial.print(joystickY);
  Serial.print(",");
  Serial.println(joystickButton);

  if (key) {
    Serial.print(tiltState);
    Serial.print(",");
    Serial.print(touchState);
    Serial.print(",");
    Serial.print(joystickX);
    Serial.print(",");
    Serial.print(joystickY);
    Serial.print(",");
    Serial.print(joystickButton);
    Serial.print(",");
    Serial.println(key);
  }

  if (now - lastPing >= 1000) {
    lastPing = now;
    Serial.print("PING,");
    Serial.println(now);
  }

  delay(20); 
}


