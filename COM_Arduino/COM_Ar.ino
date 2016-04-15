#include <string.h>
bool turn =0;
void setup() {
  Serial.begin(57600);
  pinMode(13, OUTPUT);
}
int check_pass()
{
  String pass = Serial.readString();
  return pass == "pass";
}
void process()
{
        String r = Serial.readString();
        if (r == "bat")
        {
          digitalWrite(13, HIGH);
          Serial.write("1234567#");
        } else 
        if (r == "tat")
        {
          digitalWrite(13, LOW);
          Serial.write("abcdefgh#");
        } else
        if (r=="Dong") turn =0;
        Serial.flush();
}
void loop()
{
  if (Serial.available() > 0)
  {
    if ( turn==0)
        if (check_pass())
            {
              turn =1;
              Serial.write("Dung pass#");
              process();
            } else Serial.write("Sai pass#");
     else  process();     
  }
}
