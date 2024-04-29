using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode.Components;
using Unity.Netcode;
using Random = System.Random;

public class ConvertToHandle : NetworkBehaviour
{
	public GameObject[] handles;
	private Random rd;
	public int bulletOwner = 0;

	void Start()
	{
		rd = new Random();
	}

    void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "Climbable")
		{
			Debug.Log("Bullet Hit a wall");
			ContactPoint contact = col.contacts[0];
			//Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
			//Transform position = contact.point;
			//Vector3 colPos = contact.point;
			if(IsServer)
			{
				SpawnHandle(contact.point);
			}
		}
	}

	private void SpawnHandle(Vector3 colPos)
	{
		int randInd = rd.Next(handles.Length);
		GameObject handle = handles[randInd];
		GameObject spawnedHandle = Instantiate(handle);
		//spawnedHandle.transform.position =  col.transform.position;
		spawnedHandle.transform.position =  colPos;
		//Instantiate(handle, position, rotation);
		Destroy(gameObject);		
		var instanceNetworkObject = spawnedHandle.GetComponent<NetworkObject>();
		instanceNetworkObject.Spawn();
	}
}
