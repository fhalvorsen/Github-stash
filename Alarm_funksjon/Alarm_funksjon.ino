#include <Wire.h>
#include "RTClib.h"
#include <SPI.h>
#include  <Adafruit_ST7735.h>
#include  <Adafruit_GFX.h>
#include <Fonts/FreeMonoBold12pt7b.h> //Fonten på dato og tid

RTC_DS1307 RTC;
const int buzzer = 8; //Buzzer 

#define TFT_CS     10
#define TFT_RST    9
#define TFT_DC     7
Adafruit_ST7735 tft = Adafruit_ST7735(TFT_CS,  TFT_DC, TFT_RST);
#define TFT_SCLK 13
#define TFT_MOSI 11

void setup () {
  tft.initR(INITR_BLACKTAB);
  tft.fillScreen(ST7735_BLACK);
  tft.setFont(&FreeMonoBold12pt7b);
  Serial.begin(9600);
  Wire.begin();
  RTC.begin();
  pinMode(buzzer, OUTPUT);

  if (! RTC.isrunning()) {
    Serial.println("RTC is NOT running!");
    //RTC.adjust(DateTime(__DATE__, __TIME__)); // Use ONCE then comment out and reload
    //RTC.adjust(DateTime(2018, 04, 11, 16, 00, 00)); // Set time manually ONCE as above
  }
}

void loop () {
  drawTime();
}


void drawTime () {
  DateTime now = RTC.now();
  
  tft.setCursor(0, 100);
  tft.print(now.day(), DEC);
  tft.print('-');
  tft.print(now.month(), DEC);
  tft.print('-');
  tft.print(now.year(), DEC);
  tft.print(' ');

  tft.setCursor(25, 55);
  if (now.hour()<10)
    tft.print('0');
    tft.print(now.hour(), DEC);
    tft.print(':');
     if (now.minute()<10)
    tft.print('0');
    tft.print(now.minute(), DEC);
    tft.setCursor(12, 0);
    if (now.hour() == 17 && now.minute() == 58){
      Buzz();
    }
    

}

void Buzz() {
    tone(buzzer, 1000); // Send 1KHz sound signal...
    delay(500);        // ...for 1 sec
    tone(buzzer, 1000);     // Stop sound...
    delay(500);        // ...for 1sec
  }




