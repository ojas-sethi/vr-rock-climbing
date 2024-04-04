using UnityEngine;

public class CoinCollector : MonoBehaviour
{
    public int coinsCollected = 0;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            coinsCollected++;
            Destroy(other.gameObject); 
            // TODO: Add some sound and UI feedback to tell the user
        }
    }
}