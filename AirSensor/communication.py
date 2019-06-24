import requests
import measurement
import json

class Communication:

    def __init__(self, url):
        self._url = url
        self._headers = { 'content-type':'application/json' }
        self._mac = ''

    def _measurement_url(self):
        return self._url + 'measurements/'

    def _login_url(self):
        return self._url + 'devices/login/'

    def _get_mac(self):
        if self._mac == '':
            import getmac
            self._mac = getmac.get_mac_address()
        return self._mac

    def _create_dict_data(self, data):
        measurement = {}
        measurement['pM1'] = data.pm1_0
        measurement['pM25'] = data.pm2_5
        measurement['pM10'] = data.pm10
        measurement['temperature'] = data.temp
        measurement['humidity'] = data.humid
        measurement['pressure'] = data.pressure
        measurement['deviceMAC'] = self._get_mac()
        return measurement

    def login(self):
        login_data = {}
        login_data['deviceMAC'] = self._get_mac()
        login_request = requests.post(self._login_url(), json=login_data, headers=self._headers)
        login_request.raise_for_status()

    def post_measurement(self, data):
        measurement_dict = self._create_dict_data(data)
        post_request = requests.post(self._measurement_url(), json=measurement_dict, headers=self._headers)
        post_request.raise_for_status()



