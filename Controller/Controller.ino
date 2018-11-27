//  可変抵抗の入力電圧ピン
const int REGISTER_PIN = A0;

//  タクトスイッチの入力電圧ピン
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

    //  コンマ区切りの文字列でシリアル通信で渡す。
    sprintf(str, "%d,%d", switchValue, registerValue);
    Serial.println(str);
}
