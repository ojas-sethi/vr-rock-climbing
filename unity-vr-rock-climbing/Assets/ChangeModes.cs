using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeModes : MonoBehaviour
{
    public GameObject Gun;

    // Start is called before the first frame update
    public void setToSinglePlayer()
    {
        Gun.GetComponent<FireBulletOnActivate>().Shared_net.Value = false;
    }

    public void setToMultiPlayer()
    {
        Gun.GetComponent<FireBulletOnActivate>().Shared_net.Value = true;
    }

}
