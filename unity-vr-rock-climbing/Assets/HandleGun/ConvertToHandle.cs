using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode.Components;
using Unity.Netcode;

public class ConvertToHandle : NetworkBehaviour
{
	public GameObject handle;
    void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "Climbable")
		{
			Debug.Log("Bullet Hit a wall");
			ContactPoint contact = col.contacts[0];
			//Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
			//Transform position = contact.point;
			//Vector3 colPos = contact.point;
			if(IsOwner)
			{
				SpawnHandleServerRpc(contact.point);
			}
		}
	}

	[ServerRpc]
	private void SpawnHandleServerRpc( Vector3 colPos)
	{
		GameObject spawnedHandle = Instantiate(handle);
		//spawnedHandle.transform.position =  col.transform.position;
		spawnedHandle.transform.position =  colPos;
		//Instantiate(handle, position, rotation);
		Destroy(gameObject);		
		var instanceNetworkObject = spawnedHandle.GetComponent<NetworkObject>();
		instanceNetworkObject.Spawn();
	}
}
