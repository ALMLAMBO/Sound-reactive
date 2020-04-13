#include <ESP8266WiFi.h>

const char * ssid = "sound_reactive";
const char * password = "password";

WiFiServer server(8080);
String header;

void setup() {
  Serial.begin(115200);
  Serial.print("Connecting to ");
  Serial.println(ssid);
  WiFi.persistent(false);
  WiFi.mode(WIFI_AP);
  WiFi.softAP(ssid, password);

  pinMode(LED_BUILTIN, OUTPUT);
  digitalWrite(LED_BUILTIN, LOW);

  int watchDog = 0;
  while (WiFi.status() != WL_DISCONNECTED) {
    delay(500);
    watchDog++;
    
    if(watchDog == 60) {
      WiFi.disconnect();
      WiFi.printDiag(Serial);
      watchDog = 0;
      return;
    }
  }

  Serial.println("Connected");

  Serial.println("WiFi connected.");
  Serial.println("IP address: ");
  Serial.println(WiFi.softAPIP());
  server.begin();
}

void loop() {
  WiFiClient new_client = server.available();
  if(new_client) {
    Serial.println("New client connected");
  }
  
  delay(1000);
}
