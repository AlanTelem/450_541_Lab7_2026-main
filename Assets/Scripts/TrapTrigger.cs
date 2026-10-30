using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    [SerializeField] private int damageAmount = 1;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("I am an evil trap. Here hold this health decrement.");//this gets called in console, where's the player's debug log?
            PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
            if (playerHealth != null) playerHealth.TakeDamage(damageAmount);
        }
    }
}
