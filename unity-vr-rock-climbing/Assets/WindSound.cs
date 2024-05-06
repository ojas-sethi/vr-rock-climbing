using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class WindSound : MonoBehaviour
{
    public Rigidbody playerRigidbody;
    public AudioClip windClip;

    private AudioSource audioSource;
    private bool isPlaying;

    // Adjust these values to control how the wind sound changes with player speed
    public float minSpeed = 1f;
    public float maxSpeed = 10f;
    public float minPitch = 0.5f;
    public float maxPitch = 1.5f;
    public float fadeSpeed = 2f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = windClip;
        audioSource.loop = true;
        audioSource.playOnAwake = false;
        isPlaying = false;
    }

    private void Update()
    {
        if (playerRigidbody == null || windClip == null)
        {
            Debug.LogWarning("Player Rigidbody or Wind Clip not assigned.");
            return;
        }

        float playerSpeed = playerRigidbody.velocity.magnitude;

        // Map player speed to pitch
        float normalizedSpeed = Mathf.InverseLerp(minSpeed, maxSpeed, playerSpeed);
        float targetPitch = Mathf.Lerp(minPitch, maxPitch, normalizedSpeed);

        // Update audio pitch
        audioSource.pitch = targetPitch;

        // Play, stop, or fade the wind sound based on player speed
        if (playerSpeed > minSpeed)
        {
            if (!isPlaying)
            {
                audioSource.volume = 0f; // Set volume to minimum before fading in
                audioSource.Play();
                isPlaying = true;
            }
            // Fade in
            audioSource.volume = Mathf.MoveTowards(audioSource.volume, 1f, fadeSpeed * Time.deltaTime);
        }
        else
        {
            if (isPlaying)
            {
                // Gradually reduce volume
                audioSource.volume = Mathf.MoveTowards(audioSource.volume, 0f, fadeSpeed * Time.deltaTime);
                if (audioSource.volume <= 0.01f)
                {
                    audioSource.Stop();
                    isPlaying = false;
                }
            }
        }
    }
}
