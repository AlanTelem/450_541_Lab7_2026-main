using UnityEngine;

public class PowerUpPickup : MonoBehaviour
{
    [Header("Power Up Respawn Timers")]
    [SerializeField] private float respawnSpeedPowerUpTimer = 30f;
    [SerializeField] private float respawnJumpPowerUpTimer = 30f;
    [SerializeField] private float respawnScorePowerUpTimer = 30f;
    public PowerUpType type;

    // private GameObject visuals;
    private Collider triggerCollider;

    // private AudioSource audioSource;

    private void Awake()
    {
        // Assuming your visual is a child named "Visuals"
        // visuals = transform.Find("Visuals").gameObject;
        triggerCollider = GetComponent<Collider>();
        // audioSource = GetComponent<AudioSource>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerPowerUps player = other.GetComponent<PlayerPowerUps>();
        if (player == null) return;

        // 1. Apply the effect
        switch (type)
        {
            case PowerUpType.Speed: player.ActivateSpeedBoost(); break;
            case PowerUpType.Jump:  player.ActivateJumpBoost(); break;
            case PowerUpType.ScoreMultiplier: player.ActivateScoreBoost(); break;
        }

        // 2. Play sound, spawn particles
        // audioSource.Play();

        // 3. Hide and schedule respawn
        // visuals.SetActive(false);
        triggerCollider.enabled = false;
        // Invoke(nameof(Reappear), respawnDelay);
    }

    private void Reappear()
    {
        // visuals.SetActive(true);
        triggerCollider.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
