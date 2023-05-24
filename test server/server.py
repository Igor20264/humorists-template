import socket
import sqlite3

# Переменная для хранения активных пользователей
import threading
import time

active_users = []

class database:
    def __init__(self):
        self.conn = sqlite3.connect(r'C:\Program Files\SQLiteStudio\database', check_same_thread=False)

    def add_data(self,id,data):
        cursor = self.conn.cursor()
        cursor.execute("SELECT * FROM User_No_Send (id, data) VALUES (?, ?)",(id,str(data)))
        self.conn.commit()
        cursor.close()

    def add_user(self,id,name,surname,school,Class):
        cursor = self.conn.cursor()
        cursor.execute("INSERT INTO All_User (id, name, surname, school, class) VALUES (?, ?, ?, ?,?)",(id,name,surname,school,Class))
        self.conn.commit()
        cursor.close()

    def data_confirmed(self,id):
        cursor = self.conn.cursor()
        cursor.execute("DELETE FROM User_No_Send WHERE id = ?", (id,))
        self.conn.commit()
        cursor.close()

    def get_data_from_database(self,id):
        cursor = self.conn.cursor()
        cursor.execute("SELECT * FROM User_No_Send WHERE id = ?", (id,))
        data = cursor.fetchall()
        cursor.close()
        return data

    def __del__(self):
        self.conn.close()

    def __exit__(self, exc_type, exc_val, exc_tb):
        self.conn.close()

db = database()


# Функция для обработки подключения клиента
def handle_client_connection(client_socket):
    def send_data(message):
        message_parts = [message[i:i + 1023] for i in range(0, len(message), 1023)]
        client_socket.send("split".encode('utf-8'))
        for part in message_parts:
            client_socket.send(part.encode('utf-8'))
            time.sleep(0.02)
        client_socket.send("break".encode('utf-8'))
    local_id = client_socket.recv(256).decode('utf-8')
    message = str(db.get_data_from_database(local_id))
    send_data(message)
    err = 0
    while True and err<4:
        try:
            data = client_socket.recv(1024).decode('utf-8')
        except Exception as e:
            err+=1
            print('Ошибка при принятии подключения:', e)
        else:
            if data == "stop":
                break
            elif data == "update":
                send_data("okey")
            elif data == "data_confirmed":
                db.data_confirmed(local_id)

# Главная функция сервера
def start_server():
    # Создаем TCP сокет
    server_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)

    # Привязываем сокет к IP-адресу и порту
    server_address = ('127.0.0.1', 1234)
    server_socket.bind(server_address)

    # Начинаем прослушивать входящие подключения
    server_socket.listen(1)
    print('Сервер запущен и ожидает подключения клиентов...')

    while True:
        try:
            # Принимаем входящее подключение
            client_socket, client_address = server_socket.accept()
            #print('Подключение от:', client_address)

            # Обрабатываем подключение клиента в отдельном потоке
            ks = threading.Thread(target=handle_client_connection, args=(client_socket,))
            ks.start()

        except Exception as e:
            print('Ошибка при принятии подключения:', e)

# Запускаем сервер
start_server()