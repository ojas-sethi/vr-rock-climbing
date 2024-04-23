using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using Unity.Netcode.Components;
using Unity.Netcode;

public class FireBulletOnActivate : NetworkBehaviour
{
	public GameObject bullet;
	public Transform spawnPoint;
	public float fireSpeed = 20;
	public InputActionProperty fire;
	
    // Start is called before the first frame update
    void Start()
    {
		//XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
		//gun.activated.AddListener(FireBullet);
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner && fire.action.WasPressedThisFrame()) {
			FireBulletServerRpc(spawnPoint.position, spawnPoint.forward);
		}
    }
	
	//public void FireBullet()//ActivateEventArgs arg)
	//{
	//	GameObject spawnedBullet = Instantiate(bullet);
	//	spawnedBullet.transform.position = spawnPoint.position;
	//	spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
	//	Destroy(spawnedBullet, 5);
	//}

	[ServerRpc(RequireOwnership = false)]
	private void FireBulletServerRpc(Vector3 bulletPosition, Vector3 gunForward)
	{
		GameObject spawnedBullet = Instantiate(bullet);
		spawnedBullet.transform.position = bulletPosition;
		spawnedBullet.GetComponent<Rigidbody>().isKinematic = false;
		spawnedBullet.GetComponent<Rigidbody>().velocity = gunForward * fireSpeed;
		Destroy(spawnedBullet, 5);
		//Destroy(gameObject);		
		var instanceNetworkObject = spawnedBullet.GetComponent<NetworkObject>();
		instanceNetworkObject.Spawn();
	}
}
