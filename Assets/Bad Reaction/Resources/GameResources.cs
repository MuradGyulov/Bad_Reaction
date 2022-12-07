using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu (fileName = "ResourcesContainer")]

public class GameResources : ScriptableObject
{
    [SerializeField] private float playerMovementSpeed;
    [SerializeField] private float playerSpeedBoostValue;
    [SerializeField] private float delayBeforeOpenMainMenu;
    [SerializeField] private float menuHiddingSpeed;
    [Space(20)]
    [SerializeField] private AudioClip buttonsSound;
    [SerializeField] private AudioClip cristalSound;
    [SerializeField] private AudioClip turningSound;
    [SerializeField] private AudioClip newBestResult;
    [SerializeField] private AudioClip crashSound;
    [Space(20)]
    [SerializeField] private Sprite sounds_off;
    [SerializeField] private Sprite sounds_on;


    public AudioClip ButtonSound { get => buttonsSound; }
    public AudioClip CristalSound { get => cristalSound; }
    public AudioClip TurningSound { get => turningSound; }
    public AudioClip NewBesResult { get => newBestResult; }
    public AudioClip CrashSound { get => crashSound; }

    public float PlayerMovementSpeed { get => playerMovementSpeed; }
    public float PlayerSpeedBoostValue { get => playerSpeedBoostValue; }
    public float DelayBeforeOpenMainMenu { get => delayBeforeOpenMainMenu; }
    public float MenuHiddingSpeed { get => menuHiddingSpeed; }
    public Sprite Sounds_off { get => sounds_off; }
    public Sprite Sounds_on { get => sounds_on; }
}
