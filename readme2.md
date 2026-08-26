# Haru App 2 - Sensor Dashboard

## Overview

PyQt6 desktop application that displays real-time sensor data from ESP32 via cloud API.

## Data Source

ESP32 sends JSON array to cloud API:
```json
[
  {"timestamp": 1787222431, "data": 2164, "data_type": "number", "label": "co2"},
  {"timestamp": 1787222431, "data": 1, "data_type": "number", "label": "pm1.0"},
  {"timestamp": 1787222431, "data": 2, "data_type": "number", "label": "pm2.5"},
  {"timestamp": 1787222431, "data": 2, "data_type": "number", "label": "pm10"},
  {"timestamp": 1787222431, "data": 29.03, "data_type": "number", "label": "temperature"},
  {"timestamp": 1787222431, "data": 42.6, "data_type": "number", "label": "humidity"},
  {"timestamp": 1787222431, "data": 1000.8, "data_type": "number", "label": "pressure"},
  {"timestamp": 1787222431, "data": 62181, "data_type": "number", "label": "gas"},
  {"timestamp": 1787222431, "data": 51, "data_type": "number", "label": "battery"}
]
```

## API Response

GET request returns:
```json
{
  "latest": {
    "co2": {"data": 2164, "reading_time": "2026-08-26 12:00:00"},
    "temperature": {"data": 29.03, "reading_time": "2026-08-26 12:00:00"},
    ...
  },
  "history": {
    "co2": [{"reading_time": "2026-08-26 11:59:00", "data": 2160}, ...],
    ...
  }
}
```

## Features

### 1. Real-time Sensor Cards (Left Side)
9 cards showing current value for each sensor:
- CO2 (ppm)
- PM1.0 (ug/m3)
- PM2.5 (ug/m3)
- PM10 (ug/m3)
- Temperature (C)
- Humidity (%)
- Pressure (hPa)
- Gas (ohm)
- Battery (%)

Each card shows: sensor name, current value, last updated time

### 2. Line Charts (Right Side)
3 charts displayed vertically (stacked on top of each other):
- **Top chart:** Air Quality (CO2, PM1.0, PM2.5, PM10)
- **Center chart:** Environment (Temperature, Humidity, Pressure)
- **Bottom chart:** Other (Gas, Battery)

Each chart:
- X-axis: time (HH:MM:SS format)
- Y-axis: sensor values
- Multiple lines (one per sensor in group)
- Shows ~100 historical data points
- Title at top of each chart

### 3. Collapsible Log Screen (Bottom)
- Small tab at bottom: "[Log]"
- Click to expand/collapse
- Shows API requests, errors, status messages
- Color-coded: Red=error, Green=ok, White=info

### 4. Auto-refresh
- Polls API every 5 seconds
- Updates cards and charts automatically

## Layout

```
+-------------------+---------------------------------------------------+
|  Config Panel     |   Air Quality Chart                               |
|  [API URL]        |   +-------------------------------------------+   |
+-------------------+   |  CO2, PM1.0, PM2.5, PM10                  |   |
|  Real-time        |   |    /\  /\                                 |   |
|  Cards            |   |   /  \/  \                                |   |
|                   |   +-------------------------------------------+   |
|  +-----++-----+  |                                                   |
|  | CO2 || PM1 |  |   Environment Chart                               |
|  |2164 ||  1  |  |   +-------------------------------------------+   |
|  +-----++-----+  |   |  Temp, Humidity, Pressure                 |   |
|  +-----++-----+  |   |    /\  /\                                 |   |
|  |Temp ||Humid|  |   |   /  \/  \                                |   |
|  |29.0 ||42.6 |  |   +-------------------------------------------+   |
|  +-----++-----+  |                                                   |
|  +-----++-----+  |   Other Chart                                     |
|  |Gas  ||Batt |  |   +-------------------------------------------+   |
|  |62181|| 51  |  |   |  Gas, Battery                             |   |
|  +-----++-----+  |   |    /\  /\                                 |   |
|  ...             |   |   /  \/  \                                |   |
|  [Pressure]      |   +-------------------------------------------+   |
|  |1000.8|        |                                                   |
+-------------------+---------------------------------------------------+
|  [Log]  <-- click to expand                                           |
+------------------------------------------------------------------------+
```

## File Structure

| File | Purpose |
|------|---------|
| main.py | Entry point - creates QApplication, shows window |
| app.py | Main class SensorDashboardApp - assembles UI, handles polling |
| panels.py | UI panels - create_config_group(), create_cards_group(), create_log_group() |
| chart.py | ChartWidget - 3 line charts for sensor groups |
| worker.py | Worker(QThread) - background GET/POST requests |

## Dependencies

```
pip install PyQt6 requests matplotlib
```

## Run

```
python main.py
```

## API Endpoints

| Action | Method | Purpose |
|--------|--------|---------|
| GET /api.php | GET | Fetch latest + history |
| POST /api.php | POST | Send sensor data (from ESP32) |

## Sensor Groups

| Group | Sensors |
|-------|---------|
| Air Quality | CO2, PM1.0, PM2.5, PM10 |
| Environment | Temperature, Humidity, Pressure |
| Other | Gas, Battery |

## Polling

- Interval: 5 seconds
- History: ~100 data points per sensor
- Updates: Cards + Charts simultaneously

## Log Screen

- Default: Collapsed (small tab at bottom)
- Expand: Click "[Log]" tab
- Content: API requests, errors, status
- Colors: Red=error, Green=ok, White=info
