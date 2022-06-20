using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePlayController : MonoBehaviour {

    public static GamePlayController instance;
    [SerializeField] GameObject panelMandem, fateOut;
    [SerializeField] GameObject btnFlap,btnPlay, dialogGameover;
    [SerializeField] GameObject scoreTxt;
    //[SerializeField] Text txtCountDown;
    [SerializeField] GameObject Player;
    [SerializeField] Transform[] tfGround;
    [SerializeField] GameObject[] objGround;
    [SerializeField] Button btnSound;
    [SerializeField] Sprite spSoundOn, spSoundOff;

    [Header("Dialog game over")]
    [SerializeField] Text txtSocerOver;
    [SerializeField] Text txtBestSocer;

    [Header("Setting level")]
    [Tooltip("Bao nhiêu điểm thì giảm khoảng cách 2 pipe")]
    public int targetPipe = 10;
    [Tooltip("Bao nhiêu điểm thì tăng thời gian sinh pipe")]
    public int targetTime = 5;
    [Tooltip("Tốc độ di chuyển bgr, pipe")]
    public float speedPipe = 2;
    [Tooltip("Thời gian delay sinh pipe")]
    public float timeDelayInstanPipe = 1.5f;

    public bool birdLive;
    public SpawnerPipe spawnerPipe;

    [Header("Effec")]
    [SerializeField] GameObject[] eff;
    [SerializeField] GameObject handClick;
    [SerializeField] GameObject panelExit;
    GameObject hand;

    public bool checkLevelEasy;
    public bool checkLevelNormal;
    public bool checkLevelHard;
    private void Start()
    {
        if (instance == null) instance = this;

        //tat man dem
        StartCoroutine(delayMandem());
        //sinh hand
        instanceHand();
        PlayerPrefsControll.IsGameStarForTheFirstTime();
        PlayerPrefsControll.setLevelPipe(0);
        SoundControll.instance._stopSound(3);
        SoundControll.instance._playSoundBird();

        btnFlap.GetComponent<Button>().onClick.AddListener(()=>Player.GetComponent<BirdControler>()._FlapBtn());
        scoreTxt.GetComponent<Text>().text = 0+"";

        //load sound
        SoundControll.instance.onSound(PlayerPrefsControll.getSound());
        if (PlayerPrefsControll.getSound())
            btnSound.GetComponent<Image>().sprite = spSoundOn;
        else
            btnSound.GetComponent<Image>().sprite = spSoundOff;
    }
    //thoat game
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            panelExit.SetActive(true);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            print("Space key was pressed");

        }
    }
    #region button
    public void btnPlayClick()
    {
        Time.timeScale = 1;
        birdLive = true;
        StopAllCoroutines();
        if(hand != null)
            Destroy(hand);
        //chay sinh cot
        StartCoroutine(spawnerPipe.Spawner());

        if(checkLevelEasy)
            Player.GetComponent<Rigidbody2D>().gravityScale = 1.8f;
        else 
        if (checkLevelNormal)
            Player.GetComponent<Rigidbody2D>().gravityScale = 2f;
        else
        if (checkLevelHard)
            Player.GetComponent<Rigidbody2D>().gravityScale = 2.2f;
        btnFlap.SetActive(true);
        btnPlay.SetActive(false);
    }

    public void btnReplayClick()
    {
        StartCoroutine(delayReplay());
    }
    public void btnSoundClick()
    {
        if (PlayerPrefsControll.getSound())
        {
            btnSound.GetComponent<Image>().sprite = spSoundOff;
            PlayerPrefsControll.setSound(0);
        }
        else
        {
            btnSound.GetComponent<Image>().sprite = spSoundOn;
            PlayerPrefsControll.setSound(1);
        }
        SoundControll.instance.onSound(PlayerPrefsControll.getSound());
    }
    public void btnHomeClick()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void btnYesExitClick()
    {
        StartCoroutine(delayLoadScene("Thank"));
    }
    public void btnNoExitClick()
    {
        panelExit.SetActive(false);
    }
    #endregion

    #region funtion
    public void _SetScore(int score)
    {
        scoreTxt.GetComponent<Text>().text = score + "";
        scoreTxt.GetComponent<Animator>().SetTrigger("add_score");

        if (checkLevelEasy)
        {
            //giảm khoảng cách giữa 2 pipe
            if (score % targetPipe == 0)
            {
                if (PlayerPrefsControll.getLevelPipe() < 4)
                    PlayerPrefsControll.setLevelPipe(PlayerPrefsControll.getLevelPipe() + 1);
                speedPipe += 0.1f;
            }
            //tăng thời gian sinh pipe
            if (score % targetTime == 0)
            {
                timeDelayInstanPipe -= 0.1f;
            }
        }
        if (checkLevelNormal)
        {
            //giảm khoảng cách giữa 2 pipe
            if (score % targetPipe == 0)
            {
                if (PlayerPrefsControll.getLevelPipe() < 2)
                    PlayerPrefsControll.setLevelPipe(PlayerPrefsControll.getLevelPipe() + 1);
                speedPipe += 0.1f;
            }
            //tăng thời gian sinh pipe
            if (score % targetTime == 0)
            {
                timeDelayInstanPipe -= 0.1f;
            }
        }

        if (checkLevelHard)
        {
            //giảm khoảng cách giữa 2 pipe
            if (score % targetPipe == 0)
            {
                if (PlayerPrefsControll.getLevelPipe() < 2)
                    PlayerPrefsControll.setLevelPipe(PlayerPrefsControll.getLevelPipe() + 1);
                speedPipe += 0.1f;
            }
            //tăng thời gian sinh pipe
            if (score % targetTime == 0)
            {
                timeDelayInstanPipe -= 0.1f;
            }
        }
    }
    public void _BirdDiedShowPanel(int score)
    {

        PlayerPrefsControll._SetScoreDie(getKeyScore(),score);

        if (btnFlap.activeInHierarchy)
            SoundControll.instance._playSound(3);

        btnFlap.SetActive(false);
        StartCoroutine(DelayShowPanel());
        if (score > PlayerPrefsControll._GetHighScore(getKeyScore()))
        {
            PlayerPrefsControll._SetHighScore(getKeyScore(),score);
        }
    }

    IEnumerator DelayShowPanel()
    {
        yield return new WaitForSeconds(0.3f);
        //Debug.Log("DelayShowPanel");
        txtSocerOver.text = PlayerPrefsControll._GetScoreDie(getKeyScore())+"";
        txtBestSocer.text = PlayerPrefsControll._GetHighScore(getKeyScore()) +"";
        dialogGameover.SetActive(true);
        FindObjectOfType<GameMenu>().MenuPannelButton.gameObject.SetActive(false);
    }
    IEnumerator delayMandem()
    {
        panelMandem.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        panelMandem.SetActive(false);
    }
    IEnumerator delayReplay()
    {
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    IEnumerator delayLoadScene(string namescene)
    {
        fateOut.SetActive(true);
        yield return new WaitForSeconds(.2f);
        SceneManager.LoadScene(namescene);
    }

    GameObject ground0;
    public void instanceGround(int id)
    {
        if (ground0 != null)
            ground0.GetComponent<SpriteRenderer>().sortingOrder = -3;

        if (id == 0)
            ground0 = Instantiate(objGround[id], tfGround[id].position, Quaternion.identity) as GameObject;
        else
            Instantiate(objGround[id], tfGround[id].position, Quaternion.identity);
    }

    public void playEff(int id,Vector2 pos)
    {
        GameObject e = Instantiate(eff[id], pos, Quaternion.identity) as GameObject;
        Destroy(e, 1.5f);
    }

    void instanceHand()
    {
        hand = Instantiate(handClick, btnPlay.transform.position, Quaternion.identity) as GameObject;
        //StartCoroutine(delayEffHand());
    }
    IEnumerator delayEffHand()
    {
        yield return new WaitForSeconds(1f);
        GameObject e = Instantiate(eff[0], btnPlay.transform.position, Quaternion.identity) as GameObject;
        Destroy(e, 1.5f);
        StartCoroutine(delayEffHand());
    }

    string getKeyScore()
    {
        if (checkLevelEasy) return Util.mapEasy;
        if (checkLevelNormal) return Util.mapNormal;
        if (checkLevelHard) return Util.mapHard;
        return "score";
    }
    #endregion
}
