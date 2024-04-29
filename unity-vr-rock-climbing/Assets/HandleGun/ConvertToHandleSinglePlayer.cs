using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode.Components;
using Unity.Netcode;
using Random = System.Random;

public class ConvertToHandleSinglePlayer : MonoBehaviour
{


	public GameObject[] handles;
	private Random rd;
	public int bulletOwner = 0;
	//public Collider scoreHandler;

	void Start()
	{
		rd = new Random();
	}

    void OnCollisionEnter(Collision col)
	{
		if(col.gameObject.tag == "Climbable")
		{
			//Debug.Log("Bullet Hit a wall");
			//scoreHandler.GetComponent<ScoreHandler>().HandleLaunched(bulletOwner);
			ContactPoint contact = col.contacts[0];
			//Quaternion rotation = Quaternion.FromToRotation(Vector3.up, contact.normal);
			//Transform position = contact.point;
			//Vector3 colPos = contact.point;
			SpawnHandle(contact.point, col.gameObject);
		}
	}

	private void SpawnHandle( Vector3 colPos, GameObject parent)
	{
		int randInd = rd.Next(handles.Length);
		GameObject handle = handles[randInd];	
		GameObject spawnedHandle = Instantiate(handle);//, parent.transform, false);
		//spawnedHandle.transform.position =  col.transform.position;
		spawnedHandle.transform.position =  colPos;
		//Instantiate(handle, position, rotation);
		Destroy(gameObject);		
	}
}
