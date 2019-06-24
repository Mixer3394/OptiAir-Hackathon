import scheduler
import logging
import requests
import time

class CommonApp:
    """
    Common app logic for both concrete and mocked application.
    """

    def __init__(self, air_sensor, communication, args):
        self._air_sensor = air_sensor
        self._communication = communication
        self._args = args
        self._config_logger()

        self._login()

    def _config_logger(self):
        import os
        logging.basicConfig(level=logging.INFO if self._args.verbose else logging.WARNING,
        format="%(asctime)s [%(levelname)-5.5s] %(message)s",
        handlers=[logging.FileHandler(f'{os.getcwd()}/client.log'), logging.StreamHandler()]
        )
        logging.getLogger().info('Logger initialized.')
        
    def _login(self):
        """
        Tries to log into server. 
        Required to perform in order to run send measurements to server.
        """
        while True:
            try:
                self._communication.login()
                logging.getLogger().info('Logged in')
            except KeyboardInterrupt:
                exit(0)
            except requests.HTTPError as http_exception:
                logging.getLogger().exception(http_exception)
                time.sleep(1)
            except Exception as e:
                logging.getLogger().critical(e)
                break

    def gather_data_and_send(self):
        """
        Reads data from sensors and trasmit them to server.
        Normally invoked by 'start' method.
        """
        try:
            data = self._air_sensor.get_readout()
            logging.getLogger().info(data)
            self._communication.post_measurement(data)
        except Exception as e:
            logging.getLogger().exception(e)

    def start(self):
        """
        Starts transmission to server.
        """
        scheduler.run(self.gather_data_and_send)
