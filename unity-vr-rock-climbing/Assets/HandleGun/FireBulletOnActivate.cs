using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;
using Unity.Netcode.Components;
using Unity.Netcode;

public class FireBulletOnActivate : NetworkBehaviour
{
	public GameObject mBullet;
	public GameObject sBullet;
	public Transform spawnPoint;
	public float fireSpeed = 20;
	public InputActionProperty fire;
	public bool Shared = true;
	public NetworkVariable<bool> Shared_net = new NetworkVariable<bool>();
	public AudioSource sfx;
	
    // Start is called before the first frame update
    void Start()
    {
		//XRGrabInteractable grabbable = GetComponent<XRGrabInteractable>();
		//gun.activated.AddListener(FireBullet);
        Shared_net.Value = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsOwner && fire.action.WasPressedThisFrame()) {
			//GetComponent<hapticOnFire>().fireHaptic();
			Debug.Log(Shared);
			if (Shared_net.Value)
			{
				FireBulletServerRpc(spawnPoint.position, spawnPoint.forward);
			}
			else
			{
				FireBullet(spawnPoint.position, spawnPoint.forward);
			}
			
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
	private void FireBulletServerRpc(Vector3 bulletPosition, Vector3 gunForward)//, ServerRpcParams serverRpcParams = default)
	{
		//var clientId = serverRpcParams.Receive.SenderClientId;
		//Debug.Log(clientId);
		GameObject spawnedBullet = Instantiate(mBullet);
		spawnedBullet.transform.position = bulletPosition;
		//spawnedBullet.GetComponent<ConvertToHandle>().bulletOwner = (int) clientId;
		spawnedBullet.GetComponent<Rigidbody>().isKinematic = false;
		spawnedBullet.GetComponent<Rigidbody>().velocity = gunForward * fireSpeed;
		Destroy(spawnedBullet, 5);
		//Destroy(gameObject);		
		var instanceNetworkObject = spawnedBullet.GetComponent<NetworkObject>();
		instanceNetworkObject.Spawn();
	}

	private void FireBullet(Vector3 bulletPosition, Vector3 gunForward)
	{
		GameObject spawnedBullet = Instantiate(sBullet);
		spawnedBullet.transform.position = bulletPosition;
		spawnedBullet.GetComponent<Rigidbody>().isKinematic = false;
		spawnedBullet.GetComponent<Rigidbody>().velocity = gunForward * fireSpeed;
		if (sfx != null)
        {
            sfx.Play();
        }
		Destroy(spawnedBullet, 5);
		//Destroy(gameObject);		
	}

	public void changeMode(bool mode)
	{
		Shared = mode;
	}
}
