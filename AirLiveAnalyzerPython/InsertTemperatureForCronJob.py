import time
import mysql.connector
from gpiozero import CPUTemperature
from database import *

db = mysql.connector.connect(
    host = DB_SERVER,
    user= DB_USER,
    password = DB_PASS,
    database = DB_DB
    )

temperature = CPUTemperature()
dbcursor = db.cursor()
params = ["1",
          round(temperature.temperature,2),
          time.strftime('%Y-%m-%d %H:%M:%S')]

dbcursor.execute("INSERT INTO temperature (ProductId, Temperature, MeasuredDateTime) VALUES(%s, %s, %s)", params)
db.commit()
db.close()