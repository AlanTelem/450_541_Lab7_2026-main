using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerPowerUps : MonoBehaviour
{
    [Header("Power Up Durations")]
    [SerializeField] private float speedPowerUpDuration = 5f;
    [SerializeField] private float jumpPowerUpDuration = 30f;
    [SerializeField] private float pointPowerUpDuration = 10f;
    [Header("Power Up Effect Amplitudes")]
    [SerializeField] private float speedUpMultiplier = 1.5f;
    [SerializeField] private float scoreMultiplier = 2f;

    public float SpeedTimeLeft { get; private set; }
    public float JumpTimeLeft { get; private set; }
    public float ScoreTimeLeft { get; private set; }
    private PlayerMovement movement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }

    public void ActivateSpeedBoost()
    {
        SpeedTimeLeft = speedPowerUpDuration;
        movement.SpeedMultiplier = speedUpMultiplier;
    }

    public void ActivateJumpBoost()
    {
        JumpTimeLeft = jumpPowerUpDuration;
        movement.HasDoubleJumpPowerUp = true;
    }

    public void ActivateScoreBoost()
    {
        ScoreTimeLeft = pointPowerUpDuration;
        ScoreManager.Instance.scoreMultiplier =2f;
    }

    // Update is called once per frame
    void Update()
    {
        if (SpeedTimeLeft > 0)
        {
            SpeedTimeLeft -= Time.deltaTime;
            if (SpeedTimeLeft <= 0) movement.SpeedMultiplier = 1f;
        }

        if (JumpTimeLeft > 0)
        {
            JumpTimeLeft -= Time.deltaTime;
            if (JumpTimeLeft <= 0) movement.HasDoubleJumpPowerUp = false;
        }

        if (ScoreTimeLeft > 0) 
        {
            ScoreTimeLeft -= Time.deltaTime;
            if (ScoreTimeLeft <= 0) ScoreManager.Instance.scoreMultiplier = 1f;
        }
    }
}
