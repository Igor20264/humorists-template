import json
import random
import threading
import socket

s = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

s.bind(('localhost', 1025))  # Привязываем серверный сокет к localhost и 3030 порту.
s.listen(1)  # Начинаем прослушивать входящие соединения.
conn, addr = s.accept()  # Метод который принимает входящее соединение.

def Start():
    while True:
        try:
            data = conn.recv(1024)  # Получаем данные из сокета.
        except:
            conn.close()
        if not data:
            break

        conn.sendall(data)  # Отправляем данные в сокет.
        data = data.decode('utf-8')
        if message_list[int(data)]:
            message_list[int(data)] = None

        print(message_list)
    conn.close()

start = threading.Thread(target=Start,daemon=True)
start.start()
message_list = {}
message_count = 1
while True:
    a = input()

    act_list = [{"newmark":random.randint(1,5),"type":"home"},
                {"addgem":random.randint(1,1000),"message":"хахахах а тут текст."},
                {"addcount":random.randint(1,1000),"message":"Braaa"},
                {"message":"text"}
    ]
    data = {"name":"Vova","last_name":"Pupkin","id":random.randint(1,100000),"mess_id":message_count,"action":act_list[random.randint(0,3)]}
    data = json.dumps(data).encode('utf-8')
    message_list[message_count] = False
    print(data)
    conn.sendall(data)
    message_count+=1