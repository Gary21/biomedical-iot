# BiomedicalDataService
.NET service for receiving data from medical sensors

Devided into four separated containers:
DataGenerator - generates random (can be parametrised) samples data, can be replaced with physical device
MQTT - queuing service, used in communication between DataGenerator(or device) and WebAPI
WebAPI - backend, saves samples in database and handles frontend requests
frontend - GUI

## Run Locally

Clone the project

```bash
  git clone https://github.com/Gary21/biomedical-iot.git
```

Go to the project directory

```bash
  cd biomedical-data-service
```

Start containers with docker compose file

```bash
  docker compose -p project up -d
```

Default MQTT credentials
```
login: user1
password: user1
```