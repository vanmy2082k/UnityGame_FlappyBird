using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private GameObject GameMenuPannel;
    public Button MenuPannelButton;
    [SerializeField] private TextMeshProUGUI CoutDownTime;

    [SerializeField] private Button RemuseButton;
    [SerializeField] private Button RestartButton;
    [SerializeField] private Button HomeButton;


    private void Start()
    {
        GameMenuPannel.SetActive(false);
        CoutDownTime.enabled = false;
    }

    private void OnEnable()
    {

        MenuPannelButton.onClick.AddListener(() => StartCoroutine(PauseOrRemuse()));
        RemuseButton.onClick.AddListener(() => StartCoroutine(PauseOrRemuse()));
        RestartButton.onClick.AddListener(Restart);    
        HomeButton.onClick.AddListener(Home);


    }

    private void Home()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0);
    }

    private IEnumerator PauseOrRemuse()
    {
        if (!GameMenuPannel.activeInHierarchy)
        {
            Time.timeScale = 0f;
            GameMenuPannel.SetActive(true);
            MenuPannelButton.gameObject.SetActive(false);
        }
        else
        {
            var delayTime = 4f;
            var timeStart = 1f;

            GameMenuPannel.SetActive(false);
            MenuPannelButton.gameObject.SetActive(true);

            while(timeStart <= delayTime)
            {
                CoutDownTime.enabled = true;
                CoutDownTime.text = $"{(int)delayTime}";
                delayTime -= Time.unscaledDeltaTime;
                yield return null;
            }

            Time.timeScale = 1f;
            CoutDownTime.enabled = false;
        }
    }
    private void Restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
