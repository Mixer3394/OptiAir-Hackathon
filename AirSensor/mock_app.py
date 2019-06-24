"""
This app is for test purposes only!
"""

import argparse

def parse_args():
    parser = argparse.ArgumentParser(description='Receives data from connected sensors and transmits them to OptiAir server.')
    parser.add_argument('--url', type=str, help='Server URL.', default='https://optiair.azurewebsites.net/api/', required=False)
    parser.add_argument('--mock', type=bool, help='If true - transmits mocked data. Test purposes only!', default=False, required=False)
    parser.add_argument('--pm1_0', type=int, help='Only test purposes!', default=0, required=False)
    parser.add_argument('--pm2_5', type=int, help='Only test purposes!', default=0, required=False)
    parser.add_argument('--pm10', type=int, help='Only test purposes!', default=0, required=False)
    parser.add_argument('--temp', type=int, help='Only test purposes!', default=0, required=False)
    parser.add_argument('--humid', type=int, help='Only test purposes!', default=0, required=False)
    parser.add_argument('--pressure', type=int, help='Only test purposes!', default=0, required=False)
    return parser.parse_args()

args = parse_args()

from airsensor import MockAirSensor

# Sensors decorator
air_sensor = MockAirSensor(args.pm1_0, args.pm2_5, args.pm10, args.temp, args.humid, args.pressure)

import communication
communication = communication.Communication(args.url)

import common_app
app = common_app.CommonApp(air_sensor, communication, args)