using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YG;

public class InterfaceController:MonoBehaviour
{
    public static InterfaceController Singleton;

    [SerializeField] private GameResources resources;
    [Space(25)]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Animator animator;
    [SerializeField] private Image soundsButton;
    [Space(25)]
    [SerializeField] private GameObject tutorialText;
    [SerializeField] private Text scoreCounterText;
    [SerializeField] private Text lastResultText;
    [SerializeField] private Text bestResultText;

    private AudioSource[] allAudioSources = new AudioSource[0];
    private int currentScore;


    private void OnEnable()
    {
        YandexGame.GetDataEvent += GetLoad;

        Player.GetScoreEvent += ScoreCounter;
        Player.PlayerDeadEvent += PlayerDead;
    }

    private void OnDisable()
    {
        YandexGame.GetDataEvent -= GetLoad;

        Player.GetScoreEvent += ScoreCounter;
        Player.PlayerDeadEvent += PlayerDead;
    }

    private void Start()
    {
        allAudioSources = GameObject.FindObjectsOfType(typeof(AudioSource)) as AudioSource[];
        Singleton = this;
        GetLoad();
    }

    private void GetLoad()
    {
        if (YandexGame.savesData.saved_SoundsON)
        {            
            foreach(AudioSource aus in allAudioSources)
            {
                aus.volume = 1f;
            }

            soundsButton.sprite = resources.Sounds_on;
        }
        else
        {          
            foreach (AudioSource aus in allAudioSources)
            {
                aus.volume = 0f;
            }

            soundsButton.sprite = resources.Sounds_off;
        }
    }

    public void btn_Start()
    {
        audioSource.PlayOneShot(resources.ButtonSound);
        animator.SetTrigger("StartMenuHide");
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().StartGame();
    }

    public void btn_Restar()
    {
        currentScore = 0;
        scoreCounterText.text = 0.ToString();
        audioSource.PlayOneShot(resources.ButtonSound);
        animator.SetTrigger("MainMenuHide");
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().StartGame();
    }

    public void btn_Sounds()
    {
        if (YandexGame.savesData.saved_SoundsON)
        {
            foreach (AudioSource aus in allAudioSources)
            {
                aus.volume = 0f;
            }

            soundsButton.sprite = resources.Sounds_off;
            YandexGame.savesData.saved_SoundsON = false;
        }
        else
        {
            foreach (AudioSource aus in allAudioSources)
            {
                aus.volume = 1f;
            }

            soundsButton.sprite = resources.Sounds_on;
            YandexGame.savesData.saved_SoundsON = true;
        }

        audioSource.PlayOneShot(resources.ButtonSound);
        YandexGame.SaveProgress();
    }

    private void ScoreCounter()
    {
        currentScore++;
        scoreCounterText.text = currentScore.ToString();
    }

    private void PlayerDead()
    {
        YandexGame.FullscreenShow();
        animator.SetTrigger("MainMenuShow");
        lastResultText.text = currentScore.ToString();

        if (currentScore > YandexGame.savesData.bestSavedResult)
        {
            bestResultText.text = currentScore.ToString();
            audioSource.PlayOneShot(resources.NewBesResult); 
            YandexGame.savesData.bestSavedResult = currentScore;
            YandexGame.SaveProgress();
        }
        else
        {
            bestResultText.text = YandexGame.savesData.bestSavedResult.ToString();
        }
    }
}