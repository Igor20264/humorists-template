import json
import random
import threading
import socket
import time

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
addresses = {}
s.bind(('localhost', 1025))  # Привязываем серверный сокет к localhost и 3030 порту.
s.listen(1)  # Начинаем прослушивать входящие соединения.
conn, addr = s.accept()  # Метод который принимает входящее соединение.

def Start():
    while True:
        try:client, addr = conn.accept()
        except socket.error:
            pass
        else:
            data,addr = conn.recv(2048)  # Получаем данные из сокета.
            print(addr)
            if not data:
                break

            #conn.sendall(data)  # Отправляем данные в сокет.
            data = data.decode('utf-8')
            print(data)
    conn.close()

start = threading.Thread(target=Start,daemon=True)
start.start()
message_count = 1
from faker import Faker
fake = Faker(['ru_RU'])
while True:
    a = input()
    for i in range(100):
        st = fake.name()
        data = {"name":st.split(" ")[0],"last_name":st.split(" ")[1],"id":random.randint(1,100000),"mess_id":message_count,"type":1,"message":fake.text(),"action":fake.text()}
        time.sleep(0.08)
        data = json.dumps(data).encode('utf-8')
        conn.sendall(data)
        message_count+=1