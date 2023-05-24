import hashlib
import socket
import time

def send_data_to_server(id):
    while True:
        try:
            # Создаем TCP сокет
            client_socket = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            server_address = ('127.0.0.1', 1234)
            client_socket.connect(server_address)

            def get_data():
                s = True
                l = ""
                while s:
                    response = client_socket.recv(1024).decode('utf-8')
                    if response == "break":
                        break
                    else:
                        l+=response
                return l

            def send_command(mess,returns=False):
                client_socket.send(mess.encode('utf-8'))
                if returns:
                    response = client_socket.recv(1024).decode('utf-8')
                    if response == "split":
                        s = get_data()
                        return s
                    else:
                        return response

            l = send_command(id,returns=True)
            print(l)
            l = send_command(types[2],True)
            print(l)
            send_command(types[3])
            send_command(types[4])

            client_socket.close()
            break

        except Exception as e:
            print('Ошибка при подключении к серверу:', e)
            print('Повторная попытка подключения через 5 секунд...')
            time.sleep(5)  # Пауза перед повторной попыткой подключения

name = "Ваня".upper()
last_name = "Терешков".lower()
Class = "7A".upper()
school = "МОУ Синьковска СОШ 2".lower().replace(" ","")
id = name+last_name+Class+school
m = hashlib.sha224(id.encode())
print(f"Имя: {name} | Фамилия: {last_name} | Класс: {Class} | Школа: {school}")
print(f"id: {m.hexdigest()}")
l = m.hexdigest()
types = {
    2:"update",
    3:"data_confirmed",
    4:"stop",
    99:"del"
}
# Отправляем данные на сервер

send_data_to_server(l)
