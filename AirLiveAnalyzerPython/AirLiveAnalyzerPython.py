import mysql.connector
from matplotlib import pyplot as plt
from matplotlib import style
from database import *

style.use('ggplot')

db = mysql.connector.connect(
    host = DB_SERVER,
    user= DB_USER,
    password = DB_PASS,
    database = DB_DB
    )

dbcursor = db.cursor()

dbcursor.execute("SELECT MeasuredDateTime FROM Temperature")
xAxis = dbcursor.fetchall()

dbcursor.execute("SELECT Temperature FROM Temperature")
yAxis = dbcursor.fetchall()
db.close()
print(xAxis)
print(yAxis)

plt.plot(xAxis,yAxis)

plt.title('Temperature in Celcius')
plt.ylabel('Temp')
plt.xlabel('Time')

plt.show();