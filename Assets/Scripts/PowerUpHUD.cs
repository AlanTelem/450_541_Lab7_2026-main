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
        if (powerUps == null) return;

        float speedTimeLeft = powerUps.SpeedTimeLeft;
        float jumpTimeLeft = powerUps.JumpTimeLeft;
        float scoreTimeLeft = powerUps.ScoreTimeLeft;

        if (speedIndicator != null && speedTimeText != null)
        {
            speedIndicator.SetActive(speedTimeLeft > 0f);
            speedTimeText.text = Mathf.CeilToInt(speedTimeLeft) + "s";
        }
        if (jumpIndicator != null && jumpTimeText != null)
        {
            jumpIndicator.SetActive(jumpTimeLeft > 0f);
            jumpTimeText.text = Mathf.CeilToInt(jumpTimeLeft) + "s";
        }
        if (scoreIndicator != null && scoreTimeText != null)
        {
            scoreIndicator.SetActive(scoreTimeLeft > 0f);
            scoreTimeText.text = Mathf.CeilToInt(scoreTimeLeft) + "s";
        }
    }
}
