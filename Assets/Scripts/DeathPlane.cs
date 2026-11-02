using UnityEngine;
using UnityEngine.SceneManagement;

public class DeathPlane : MonoBehaviour
{
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
            if (ScoreManager.Instance != null) ScoreManager.Instance.RestartLevelScore();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
