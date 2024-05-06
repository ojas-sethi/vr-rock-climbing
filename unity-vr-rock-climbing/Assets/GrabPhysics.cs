using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class GrabPhysics : MonoBehaviour
{
    public InputActionProperty grabInputSource;
    public float radius = 0.1f;
    public LayerMask grabLayer;

    private FixedJoint fixedJoint;
    private bool isGrabbing = false;
    private GameObject closestHandle;


    // -------------------------sfx
    public float minDownwardSpeed = 5f;
    public AudioSource audioSource;
    public List<AudioClip> soundEffects = new List<AudioClip>();

    private Rigidbody rb;
    private bool isMovingDownward = false;
    public float minDelayBetweenEffects = 1f; // Minimum delay between playing effects
    private bool canPlayEffect = true; // Flag to check if an effect can be played

    // --------------------/sfx

    // Update is called once per frame
    void FixedUpdate()
    {
        bool isGrabButtonPressed = grabInputSource.action.ReadValue<float>() > 0.1f;

        if(isGrabButtonPressed && !isGrabbing)
        {
            Collider[] nearbyColliders = Physics.OverlapSphere(transform.position, radius, grabLayer, QueryTriggerInteraction.Ignore);

            if(nearbyColliders.Length > 0)
            {
                Rigidbody nearbyRigidbody = nearbyColliders[0].attachedRigidbody;

                fixedJoint = gameObject.AddComponent<FixedJoint>();
                fixedJoint.autoConfigureConnectedAnchor = false;

                if(nearbyRigidbody)
                {
                    fixedJoint.connectedBody = nearbyRigidbody;
                    fixedJoint.connectedAnchor = nearbyRigidbody.transform.InverseTransformPoint(transform.position);
                }
                else
                {
                    fixedJoint.connectedAnchor = transform.position;
                }

                closestHandle = nearbyColliders[0].gameObject;
                closestHandle.GetComponent<ChangeColor>().Grabbed();

                isGrabbing = true;
            }
        }
        else if(!isGrabButtonPressed && isGrabbing)
        {
            isGrabbing = false;

            if(fixedJoint)
            {
                closestHandle.GetComponent<ChangeColor>().UnGrabbed();
                Destroy(fixedJoint);
            }
        }
    }

//------------------------sfx
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (rb.velocity.y < -minDownwardSpeed && !isMovingDownward && canPlayEffect && isGrabbing)
        {
            isMovingDownward = true;
            StartCoroutine(PlayRandomSoundWithDelay());
        }
        else if (rb.velocity.y >= -minDownwardSpeed && isMovingDownward)
        {
            isMovingDownward = false;
        }
    }

    IEnumerator PlayRandomSoundWithDelay()
    {
        if (soundEffects.Count > 0)
        {
            int randomIndex = Random.Range(0, soundEffects.Count);
            audioSource.PlayOneShot(soundEffects[randomIndex]);
        }

        canPlayEffect = false; // Set flag to prevent playing effects until the delay is over
        yield return new WaitForSeconds(minDelayBetweenEffects);
        canPlayEffect = true; // Reset flag to allow playing effects again
    }
//---------------------/sfx
}
