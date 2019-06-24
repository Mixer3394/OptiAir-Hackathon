import schedule
import pms7003
import si7021
import pressure_sensor
import airsensor
import communication

url = 'https://optiair.azurewebsites.net/api/'

# Used sensors
dust_sensor = pms7003.Pms7003Sensor('/dev/ttyS0')
temp_sensor = si7021.Si7021Sensor()
humid_sensor = temp_sensor
pressure_sensor = pressure_sensor.MockBme280Sensor()
# Sensors decorator
air_sensor = airsensor.AirSensor(dust_sensor, temp_sensor, humid_sensor, pressure_sensor)
# Communication
communication = communication.Communication(url)


def gather_data_and_send():
    data = air_sensor.get_readout()
    communication.post_measurement(data)


schedule.every(10).seconds.do(gather_data_and_send)

while True:
    try:
        schedule.run_pending()
        # time.sleep(1)
    except KeyboardInterrupt:
        exit(0)