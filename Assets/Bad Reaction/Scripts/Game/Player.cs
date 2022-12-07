using System;
using System.Collections;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.Timeline;
using YG;
using Random = UnityEngine.Random;

public class Player : MonoBehaviour
{
    public static Action GetScoreEvent;
    public static Action PlayerDeadEvent;

    private Transform thisTransform;
    private AudioSource audioSource;
    private GameResources resources;
    private SpriteRenderer spriteRenderer;
    private ParticleSystem trailParticles;
    private ParticleSystem explosionParticles;

    private bool gameStarted;
    private float speedBoost;
    private float delayMenu;
    private float movementSpeed;

    private Vector3 defaultPosition;
    private Quaternion defaultRotationAngles;


    private void Start()
    {
        thisTransform = GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        resources = Resources.Load<GameResources>("ResourcesContainer");
        trailParticles = thisTransform.Find("TrailParticles").gameObject.GetComponent<ParticleSystem>();
        explosionParticles = thisTransform.Find("ExplosionParticles").gameObject.GetComponent<ParticleSystem>();

        defaultPosition = thisTransform.position;
        defaultRotationAngles.eulerAngles = thisTransform.eulerAngles;
        speedBoost = resources.PlayerSpeedBoostValue;
        delayMenu = resources.DelayBeforeOpenMainMenu;
    }

    public void StartGame()
    {
        if (YandexGame.savesData.saved_SoundsON)
        {
            audioSource.volume = 1;
        }
        else
        {
            audioSource.volume = 0;
        }

        movementSpeed = resources.PlayerMovementSpeed;
        spriteRenderer.enabled = true;
        trailParticles.Play();
        gameStarted = true;
    }

    private void Update()
    {
        if (gameStarted)
        {
            if (Input.GetMouseButtonDown(0))
            {
                audioSource.PlayOneShot(resources.TurningSound);
                thisTransform.Rotate(0, 0, -90f);
            }

            thisTransform.Translate(movementSpeed * Vector2.up * Time.deltaTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Wall":
                if (YandexGame.savesData.saved_SoundsON) { audioSource.PlayOneShot(resources.CrashSound); }
                spriteRenderer.enabled = false;
                explosionParticles.Play();
                trailParticles.Stop();  
                StartCoroutine(Delay());
                gameStarted = false;
                break;
            case "Cristal":
                Cristal cristal = collision.gameObject.GetComponent<Cristal>();
                GetScoreEvent?.Invoke();
                cristal.GetCristal();
                movementSpeed += speedBoost;

                if(Random.Range(0, 100) <= 60)
                {
                    thisTransform.Rotate(0, 180f, 180f);
                }
                break;
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(delayMenu);
        thisTransform.position = defaultPosition;
        PlayerDeadEvent?.Invoke();
        thisTransform.eulerAngles = Vector3.zero;
        thisTransform.eulerAngles = defaultRotationAngles.eulerAngles;
    }
}
