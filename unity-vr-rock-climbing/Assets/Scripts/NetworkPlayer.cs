using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkPlayer : NetworkBehaviour
{
	public Transform root;
	public Transform head;
	public Transform body;
	public Transform leftHand;
	public Transform rightHand;

	public float bodyYoffset;
	
	//
	public Transform gun;
	
	public Renderer[] meshToDisable;
	
    // Start is called before the first frame update
    public override void OnNetworkSpawn()
    {
		base.OnNetworkSpawn();
		if(IsOwner)
		{
			foreach (var item in meshToDisable)
			{
				item.enabled = false;
			}
		}
    }

    // Update is called once per frame
    void Update()
    {
        if(IsOwner)
		{
			root.position = VRRigReferences.Singleton.root.position;
			root.rotation = VRRigReferences.Singleton.root.rotation;
			
			head.position = VRRigReferences.Singleton.head.position;
			head.rotation = VRRigReferences.Singleton.head.rotation;

			body.position = new Vector3(VRRigReferences.Singleton.head.position.x, VRRigReferences.Singleton.head.position.y + bodyYoffset, VRRigReferences.Singleton.head.position.z);
			//body.rotation = VRRigReferences.Singleton.head.rotation;
			//body.rotation = new Quaternion(0,0, 0, VRRigReferences.Singleton.head.rotation.w);
			body.rotation = Quaternion.Euler(0, VRRigReferences.Singleton.head.eulerAngles.y,0 );
			
			leftHand.position = VRRigReferences.Singleton.leftHand.position;
			leftHand.rotation = VRRigReferences.Singleton.leftHand.rotation;
			
			rightHand.position = VRRigReferences.Singleton.rightHand.position;
			rightHand.rotation = VRRigReferences.Singleton.rightHand.rotation;
			
			gun.position = VRRigReferences.Singleton.rightHand.position;
			gun.rotation = VRRigReferences.Singleton.rightHand.rotation;
		}
    }
}
