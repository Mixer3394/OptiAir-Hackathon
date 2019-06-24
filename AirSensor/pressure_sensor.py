import bme280
from smbus2 import SMBus

class MockBme280Sensor:

    def pressure(self):
        return 1000

class Bme280Sensor:

    def __init__(self):
        self._bus = SMBus(1)
        self._address = 0x76
        self._calibration_params = bme280.load_calibration_params(self._bus, self._address)

    def __del__(self):
        self._bus.close()

    def _get_data(self):
        return bme280.sample(self._bus, self._address, self._calibration_params)
        
    def pressure(self):
        return self._get_data().pressure
        
