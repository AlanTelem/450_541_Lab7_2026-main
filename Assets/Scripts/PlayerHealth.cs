using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static PlayerHealth Instance {get; private set;}
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float invulnerabilityDuration = 1f;
    private float invulnerabilityTimer = 0f;
    public int CurrentHealth {get; private set;}
    //consider adding damage multiplier

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        CurrentHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if(invulnerabilityTimer > 0f) invulnerabilityTimer -=Time.deltaTime;
    }

    public void TakeDamage(int damageAmount)
    {
        if(invulnerabilityTimer > 0f) return;

        CurrentHealth -= damageAmount;
        invulnerabilityTimer = invulnerabilityDuration;
        Debug.Log("Player health: " + CurrentHealth);
        
        if (CurrentHealth <= 0) Die();
    }

    private void Die()
    {
        Debug.Log("You are dead, act accordingly.");
        if (ScoreManager.Instance != null) ScoreManager.Instance.RestartLevelScore();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
