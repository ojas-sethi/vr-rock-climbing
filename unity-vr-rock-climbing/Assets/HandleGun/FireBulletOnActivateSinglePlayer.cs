using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;


public class FireBulletOnActivateSinglePlayer : MonoBehaviour
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
        if (fire.action.WasPressedThisFrame()) {
			FireBullet(spawnPoint.position, spawnPoint.forward);
		}
    }
	
	//public void FireBullet()//ActivateEventArgs arg)
	//{
	//	GameObject spawnedBullet = Instantiate(bullet);
	//	spawnedBullet.transform.position = spawnPoint.position;
	//	spawnedBullet.GetComponent<Rigidbody>().velocity = spawnPoint.forward * fireSpeed;
	//	Destroy(spawnedBullet, 5);
	//}

	private void FireBullet(Vector3 bulletPosition, Vector3 gunForward)
	{
		GameObject spawnedBullet = Instantiate(bullet);
		spawnedBullet.transform.position = bulletPosition;
		spawnedBullet.GetComponent<Rigidbody>().isKinematic = false;
		spawnedBullet.GetComponent<Rigidbody>().velocity = gunForward * fireSpeed;
		Destroy(spawnedBullet, 5);
		//Destroy(gameObject);		
	}
}
