import scheduler

class CommonApp:

    def __init__(self, air_sensor, communication, args):
        self._air_sensor = air_sensor
        self._communication = communication
        self._args = args
        
    def gather_data_and_send(self):
        data = self._air_sensor.get_readout()
        self._communication.post_measurement(data)

    def app_start(self):
        scheduler.run(self.gather_data_and_send)
