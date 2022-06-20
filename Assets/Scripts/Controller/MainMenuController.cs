using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //inport de su dung load scence

public class MainMenuController : MonoBehaviour {
   // public GameObject CreditUI;
    [SerializeField] GameObject fateIn, fateOut;
    [SerializeField] GameObject panelLoadding;
    [SerializeField] GameObject birdFilyMenu;
    [SerializeField] GameObject panelExit;
    [SerializeField] GameObject panelInfor;
    private void Start()
    {
        //load sound
        SoundControll.instance.onSound(PlayerPrefsControll.getSound());
        SoundControll.instance._stopSoundBird();
        fateOut.SetActive(false);

        SoundControll.instance._stopSound(3);

        if (!Util.checkFirtRun)
        {
            Util.checkFirtRun = true;
            StartCoroutine(delayLoadding());
        }
        else
        {
            StartCoroutine(delayShowMainMenu());
        }
    }

    //thoat game
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            panelExit.SetActive(true);
        }
        
    }

    IEnumerator delayLoadScene(string namescene)
    {
        fateOut.SetActive(true);
        yield return new WaitForSeconds(.2f);
        SceneManager.LoadScene(namescene);
    }
    IEnumerator delayLoadding()
    {
        fateIn.SetActive(true);
        panelLoadding.SetActive(true);
        yield return new WaitForSeconds(4.1f);
        fateIn.SetActive(false);
        panelLoadding.SetActive(false);
        StartCoroutine(delayShowMainMenu());
    }
    IEnumerator delayShowMainMenu()
    {
        fateIn.SetActive(true);
        yield return new WaitForSeconds(.2f);
        fateIn.SetActive(false);
        birdFilyMenu.SetActive(true);
    }
    public void btnEasyClick()
    {
        //MyGoogleMobileAdVideo.instance.showRewarBaseAd();
        StartCoroutine(delayLoadScene("Level Easy"));
    }
    public void btnNormalClick()
    {
        StartCoroutine(delayLoadScene("Level Normal"));
    }
    public void btnHardClick()
    {
        StartCoroutine(delayLoadScene("Level Hard"));
    }

    public void btnYesExitClick()
    {
        StartCoroutine(delayLoadScene("Thank"));
    }
    public void btnNoExitClick()
    {
        panelExit.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void openInforClick()
    {
        panelInfor.SetActive(true);
    }
    public void closeInforClick()
    {
        panelInfor.SetActive(false);
    }
}
