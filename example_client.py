import socket
import time


def connect_to_server(server_ip, server_port):
    while True:
        try:
            client = socket.socket(socket.AF_INET, socket.SOCK_STREAM)
            client.connect((server_ip, server_port))
            print(f"Подключено к серверу: {server_ip}:{server_port}")

            # Отправка данных на сервер
            data_to_send = "Hello, server!"
            client.send(data_to_send.encode())

            # Получение инструкций от сервера
            while True:
                received_data = client.recv(2048).decode()
                if received_data:
                    # Обработка полученных инструкций
                    print("Получено от сервера:", received_data)
                else:
                    # Закрытие соединения при получении пустого сообщения от сервера
                    close_connection(client)
                    return

        except Exception as e:
            print("Ошибка подключения к серверу:", e)
            time.sleep(1)  # Пауза перед повторной попыткой подключения

def close_connection(client):
    try:
        # Закрытие соединения
        client.close()
        print("Соединение закрыто")
    except Exception as e:
        print("Ошибка при закрытии соединения:", e)

# Пример использования
server_ip = "127.0.0.1"
server_port = 1025
connect_to_server(server_ip, server_port)