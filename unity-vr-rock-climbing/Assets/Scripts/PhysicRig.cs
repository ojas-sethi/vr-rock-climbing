using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicRig : MonoBehaviour
{
    public Transform playerHead;
    public Transform leftController;
    public Transform rightController;

    public ConfigurableJoint headJoint;
    public ConfigurableJoint leftHandJoint;
    public ConfigurableJoint rightHandJoint;

    public CapsuleCollider bodyCollider;

    public float bodyHeightMin = 0.5f;
    public float bodyHeightMax = 2;

    private float countDown = 100;
    private float countUp = 20;
    public LayerMask groundLayer;
    private bool isGrounded = true;
    private int g_count = 200;
    private float lerp_per = 0.1f;
    private float height_goal;
    private Vector3 center_goal;

    // Update is called once per frame

    void FixedUpdate()
    {
        
        isGrounded = CheckIfGrounded();
        
        if (isGrounded)
        {
            countDown = 100;
            //bodyCollider.height = 1.2f;
            height_goal = Mathf.Clamp(playerHead.localPosition.y, bodyHeightMin, bodyHeightMax);
            bodyCollider.height = Mathf.Lerp(bodyCollider.height, height_goal, lerp_per);

            center_goal = new Vector3(playerHead.localPosition.x, bodyCollider.height / 2,
                playerHead.localPosition.z);
            bodyCollider.center = Vector3.Lerp(bodyCollider.center, center_goal, lerp_per);
        }
        else if (countDown < 10)
        {
            //countUp = 20;
            height_goal = 0.5f;
            bodyCollider.height = Mathf.Lerp(bodyCollider.height, height_goal, lerp_per);
            center_goal = new Vector3(playerHead.localPosition.x, playerHead.localPosition.y - 0.1f,
                playerHead.localPosition.z);
            bodyCollider.center = Vector3.Lerp(bodyCollider.center, center_goal, lerp_per);
        }
        //bodyCollider.height = Mathf.Clamp(playerHead.localPosition.y, bodyHeightMin, bodyHeightMax);
        //bodyCollider.center = new Vector3(playerHead.localPosition.x, bodyCollider.height / 2,
        //    playerHead.localPosition.z);

        leftHandJoint.targetPosition = leftController.localPosition;
        leftHandJoint.targetRotation = leftController.localRotation;

        rightHandJoint.targetPosition = rightController.localPosition;
        rightHandJoint.targetRotation = rightController.localRotation;

        headJoint.targetPosition = playerHead.localPosition;

        countDown = Mathf.Clamp(countDown, 0, 100) - 1;
        //countUp = Mathf.Clamp(countUp, 0, 20) - 1;
    }

    void FixedUpdateasas()
    {
        //bodyCollider.height = Mathf.Clamp(playerHead.localPosition.y, bodyHeightMin, bodyHeightMax);
        bodyCollider.height = 0.5f;
        //bodyCollider.center = new Vector3(playerHead.localPosition.x, playerHead.localPosition.y / 2,
        //    playerHead.localPosition.z);

        bodyCollider.center = new Vector3(playerHead.localPosition.x, bodyCollider.height / 2,
            playerHead.localPosition.z);

        leftHandJoint.targetPosition = leftController.localPosition;
        leftHandJoint.targetRotation = leftController.localRotation;

        rightHandJoint.targetPosition = rightController.localPosition;
        rightHandJoint.targetRotation = rightController.localRotation;

        headJoint.targetPosition = playerHead.localPosition;
    }

    void FixedUpdateIgnoe()
    {
        bodyCollider.height = Mathf.Clamp(playerHead.localPosition.y, bodyHeightMin, bodyHeightMax);
        bodyCollider.center = new Vector3(playerHead.localPosition.x, bodyCollider.height / 2,
            playerHead.localPosition.z);

        leftHandJoint.targetPosition = leftController.localPosition;
        leftHandJoint.targetRotation = leftController.localRotation;

        rightHandJoint.targetPosition = rightController.localPosition;
        rightHandJoint.targetRotation = rightController.localRotation;

        headJoint.targetPosition = playerHead.localPosition;
    }

    public bool CheckIfGrounded()
    {
        Vector3 start = bodyCollider.transform.TransformPoint(bodyCollider.center);
        float rayLength = bodyCollider.height / 2 - bodyCollider.radius + 0.05f;
        //float rayLength = 1.2f / 2 - bodyCollider.radius + 0.05f;

        bool hasHit = Physics.SphereCast(start, bodyCollider.radius, Vector3.down, out RaycastHit hitInfo, rayLength, groundLayer);

        return hasHit;
    }
}
