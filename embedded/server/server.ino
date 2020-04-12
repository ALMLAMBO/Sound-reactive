#include <ESP8266WiFi.h>

const char * ssid = "sound";
const char * password = "password";

WiFiServer server(80);
String header;

void setup() {
  Serial.begin(115200);
  digitalWrite(LED_BUILTIN, LOW);
  Serial.print("Connecting to ");
  Serial.println(ssid);
  WiFi.begin(ssid, password);

  int i = 0;
  while (WiFi.status() != WL_CONNECTED) {
    delay(500);
    Serial.print(++i); Serial.print(' ');
  }

  digitalWrite(LED_BUILTIN, HIGH);

  Serial.println("");
  Serial.println("WiFi connected.");
  Serial.println("IP address: ");
  Serial.println(WiFi.localIP());
  server.begin();
}

void loop() {
  
}
