using UnityEngine;
using TMPro;
using System; //TMP_Text
// Shows the speed boost icon and seconds left while the boost is on.
public class PowerUpHUD : MonoBehaviour
{
    [SerializeField] private PlayerPowerUps powerUps;
    [Header("Speed Boost")]
    [SerializeField] private GameObject speedIndicator; // icon + time
    [SerializeField] private TMP_Text speedTimeText;
    [Header("Jump Boost")]
    [SerializeField] private GameObject jumpIndicator;
    [SerializeField] private TMP_Text jumpTimeText;
    [Header("Score Boost")]
    [SerializeField] private GameObject scoreIndicator;
    [SerializeField] private TMP_Text scoreTimeText;
    private void Update()
    {
        float speedTimeLeft = powerUps.SpeedTimeLeft;
        float jumpTimeLeft = powerUps.JumpTimeLeft;
        float scoreTimeLeft = powerUps.ScoreTimeLeft;
 
        // Visible for the whole boost, hidden the rest of the time
        speedIndicator.SetActive(speedTimeLeft > 0f);
 
        // CeilToInt rounds 6.2 up to 7, so we never show 0s too early
        speedTimeText.text = Mathf.CeilToInt(speedTimeLeft) + "s";
        // Visible for the whole boost, hidden the rest of the time
        jumpIndicator.SetActive(jumpTimeLeft > 0f);
 
        // CeilToInt rounds 6.2 up to 7, so we never show 0s too early
        jumpTimeText.text = Mathf.CeilToInt(jumpTimeLeft) + "s";
        // Visible for the whole boost, hidden the rest of the time
        scoreIndicator.SetActive(scoreTimeLeft > 0f);
 
        // CeilToInt rounds 6.2 up to 7, so we never show 0s too early
        scoreTimeText.text = Mathf.CeilToInt(scoreTimeLeft) + "s";
    }
}
