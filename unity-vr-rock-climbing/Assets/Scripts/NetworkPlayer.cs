using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class NetworkPlayer : NetworkBehaviour
{
	public Transform root;
<<<<<<< Updated upstream
	//public Transform head;
=======
	public Transform head;
>>>>>>> Stashed changes
	public Transform leftHand;
	public Transform rightHand;
	
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
			
<<<<<<< Updated upstream
			//head.position = VRRigReferences.Singleton.head.position;
			//head.rotation = VRRigReferences.Singleton.head.rotation;
=======
			head.position = VRRigReferences.Singleton.head.position;
			head.rotation = VRRigReferences.Singleton.head.rotation;
>>>>>>> Stashed changes
			
			leftHand.position = VRRigReferences.Singleton.leftHand.position;
			leftHand.rotation = VRRigReferences.Singleton.leftHand.rotation;
			
			rightHand.position = VRRigReferences.Singleton.rightHand.position;
			rightHand.rotation = VRRigReferences.Singleton.rightHand.rotation;
		}
    }
}
