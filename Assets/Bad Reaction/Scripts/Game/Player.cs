using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public static UnityEvent<int, bool> PlayerEvent = new UnityEvent<int, bool> ();

    private Transform thisTransform;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rigidBody2D;
    private AudioSource audioSource;
    private GameResources resources;
    private ParticleSystem explosionParticles;
    private ParticleSystem trailParticles;

    private int touchCounter;
    private bool gameStarted;
    private float speedBoost;
    private float delayMenu;
    private float movementSpeed;
    private Vector3 defaultPosition;
    private Vector3 movementDirection;

    private void Start()
    {
        thisTransform = GetComponent<Transform>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        resources = Resources.Load<GameResources>("ResourcesContainer");
        explosionParticles = thisTransform.Find("ExplosionParticles").gameObject.GetComponent<ParticleSystem>();
        trailParticles = thisTransform.Find("TrailParticles").gameObject.GetComponent<ParticleSystem>();

        defaultPosition = thisTransform.position;
        speedBoost = resources.PlayerSpeedBoostValue;
        delayMenu = resources.DelayBeforeOpenMainMenu;

        Canvas.StartGameEvent.AddListener(StartGame);
    }

    private void StartGame()
    {
        spriteRenderer.enabled = true;
        rigidBody2D.bodyType = RigidbodyType2D.Dynamic;
        movementSpeed = resources.PlayerMovementSpeed;
        movementDirection = thisTransform.up * movementSpeed;
        touchCounter = 1;
        trailParticles.Play();
        gameStarted = true;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && gameStarted)
        {
            touchCounter++;

            switch (touchCounter)
            {
                case 1:
                    movementDirection = thisTransform.up * movementSpeed;
                    break;
                case 2:
                    movementDirection = thisTransform.right * movementSpeed;
                    break;
                case 3:
                    movementDirection = thisTransform.up * -movementSpeed;
                    break;
                case 4:
                    movementDirection = thisTransform.right * -movementSpeed;
                    break;
            }

            if(touchCounter >= 4) { touchCounter = 0; }
        }
    }

    private void FixedUpdate()
    {
        if (gameStarted) { rigidBody2D.velocity = movementDirection; }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Wall":
                spriteRenderer.enabled = false;
                trailParticles.Stop();
                explosionParticles.Play();
                movementDirection = Vector3.zero;
                rigidBody2D.bodyType = RigidbodyType2D.Static;
                gameStarted = false;
                StartCoroutine(Delay());
                break;
            case "Cristal":
                Cristal cristal = collision.gameObject.GetComponent<Cristal>();
                cristal.GetCristal();
                PlayerEvent.Invoke(1, false);
                movementSpeed += speedBoost;
                break;
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(delayMenu);
        thisTransform.position = defaultPosition;
        PlayerEvent.Invoke(0, true);
    }
}
