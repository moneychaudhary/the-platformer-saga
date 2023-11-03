using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

public class GoogleFormUploader : MonoBehaviour
{
    [SerializeField] private string googleFormURL; // Paste the Google Form URL here

    public int dissapearCount = 0;

    public void RecordData(string dataToRecord,int analyticAbilty)
    {
        StartCoroutine(UploadData(dataToRecord, analyticAbilty));
    }

    IEnumerator UploadData(string dataToRecord, int analyticAbility)
    {
        WWWForm form = new WWWForm();
        form.AddField(dataToRecord, analyticAbility.ToString());

        UnityWebRequest www = UnityWebRequest.Post(googleFormURL, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Failed to upload data: " + www.error);
        }
        else
        {
            Debug.Log("Data uploaded successfully!");
        }
    }
}

