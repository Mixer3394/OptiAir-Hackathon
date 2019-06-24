import argparse

def parse_args():
    parser = argparse.ArgumentParser(description='Receives data from connected sensors and transmits them to OptiAir server.')
    parser.add_argument('--url', type=str, help='Server URL.', default='https://optiair.azurewebsites.net/api/', required=False)
    return parser.parse_args()

args = parse_args()

import pms7003
import si7021
import pressure_sensor
import airsensor

# Used sensors
dust_sensor = pms7003.Pms7003Sensor('/dev/ttyS0')
temp_sensor = si7021.Si7021Sensor()
humid_sensor = temp_sensor
pressure_sensor = pressure_sensor.MockBme280Sensor()
# Sensors decorator
air_sensor = airsensor.AirSensor(dust_sensor, temp_sensor, humid_sensor, pressure_sensor)

import communication
communication = communication.Communication(args.url)

import common_app
app = common_app.CommonApp(air_sensor, communication, args)