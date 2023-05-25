using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;


public class ForceAcceptAll : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        return true;
    }
}

public class LoadImage : MonoBehaviour
{

    public float requestPause = 3F;
    private float nextRequest = 0.0F;

    // Start is called before the first frame update
    void Update()
    {
        if (Time.time > nextRequest) {
            StartCoroutine(DownloadImage());
        }
    }

    IEnumerator DownloadImage()
    {

        UnityWebRequest www = UnityWebRequestTexture.GetTexture("http://127.0.0.1:8000/get_next_image");
        yield return www.SendWebRequest();

         if (www.result != UnityWebRequest.Result.Success) {
            Debug.Log(www.error);
        }
        else {
            Texture myTexture = ((DownloadHandlerTexture)www.downloadHandler).texture;
            this.gameObject.GetComponent<Renderer>().material.mainTexture = myTexture;
        }

        // var cert = new ForceAcceptAll();
        // request.certificateHandler = cert;
        // yield return request.SendWebRequest();
        // cert?.Dispose();
        // if (request.isNetworkError || request.isHttpError)
        //     Debug.Log(request.error);
        // else
        //     
    }
}