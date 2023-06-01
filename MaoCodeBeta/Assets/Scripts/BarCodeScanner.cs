using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ZXing;
using ZXing.QrCode;
public class BarCodeScanner : MonoBehaviour
{
    public OpenGameTab openGameTab;
    public Navigation navigation;
    public RawImage cameraView;

    private WebCamTexture webCamTexture;
    public BarcodeReader barcodeReader;

    public bool TabOpened = false;

    void Start()
    {
        webCamTexture = new WebCamTexture();
        cameraView.texture = webCamTexture;
        webCamTexture.Play();
        barcodeReader = new BarcodeReader();
    }

    void Update()
    {
        if (navigation.Id[1] == 2 && TabOpened == false)
        {
            if (webCamTexture.isPlaying && webCamTexture.didUpdateThisFrame)
            {
                var barcodeResult = barcodeReader.Decode(webCamTexture.GetPixels32(), webCamTexture.width, webCamTexture.height);
                if (barcodeResult != null)
                {
                    if (barcodeResult.Text == 1011.ToString())
                    {
                       openGameTab.StartGame(PlayerPrefs.GetInt("Character"));
                        TabOpened = true;
                    }                      
                    if(barcodeResult.Text == 1012.ToString())
                    {
                        openGameTab.StartGame(2);
                        TabOpened = true;
                    }
                }
            }
        }
    }
}
