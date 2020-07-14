using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class DatabaseConnector : MonoBehaviour
{
    const string URL = "https://winterfrost.eu/test.php";

    void Start()
    {
        StartCoroutine(GetData(URL));
    }

    IEnumerator GetData(string url)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            yield return webRequest.SendWebRequest();

            string[] pages = url.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                string data = webRequest.downloadHandler.text;

                Debug.Log(data);
            }
        }
    }
}