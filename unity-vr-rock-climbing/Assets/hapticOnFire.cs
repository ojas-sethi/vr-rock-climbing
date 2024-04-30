using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;


public class hapticOnFire : MonoBehaviour
{
    [SerializeField]
    XRBaseController rightController;

    [Range(0, 1)]
    public float intensity;
    public float duration;

    public InputActionProperty fire;

    // Start is called before the first frame update

    void update(){
        if (fire.action.WasPressedThisFrame()) {
            Debug.Log("Haptic should be triggered");
            fireHaptic();
        }
    }

    public void fireHaptic()
    {
        rightController.SendHapticImpulse(intensity, duration);
    }
}
