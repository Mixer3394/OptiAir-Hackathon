import schedule
import time
import pms7003
import si7021
import pressure_sensor
import airsensor

dust_sensor = pms7003.Pms7003Sensor('/dev/ttyS0')
temp_sensor = si7021.Si7021Sensor()
humid_sensor = temp_sensor
pressure_sensor = pressure_sensor.MockBme280Sensor()
air_sensor = airsensor.AirSensor(dust_sensor, temp_sensor, humid_sensor, pressure_sensor)

def gather_data_and_send():
    pm, temp, humid, pressure = air_sensor.get_readout()
    print(f'pm1_0: {pm[0]}')
    print(f'pm2_5: {pm[1]}')
    print(f'pm10: {pm[2]}')
    print(f'temp: {temp}')
    print(f'humid: {humid}')
    print(f'pressure: {pressure}')


schedule.every(1).seconds.do(gather_data_and_send)

while True:
    try:
        schedule.run_pending()
        # time.sleep(1)
    except KeyboardInterrupt:
        exit(0)