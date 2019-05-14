using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class BtnShare : MonoBehaviour
{
    public void ClickShare()
    {
        // Делает только скрин
        // StartCoroutine(TakeSSAndShare());

        //Загрузит локальную картинку
        StartCoroutine(LoadImageAndShare());
    }

    private IEnumerator TakeSSAndShare()
    {
        yield return new WaitForEndOfFrame();

        Texture2D ss = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        ss.ReadPixels(new Rect(0, 0, Screen.width, Screen.height), 0, 0);
        ss.Apply();

        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, ss.EncodeToPNG());

        // To avoid memory leaks
        Destroy(ss);

        //new NativeShare().AddFile(filePath).SetSubject("Boundless Space").SetText("Try this game too!").Share();

        new NativeShare().AddFile(filePath).Share();
    }

    private IEnumerator LoadImageAndShare()
    {
        Texture2D image = Resources.Load("Logo2", typeof(Texture2D)) as Texture2D;
        yield return null;

        string filePath = Path.Combine(Application.temporaryCachePath, "shared img.png");
        File.WriteAllBytes(filePath, image.EncodeToPNG());

        new NativeShare().AddFile(filePath).SetSubject("Boundless Space \nDownload this game").SetText("https://play.google.com/store/apps/details?id=com.BigStudent.WhiteSpace").Share();
    }
}
