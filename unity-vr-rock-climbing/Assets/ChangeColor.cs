using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColor : MonoBehaviour
{
    Renderer ren;
    public Color regularColor;
    public Color grabColor;
    // Start is called before the first frame update
    void Start()
    {
        ren = GetComponent<Renderer>();
    }

    // Update is called once per frame
    public void Grabbed()
    {
        ren.material.color = grabColor;
    }

    public void UnGrabbed()
    {
        ren.material.color = regularColor;
    }
}
