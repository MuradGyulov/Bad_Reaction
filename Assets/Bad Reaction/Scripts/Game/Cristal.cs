using Unity.VisualScripting;
using UnityEngine;
using YG;

public class Cristal : MonoBehaviour
{
    [SerializeField] private Transform[] pointers = new Transform[4];

    private AudioSource audioSource;
    private Animator animator;
    private Transform thisTransform;
    private Vector2 defaultPosition;
    private bool firstRelocate = true;
    private GameResources resources;
    private int lastPointer;


    public void GetCristal()
    {
        if (firstRelocate)
        {
            audioSource = GetComponent<AudioSource>();
            animator = GetComponent<Animator>();
            thisTransform = GetComponent<Transform>();
            defaultPosition = transform.position;
            resources = Resources.Load<GameResources>("ResourcesContainer");
            firstRelocate = false;
        }

        animator.SetTrigger("Relocate");

        if (YandexGame.savesData.saved_SoundsON)
        {
            audioSource.PlayOneShot(resources.CristalSound);
        }
    }

    public void ChangePosition()
    {
        thisTransform.position = pointers[Random.Range(0, 4)].position;
    }
}
 