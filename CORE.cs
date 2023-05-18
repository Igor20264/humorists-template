using UnityEngine;
using UnityEngine.UI;

public class ServerDataLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Text loadingText;

    private void Start()
    {
        // �������� ����� ��������
        loadingScreen.SetActive(true);
        loadingText.text = "��������...";

        // ������ �������� ������ � �������
        StartCoroutine(LoadDataFromServer());
    }

    private IEnumerator LoadDataFromServer()
    {
        // ��������� ������ � �������
        l = newTcpClient.TcpClientExample("127.0.0.1");
        // �������� ������ � �������
        data = l.GetData()
        // �������� ���������
        loadingText.text = "�������� ���������!";
        yield return new WaitForSeconds(1f);

        // ������ ����� ��������
        loadingScreen.SetActive(false);

        // ���������� ����� ��� ��������� ������ �������� ����� ��������
    }
}