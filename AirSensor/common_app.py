import scheduler
import logging

class CommonApp:

    def __init__(self, air_sensor, communication, args):
        self._air_sensor = air_sensor
        self._communication = communication
        self._args = args
        self._config_logger()

    def _config_logger(self):
        import os
        logging.basicConfig(level=logging.INFO if self._args.verbose else logging.WARNING,
        format="%(asctime)s [%(levelname)-5.5s] %(message)s",
        handlers=[logging.FileHandler(f'{os.getcwd()}/client.log'), logging.StreamHandler()]
        )
        
    def gather_data_and_send(self):
        try:
            data = self._air_sensor.get_readout()
            logging.getLogger().info(data)
            self._communication.post_measurement(data)
        except Exception as e:
            logging.getLogger().exception(e)


    def start(self):
        scheduler.run(self.gather_data_and_send)
