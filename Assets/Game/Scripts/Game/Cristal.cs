using UnityEngine;

public class Cristal : MonoBehaviour
{
    [SerializeField] private Transform[] pointers = new Transform[4];

    private AudioSource audioSource;
    private Animator animator;
    private Transform thisTransform;
    private GameResources resources;

    private Vector2 defaultPosition;
    private bool firstRelocate = true;


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
        audioSource.PlayOneShot(resources.CristalSound);
    }

    public void ChangePosition()
    {
        thisTransform.position = pointers[Random.Range(0, 4)].position;
    }
}
 