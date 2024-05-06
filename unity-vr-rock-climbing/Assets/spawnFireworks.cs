using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnFireworks : MonoBehaviour
{

    public GameObject firework;

    private void OnTriggerEnter(Collider other){
        Debug.Log("Spawn Firework");
        GameObject spawnedFirework = Instantiate(firework);
        spawnedFirework.transform.position = gameObject.transform.position;
        Destroy(spawnedFirework, 10);
    }
}
