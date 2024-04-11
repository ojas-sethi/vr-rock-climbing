
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

    public class Button : MonoBehaviour
    {
        public Transform visualTarget;
        public Vector3 localAxis;
        public float resetSpeed = 5;
        public float followAng1eTreshold = 45;

        private bool freeze = false;

        private Vector3 initialLocalPos;

        private Vector3 offset;
        private Transform pokeAttachTransform;

        private XRBaseInteractable interactable;
        private bool isFollowing = false;

        void Start() {
            initialLocalPos = visualTarget.localPosition;
            interactable = GetComponent<XRBaseInteractable>();
            interactable.hoverEntered.AddListener(Follow);
            interactable.hoverExited.AddListener(Reset);
            interactable.selectEntered.AddListener(Freeze);
    //     _object.SetActive(false);
    // [SerializedField] GameObject _object;
        }






    public void Follow(BaseInteractionEventArgs hover)
    {

        if(hover.interactorObject is XRPokeInteractor) {
            XRPokeInteractor interactor =(XRPokeInteractor)hover.interactorObject;

            pokeAttachTransform = interactor.attachTransform;
            offset = visualTarget.position - pokeAttachTransform.position;

            float pokeAng1e = Vector3.Angle(offset, visualTarget.TransformDirection(localAxis));

            if(pokeAng1e < followAng1eTreshold) {
                isFollowing = true;
                freeze = false;

            }


        }
    }

    public void Reset(BaseInteractionEventArgs hover) {
        if(hover.interactorObject is XRPokeInteractor) {
            isFollowing = false;
            freeze = false;
        }
    }

    public void Freeze(BaseInteractionEventArgs hover) {
        if (hover.interactorObject is XRPokeInteractor) {
            freeze = true;
        }
    }

    void Update() {
        if (freeze) {
            return;
        }

        if(isFollowing) {
            Vector3 localTargetPosition = visualTarget.InverseTransformPoint(pokeAttachTransform.position + offset);
            Vector3 constrainedLocalTargetPosition = Vector3.Project(localTargetPosition,localAxis);

            visualTarget.position = visualTarget.TransformPoint(constrainedLocalTargetPosition);
        } else {
            visualTarget.localPosition = Vector3.Lerp(visualTarget.localPosition,initialLocalPos,Time.deltaTime * resetSpeed);
        }
    }


            







    }

