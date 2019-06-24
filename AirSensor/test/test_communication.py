import unittest
from communication import Communication
import measurement
import requests

class CommunicationTests(unittest.TestCase):

    def test_false_url(self):
        url = 'asdfasdfff'
        meas = measurement.Measurement(0, 0, 0, 0, 0, 0)
        com = Communication(url)

        with self.assertRaises(Exception):
            com.post_measurement(meas)

    def test_wrong_url(self):
        url = 'https://optiair.azurewebsites.net/api/asdf'
        meas = measurement.Measurement(0, 0, 0, 0, 0, 0)
        com = Communication(url)

        with self.assertRaises(requests.HTTPError):
            com.post_measurement(meas)

    def test_none_data(self):
        url = 'https://optiair.azurewebsites.net/api/'
        meas = None
        com = Communication(url)

        with self.assertRaises(AttributeError):
            com.post_measurement(meas)

if __name__ == '__main__':
    unittest.main()