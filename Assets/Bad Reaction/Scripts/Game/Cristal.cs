using UnityEngine;

public class Cristal : MonoBehaviour
{
    [SerializeField] private Transform[] pointers = new Transform[4];

    private AudioSource audioSource;
    private Animator animator;
    private Transform thisTransform;
    private Vector2 defaultPosition;
    private bool firstRelocate = true;
    private GameResources resources;


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
        for(int i = 0; i < Random.Range(1, pointers.Length + 1); i++)
        {
            thisTransform.position = pointers[i].position;
        }
    }
}
