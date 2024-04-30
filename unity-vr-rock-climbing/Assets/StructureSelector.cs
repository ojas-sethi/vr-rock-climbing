using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode.Components;
using Unity.Netcode;


public class StructureSelector : NetworkBehaviour
{

    public GameObject[] structures;

    [Rpc(SendTo.Everyone)]
    public void ChangeStructureRpc(int structureId)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Destroyable");
		foreach(GameObject go in gos)
     		Destroy(go);
        
        foreach(GameObject go in structures)
            go.SetActive(false);
        
        structures[structureId].SetActive(true);
    }

    public void ChangeStructureLocal(int structureId)
    {
        GameObject[] gos = GameObject.FindGameObjectsWithTag("Destroyable");
		foreach(GameObject go in gos)
     		Destroy(go);
        
        foreach(GameObject go in structures)
            go.SetActive(false);
        
        structures[structureId].SetActive(true);
    }

    public void ChangeStructure(int structureId)
    {
        Debug.Log("Structure Changing");
        ChangeStructureLocal(structureId);
        ChangeStructureRpc(structureId);
    }
}
