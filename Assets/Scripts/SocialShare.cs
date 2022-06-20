using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;

public class SocialShare : MonoBehaviour
{
	[SerializeField] private Button ShareScoreButton;
	private string imagePath;

    private void Start()
    {
		ShareScoreButton.onClick.AddListener(()=> StartCoroutine(ShareSroce()));
		Debug.Log(Application.temporaryCachePath);
	}

    private IEnumerator ShareSroce()
    {
		StartCoroutine(TakeScreenShot());

		yield return new WaitUntil(() => File.Exists(imagePath));
		new NativeShare()
			.AddFile(imagePath)
			.SetSubject("This is My Score")
			.SetText("Share Your Score With Your Friends")
			.Share();
	}

	IEnumerator TakeScreenShot()
	{
		yield return new WaitForEndOfFrame();

		Texture2D destinationTexture = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);

		Rect regionToReadFrom = new Rect(0, 0, Screen.width, Screen.height);
		int xPosToWriteTo = 0;
		int yPosToWriteTo = 0;
		bool updateMipMapsAutomatically = false;

		destinationTexture.ReadPixels(regionToReadFrom, xPosToWriteTo, yPosToWriteTo, updateMipMapsAutomatically);
		destinationTexture.Apply();

		
		imagePath = Path.Combine(Application.temporaryCachePath, "sharedImage.png");
		File.WriteAllBytes(imagePath, destinationTexture.EncodeToPNG());

		Destroy(destinationTexture);
	}

}