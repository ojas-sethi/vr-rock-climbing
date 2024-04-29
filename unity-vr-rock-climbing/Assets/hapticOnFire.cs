using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;

public class hapticOnFire : MonoBehaviour
{
    [SerializeField]
    XRBaseController rightController;

    [Range(0, 1)]
    public float intensity;
    public float duration;

    // Start is called before the first frame update

    public void fireHaptic()
    {
        rightController.SendHapticImpulse(intensity, duration);
    }
}
