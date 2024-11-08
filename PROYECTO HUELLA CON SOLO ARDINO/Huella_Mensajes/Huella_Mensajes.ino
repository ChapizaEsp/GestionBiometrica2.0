#include <WiFi.h>
#include <WebServer.h>
#include <Preferences.h>
#include <ArduinoJson.h>
#include <UIPEthernet.h>
#include <SPI.h>

// WiFi configuration
const char* ssid = "ESP32-AP";
const char* password = "password123";
IPAddress local_ip(192,168,1,1);
IPAddress gateway(192,168,1,1);
IPAddress subnet(255,255,255,0);

//Configuracion de led para simular puertas
const int pulsador1 = 4;
const int pulsador2 = 13;

bool estadoAnteriorPulsador1 = HIGH;
bool estadoAnteriorPulsador2 = HIGH;

// Ethernet configuration
#define ENC28J60_CS 5
#define PIN_DETECTOR 4
byte mac[] = { 0xDE, 0xAD, 0xBE, 0xEF, 0xFE, 0xED };
IPAddress Server(192,168,1,103);
int ethernetServerPort = 55000;

// Desktop app configuration
IPAddress desktopAppIP(192,168,1,103);
const int desktopAppPort = 55000;

// Server and preferences
WebServer server(80);
EthernetServer ethernetServer(ethernetServerPort);
EthernetClient ethClient;
Preferences preferences;

// Pin definitions
const int LED_PIN = 2;
bool lastButtonState = HIGH;
const int MAX_USERS = 10;

void setup() {
  pinMode(pulsador1,INPUT_PULLUP);
  pinMode(pulsador2,INPUT_PULLUP);

  Serial.begin(115200);
  pinMode(LED_PIN, OUTPUT);
  pinMode(PIN_DETECTOR, INPUT);

  for(int i = 0; i < 5; i++) {
    digitalWrite(LED_PIN, HIGH);
    delay(100);
    digitalWrite(LED_PIN, LOW);
    delay(100);
  }

  // WiFi setup
  WiFi.softAP(ssid, password);
  WiFi.softAPConfig(local_ip, gateway, subnet);

  server.on("/login", HTTP_POST, handleLogin);
  server.on("/users", HTTP_GET, handleListUsers);
  server.on("/saveHuella", HTTP_POST, handleSaveHuella);
  server.on("/verificarHuella", HTTP_POST, handleCheckHuella);
  server.on("/abrircaja", HTTP_POST, handleAbrircaja);
  server.on("/Borrar", HTTP_POST, clearPreferences);

  server.begin();

  preferences.begin("userdata", false);

  Serial.println("HTTP server started");
  Serial.print("WiFi AP IP address: ");
  Serial.println(WiFi.softAPIP());
  
  setupEthernet();
  
}

void loop() {
  server.handleClient();
  handleEthernetClient();
  checkPinState();
}

void setupEthernet() {
  Ethernet.init(ENC28J60_CS);
  Serial.println("Iniciando conexión Ethernet...");
  
  if (Ethernet.begin(mac) == 0) {
    Serial.println("Error al obtener la IP mediante DHCP");
    Ethernet.begin(mac, Server);
  }
  
  Serial.print("Ethernet IP: ");
  Serial.println(Ethernet.localIP());

  ethernetServer.begin();
}

void sendMessageToDesktopApp(const char* message) {
    if (ethClient.connect(Server, desktopAppPort)) {
        String messageToSend = String(message) + "\n";
        ethClient.print(messageToSend);
        Serial.println(message);
        ethClient.stop();
    }
}

void handleEthernetClient() {
    EthernetClient ethernetClient = ethernetServer.available();
    if (ethernetClient) {
        String jsonBuffer = "";
        while (ethernetClient.connected()) {
            if (ethernetClient.available()) {
                char c = ethernetClient.read();
                if (c == '\n') {
                    if (jsonBuffer.length() > 0) {
                        deserializeJsonAndSave(jsonBuffer);
                        jsonBuffer = "";
                    }
                } else {
                    jsonBuffer += c;
                }
            }
        }
        ethernetClient.stop();
    }
}

void checkPinState() {
    int estadoPulsador1 = digitalRead(pulsador1);
    int estadoPulsador2 = digitalRead(pulsador2);

    if(estadoPulsador1 == LOW && estadoAnteriorPulsador1 == HIGH) {
        Serial.println("Puerta Cerrada");
        sendMessageToDesktopApp("Puerta Cerrada");
    }

    if(estadoPulsador1 == HIGH && estadoAnteriorPulsador1 == LOW) {
        Serial.println("Puerta Abierta");
        sendMessageToDesktopApp("Puerta Abierta");
    }
    
    estadoAnteriorPulsador1 = estadoPulsador1;

    if(estadoPulsador2 == LOW && estadoAnteriorPulsador2 == HIGH) {
        Serial.println("Perilla Cerrada");
        sendMessageToDesktopApp("Perilla Cerrada");
    }

    if(estadoPulsador2 == HIGH && estadoAnteriorPulsador2 == LOW) {
        Serial.println("Perilla Abierta");
        sendMessageToDesktopApp("Perilla Abierta");
    }
    
    estadoAnteriorPulsador2 = estadoPulsador2;

    delay(500);
}

void handleLogin() {
    if (!server.hasArg("plain")) {
        server.send(400, "application/json", "{\"status\":\"error\",\"message\":\"No se recibieron datos\"}");
        return;
    }

    String body = server.arg("plain");
    DynamicJsonDocument doc(1024);
    DeserializationError error = deserializeJson(doc, body);

    if (error) {
        server.send(400, "application/json", "{\"status\":\"error\",\"message\":\"Error al procesar los datos JSON\"}");
        return;
    }

    const char* nombre = doc["Nombre"];
    const char* contrasena = doc["Contraseña"];

    if (nombre && contrasena) {
        int userCount = preferences.getInt("userCount", 0);
        bool userFound = false;
        String token = "";
        String userKey = "";

        for (int i = 0; i < userCount; i++) {
            userKey = "user" + String(i);
            String storedUser = preferences.getString(userKey.c_str(), "");
            String storedPass = preferences.getString((userKey + "_pass").c_str(), "");

            if (storedUser == nombre && storedPass == contrasena) {
                userFound = true;
                token = preferences.getString((userKey + "_token").c_str(), "");
                break;
            }
        }

        if (userFound) {
            DynamicJsonDocument responseDoc(1024);
            responseDoc["status"] = "success";
            responseDoc["message"] = "Usuario autenticado correctamente";
            responseDoc["huellaRegistrada"] = token.length() > 0;
            responseDoc["token"] = token;

            String response;
            serializeJson(responseDoc, response);
            server.send(200, "application/json", response);
        } else {
            sendMessageToDesktopApp("Usuario no encontrado o contraseña incorrecta");
            server.send(404, "application/json", "{\"status\":\"error\",\"message\":\"Usuario no encontrado o contraseña incorrecta\"}");
        }
    } else {
        server.send(400, "application/json", "{\"status\":\"error\",\"message\":\"Falta nombre de usuario o contraseña\"}");
    }
}

void handleListUsers() {
    int userCount = preferences.getInt("userCount", 0);
    DynamicJsonDocument doc(1024);
    JsonArray users = doc.createNestedArray("users");

    for (int i = 0; i < userCount; i++) {
        String userKey = "user" + String(i);
        String userName = preferences.getString(userKey.c_str(), "");
        String userContrasena = preferences.getString((userKey + "_pass").c_str(), "");
        String userToken = preferences.getString((userKey + "_token").c_str(), "");
        
        if (userName.length() > 0) {
            JsonObject user = users.createNestedObject();
            user["nombre"] = userName;
            user["contrasena"] = userContrasena;
            user["huellaRegistrada"] = userToken.length() > 0;
        }
    }

    String response;
    serializeJson(doc, response);
    server.send(200, "application/json", response);
}
void handleSaveHuella() {
    if (!server.hasArg("plain")) {
        server.send(400, "application/json", "{\"status\":\"error\",\"message\":\"No se recibieron datos\"}");
        return;
    }

    String body = server.arg("plain");
    DynamicJsonDocument doc(1024);
    DeserializationError error = deserializeJson(doc, body);

    if (error) {
        server.send(400, "application/json", "{\"status\":\"error\",\"message\":\"Error al procesar los datos JSON\"}");
        return;
    }

    const char* usuario = doc["usuario"];
    const char* token = doc["token"];

    if (usuario && token) {
        int userCount = preferences.getInt("userCount", 0);
        bool userFound = false;

        for (int i = 0; i < userCount; i++) {
            String userKey = "user" + String(i);
            if (preferences.getString(userKey.c_str(), "") == usuario) {
                preferences.putString((userKey + "_token").c_str(), token);
                userFound = true;
                Serial.println("Huella registrada exitosamente para el usuario: " + String(usuario));
                sendMessageToDesktopApp(("Huella exitosamente para el usuario: " + String(usuario)).c_str());
                break;
            }
        }

        if (userFound) {
            server.send(200, "application/json", "{\"status\":\"success\",\"message\":\"Token de huella guardado\"}");
        } else {
            sendMessageToDesktopApp("Usuario no encontrado para registro de huella");
            server.send(404, "application/json", "{\"status\":\"error\",\"message\":\"Usuario no encontrado\"}");
        }
    } else {
        server.send(400, "application/json", "{\"status\":\"error\",\"message\":\"Faltan datos requeridos\"}");
    }
}

void handleAbrircaja() {
    if (!server.hasArg("plain")) {
        server.send(400, "application/json", "{\"status\":\"error\",\"message\":\"No se recibieron datos\"}");
        return;
    }

    String body = server.arg("plain");
    DynamicJsonDocument doc(1024);
    DeserializationError error = deserializeJson(doc, body);

    if (error) {
        server.send(400, "application/json", "{\"status\":\"error\",\"message\":\"Error al procesar los datos JSON\"}");
        return;
    }

    const char* usuario = doc["usuario"];
    const char* token = doc["token"];

    if (usuario && token) {
        int userCount = preferences.getInt("userCount", 0);
        bool tokenValido = false;

        for (int i = 0; i < userCount; i++) {
            String userKey = "user" + String(i);
            if (preferences.getString(userKey.c_str(), "") == usuario) {
                String storedToken = preferences.getString((userKey + "_token").c_str(), "");
                if (storedToken == token) {
                    tokenValido = true;
                    break;
                }
            }
        }

        if (tokenValido) {
        digitalWrite(LED_PIN, HIGH);
        String message = "Caja abierta por el usuario: " + String(usuario);
        sendMessageToDesktopApp(message.c_str());
        delay(2000);
        digitalWrite(LED_PIN, LOW);
        sendMessageToDesktopApp("Caja cerrada");
         } else {
            sendMessageToDesktopApp("Token inválido o usuario no encontrado");
            server.send(401, "application/json", "{\"status\":\"error\",\"message\":\"Token inválido o usuario no encontrado\"}");
        }
    } else {
        server.send(400, "application/json", "{\"status\":\"error\",\"message\":\"Faltan datos requeridos\"}");
    }
}

void deserializeJsonAndSave(String jsonString) {
    DynamicJsonDocument doc(200);
    DeserializationError error = deserializeJson(doc, jsonString);

    if (error) {
        sendMessageToDesktopApp("Error: Error al deserializar JSON");
        return;
    }

    String username = doc["Username"].as<String>();
    String password = doc["Password"].as<String>();

    if (username.length() > 0 && password.length() > 0) {
        int userCount = preferences.getInt("userCount", 0);
        
        if (userCount >= MAX_USERS) {
            clearPreferences();
            userCount = 0;
        }
        
        String userKey = "user" + String(userCount);
        preferences.putString(userKey.c_str(), username);
        preferences.putString((userKey + "_pass").c_str(), password);
        preferences.putString((userKey + "_token").c_str(), "");

        userCount++;
        preferences.putInt("userCount", userCount);

        // Enviar mensajes de la misma forma que checkPinState
        sendMessageToDesktopApp("Usuario guardado");
        
        String username = "kl";
        sendMessageToDesktopApp(username.c_str()); 
        String password = password;
        sendMessageToDesktopApp(password.c_str());
        
        sendMessageToDesktopApp("Huella: No registrada");
    } else {
        sendMessageToDesktopApp("Error: Username or Password is empty");
    }
}

void handleCheckHuella() {
    if (!server.hasArg("plain")) {
        server.send(400, "application/json", "{\"status\":\"error\",\"message\":\"No se recibieron datos\"}");
        return;
    }

    String body = server.arg("plain");
    DynamicJsonDocument doc(2048);
    DeserializationError error = deserializeJson(doc, body);
    if (error) {
        sendMessageToDesktopApp("Error al procesar datos JSON de huella");
        server.send(400, "application/json", "{\"status\":\"error\",\"message\":\"Error al procesar los datos JSON\"}");
        return;
    }

    const char* usuario = doc["usuario"];
    const char* token = doc["token"];
    if (usuario == nullptr || token == nullptr) {
        sendMessageToDesktopApp("Faltan datos en verificación de huella");
        server.send(400, "application/json", "{\"status\":\"error\",\"message\":\"Faltan datos requeridos\"}");
        return;
    }

    int userCount = preferences.getInt("userCount", 0);
    bool tokenValido = false;

    for (int i = 0; i < userCount; i++) {
        String userKey = "user" + String(i);
        String storedUser = preferences.getString(userKey.c_str(), "");

        if (storedUser == String(usuario)) {
            String storedToken = preferences.getString((userKey + "_token").c_str(), "");
            if (storedToken == String(token)) {
                tokenValido = true;
                break;
            }
        }
    }

    if (tokenValido) {
        server.send(200, "application/json", "{\"status\":\"success\",\"message\":\"Token verificado correctamente\"}");
    } else {
        server.send(401, "application/json", "{\"status\":\"error\",\"message\":\"Token inválido o usuario no encontrado\"}");
    }
}

void clearPreferences() {
    preferences.clear();
    sendMessageToDesktopApp("Preferences vaciadas. Todos los datos de usuarios han sido eliminados.");
}

void sendAlertToDesktopApp(const char* usuario) {
    if (ethClient.connect(Server, desktopAppPort)) {
        String message = "Alerta de seguridad - Acceso por usuario: " + String(usuario) + "\n";
        ethClient.print(message);
        Serial.println("Alerta enviada: " + message);
        ethClient.stop();
    } else {
        sendMessageToDesktopApp("Error al enviar alerta de seguridad");
    }
}