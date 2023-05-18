using UnityEngine;
using System;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


public class TcpClientExample : MonoBehaviour
{
	private TcpClient client;
	private NetworkStream stream;
	private byte[] receiveBuffer = new byte[1024];
	private bool isConnected = false;

	List<string> getedServerData = new List<string>();

	private string ip;
	private int port;

	private TcpClientExample(string ip,int port=1025)
	{
		this.ip = ip;
		this.port = port;
	}
	
	public string GetData():
	{
		return getedServerData
	}

	private async void Start()
	{ 
		await ConnectToServerAsync(ip, port);
	}

	private async Task ConnectToServerAsync(string serverIP, int serverPort,string personData)
	{
		while (!isConnected)
		{
			try
			{
				client = new TcpClient();
				await client.ConnectAsync(serverIP, serverPort);
				stream = client.GetStream();
				isConnected = true;
				Debug.Log("���������� � �������: " + serverIP + ":" + serverPort);

				// �������� ������ �� ������
				byte[] sendData = Encoding.ASCII.GetBytes(personData);
				await stream.WriteAsync(sendData, 0, sendData.Length);

				// ��������� ���������� �� �������
				await ReceiveInstructionsAsync();
			}
			catch (Exception e)
			{
				Debug.Log("������ ����������� � �������: " + e.Message);
				await Task.Delay(1000); // ����� ����� ��������� �������� �����������
			}
		}
	}

	private async Task ReceiveInstructionsAsync()
	{
		try
		{
			while (isConnected)
			{
				int bytesRead = await stream.ReadAsync(receiveBuffer, 0, receiveBuffer.Length);
				if (bytesRead > 0)
				{
					// ��������� ���������� ����������
					string receivedData = Encoding.ASCII.GetString(receiveBuffer, 0, bytesRead);
					getedServerData.Add(receivedData);
					Debug.Log("�������� �� �������: " + receivedData);

					// ���������� �������� ������ �� �������
					Array.Clear(receiveBuffer, 0, receiveBuffer.Length);
				}
				else
				{
					// �������� ���������� ��� ��������� ������� ��������� �� �������
					CloseConnection();
				}
			}
		}
		catch (Exception e)
		{
			Debug.Log("������ ��� ������ ������ �� �������: " + e.Message);
		}
	}

	private void CloseConnection()
	{
		try
		{
			// �������� ����������
			stream.Close();
			client.Close();
			isConnected = false;
			Debug.Log("���������� �������");
		}
		catch (Exception e)
		{
			Debug.Log("������ ��� �������� ����������: " + e.Message);
		}
	}
}