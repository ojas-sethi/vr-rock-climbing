using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using System.Collections.Generic;

public class GruntSounds : MonoBehaviour
{
    public float minDownwardSpeed = 5f;
    public AudioSource audioSource;
    public List<AudioClip> soundEffects = new List<AudioClip>();

    private Rigidbody rb;
    private bool isMovingDownward = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (rb.velocity.y < -minDownwardSpeed && !isMovingDownward)
        {
            isMovingDownward = true;
            PlayRandomSoundEffect();
        }
        else if (rb.velocity.y >= -minDownwardSpeed && isMovingDownward)
        {
            isMovingDownward = false;
        }
    }

    void PlayRandomSoundEffect()
    {
        if (soundEffects.Count > 0)
        {
            int randomIndex = Random.Range(0, soundEffects.Count);
            audioSource.PlayOneShot(soundEffects[randomIndex]);
        }
    }
}
