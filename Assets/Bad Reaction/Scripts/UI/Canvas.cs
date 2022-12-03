using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using DG.Tweening;

public class Canvas:MonoBehaviour
{
    public static Canvas Singleton;


    private GameObject startMenu;
    private GameObject gameMenu;
    private GameObject mainMenu;
    private GameObject leadersMenu;

    public static UnityEvent StartGameEvent = new UnityEvent();


    private AudioSource audioSource;
    private GameResources resources;

    private void Start()
    {
        Singleton = this;

        audioSource = GetComponent<AudioSource>();
        resources = Resources.Load<GameResources>("ResourcesContainer");

        startMenu = transform.Find("StartMenu").gameObject;
        gameMenu = transform.Find("GameMenu").gameObject;
        mainMenu = transform.Find("MainMenu").gameObject;
        leadersMenu = transform.Find("LeadersMenu").gameObject;
    }


    private bool hided = false;

    #region START MENU FUNCTIONS    
    public void btn_Start()
    {
        StartGameEvent.Invoke();
        audioSource.PlayOneShot(resources.ButtonSound);
        startMenu.GetComponent<RectTransform>().DOAnchorPosY(2340, 0.8f, hided = true);
        print(hided);
    }
    #endregion
}
