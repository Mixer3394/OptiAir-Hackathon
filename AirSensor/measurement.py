class Measurement:

    def __init__(self, pm1_0, pm2_5, pm10, temp, humid, pressure):
        self.pm1_0 = pm1_0
        self.pm2_5 = pm2_5
        self.pm10 = pm10
        self.temp = temp
        self.humid = humid
        self.pressure = pressure

    def __str__(self):
        text = f'PM1.0: {self.pm1_0}, ' \
        + f'PM2.5: {self.pm2_5}, ' \
        + f'PM10: {self.pm10}, ' \
        + f'Temperature: {self.temp}*C, ' \
        + f'Humidity: {self.humid}%, ' \
        + f'Pressure: {self.pressure} hPa'
        return text