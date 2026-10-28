using UnityEngine;

public class PowerUpPickup : MonoBehaviour
{
    [Header("Power Up Respawn Timers")]
    [SerializeField] private float respawnSpeedPowerUpTimer = 30f;
    [SerializeField] private float respawnJumpPowerUpTimer = 30f;
    [SerializeField] private float respawnScorePowerUpTimer = 30f;
    [SerializeField] private GameObject visuals;
    [SerializeField] private AudioClip pickupSound;
    [SerializeField] private GameObject pickupEffect;
    public PowerUpType type;
    private Collider triggerCollider;
    private float timer = 0f;
    private AudioSource audioSource;

    private void Awake()
    {
        visuals = transform.Find("Visuals").gameObject;
        triggerCollider = GetComponent<Collider>();
        audioSource = GetComponent<AudioSource>();
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        switch (type)
        {
            case PowerUpType.Speed: timer = respawnSpeedPowerUpTimer; break;
            case PowerUpType.Jump: timer = respawnJumpPowerUpTimer; break;
            case PowerUpType.ScoreMultiplier: timer = respawnScorePowerUpTimer; break;
        }
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
        audioSource.Play();

        // 3. Hide and schedule respawn
        visuals.SetActive(false);
        triggerCollider.enabled = false;
        Invoke(nameof(Reappear), timer);
    }

    private void Reappear()
    {
        visuals.SetActive(true);
        triggerCollider.enabled = true;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
