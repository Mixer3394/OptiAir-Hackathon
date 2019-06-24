import measurement

class AirSensor:

    def __init__(self, dust_sensor, temp_sensor, humidity_sensor, pressure_sensor):
        self._dust_sensor = dust_sensor
        self._temp_sensor = temp_sensor
        self._humid_sensor = humidity_sensor
        self._pressure_sensor = pressure_sensor


    def _get_dust_readout(self):
        readout = self._dust_sensor.read()
        pm1_0 = readout['pm1_0']
        pm2_5 = readout['pm2_5']
        pm10 = readout['pm10']
        return pm1_0, pm2_5, pm10

    def _get_temp_readout(self):
        temperature = self._temp_sensor.temperature()
        return temperature

    def _get_humid_readout(self):
        humidity = self._humid_sensor.humidity()
        return humidity

    def _get_pressure_readout(self):
        pressure = self._pressure_sensor.pressure()
        return pressure

    def get_readout(self):
        pm1_0, pm2_5, pm10 = self._get_dust_readout()
        temp = self._get_temp_readout()
        humid = self._get_humid_readout()
        pressure = self._get_pressure_readout()

        last_measurement = measurement.Measurement(pm1_0, pm2_5, pm10, temp, humid, pressure)
        
        return last_measurement
