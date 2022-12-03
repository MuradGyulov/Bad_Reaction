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
    [Space(20)]
    [SerializeField] private AudioClip buttonsSound;
    [SerializeField] private AudioClip cristalSound;
    [SerializeField] private AudioClip turningSound;
    [SerializeField] private AudioClip crashSound;


    public AudioClip CristalSound { get => cristalSound; }
    public AudioClip ButtonSound { get => buttonsSound; }

    public float PlayerMovementSpeed { get => playerMovementSpeed; }
    public float PlayerSpeedBoostValue { get => playerSpeedBoostValue; }
    public float DelayBeforeOpenMainMenu { get => delayBeforeOpenMainMenu; }
}
