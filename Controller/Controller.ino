const int REGISTER_PIN = A0;
const int SWITCH_PIN = 7;

void setup()
{
    pinMode(REGISTER_PIN, INPUT);
    pinMode(SWITCH_PIN, INPUT);

    Serial.begin(9600);
}

void loop()
{    
    char str[8] = { 0 };
    bool switchValue = digitalRead(SWITCH_PIN);
    int registerValue = analogRead(REGISTER_PIN);

    sprintf(str, "%d,%d", switchValue, registerValue);

    Serial.println(str);

    /*
    Serial.print("switch:");
    Serial.print(switchValue ? "1" : "0");
    Serial.print(", register:");
    Serial.println(registerValue);
    */
}
