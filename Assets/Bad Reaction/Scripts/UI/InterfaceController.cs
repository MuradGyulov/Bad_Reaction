using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using YG;

public class InterfaceController:MonoBehaviour
{
    public static InterfaceController Singleton;

    
    [SerializeField] private GameResources resources;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private Animator animator;

    [SerializeField] private Image soundsButton;

    [SerializeField] private Text scoreCounterText;
    [SerializeField] private Text lastResultText;
    [SerializeField] private Text bestResultText;

    private int currentScore;
    private int lastScore;
    private int bestScore;

    private Player player;



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
        Singleton = this;

        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        player = playerObject.GetComponent<Player>();

        GetLoad();
    }

    private void GetLoad()
    {
        if (YandexGame.savesData.saved_SoundsON)
        {
            audioSource.volume = 1;
           // soundsButton.sprite = resources.Sounds_on;
        }
        else
        {
            audioSource.volume = 0;
            //soundsButton.sprite = resources.Sounds_off;
        }
    }




    #region START MENU FUNCTIONS
    public void btn_Start()
    {
        audioSource.PlayOneShot(resources.ButtonSound);
        animator.SetTrigger("StartMenuHide");
        player.StartGame();
    }
    #endregion

    #region MAIN MENU FUNCTIONS 
    private void PlayerDead()
    {
        animator.SetTrigger("MainMenuShow");

        lastResultText.text = currentScore.ToString();

        if(currentScore >= YandexGame.savesData.bestSavedResult)
        {
            bestResultText.text = "BEST " + currentScore.ToString();
            YandexGame.savesData.bestSavedResult = currentScore;
            YandexGame.SaveProgress();
        }
    }

    public void btn_Restar()
    {
        currentScore = 0;
        scoreCounterText.text = 0.ToString();
        audioSource.PlayOneShot(resources.ButtonSound);
        animator.SetTrigger("MainMenuHide");
        player.StartGame();
    }

    public void btn_Leaders()
    {
        audioSource.PlayOneShot(resources.ButtonSound);
        animator.SetTrigger("LeadersMenuShow");
    }

    public void btn_Sounds()
    {
        if (YandexGame.savesData.saved_SoundsON)
        {
            audioSource.volume = 0;
            soundsButton.sprite = resources.Sounds_off;
            YandexGame.savesData.saved_SoundsON = false;
        }
        else
        {
            audioSource.volume = 1;
            soundsButton.sprite = resources.Sounds_on;
            YandexGame.savesData.saved_SoundsON = true;
        }

        audioSource.PlayOneShot(resources.ButtonSound);
        YandexGame.SaveProgress();
    }
    #endregion

    #region LEADERS MENU FUNCTIONS
    public void btn_Back()
    {
        audioSource.PlayOneShot(resources.ButtonSound);
        animator.SetTrigger("LeadersMenuHide");
    }
    #endregion

    #region GAME MENU FUNCTIONS
    private void ScoreCounter()
    {
        currentScore++;
        scoreCounterText.text = currentScore.ToString();
    }
    #endregion
}