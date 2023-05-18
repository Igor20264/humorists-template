using UnityEngine;
using UnityEngine.UI;

public class ServerDataLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Text loadingText;

    private void Start()
    {
        // Показать экран загрузки
        loadingScreen.SetActive(true);
        loadingText.text = "Загрузка...";

        // Начать загрузку данных с сервера
        StartCoroutine(LoadDataFromServer());
    }

    private IEnumerator LoadDataFromServer()
    {
        // Выполнить запрос к серверу
        l = newTcpClient.TcpClientExample("127.0.0.1");
        // Получить данные с сервера
        data = l.GetData()
        // Загрузка завершена
        loadingText.text = "Загрузка завершена!";
        yield return new WaitForSeconds(1f);

        // Скрыть экран загрузки
        loadingScreen.SetActive(false);

        // Продолжить сцену или выполнить другие действия после загрузки
    }
}