using UnityEngine;
using UnityEngine.SceneManagement;

// Keeps track of the player's score.
// It's a singleton: there is only ever one, reachable from any script
// through ScoreManager.Instance.
public class ScoreManager : MonoBehaviour
{
    // The single shared instance that any script can reach.
    // "static" means it belongs to the class, not to one object.
    public static ScoreManager Instance { get; private set; }

    // "static" means this value belongs to the ScoreManager CLASS,
    // not to one ScoreManager object. Objects are destroyed when a
    // new scene loads, but static values stay in memory.
    public static int FinalScore { get; private set; }
    public float ScoreMultiplier { get; set; } = 1f;

    public static int ScoreAtStartOfLevel { get; private set; }

    private void OnEnable() { SceneManager.sceneLoaded += OnSceneLoaded; } //when component is turned on, adds OnSceneLoaded so unity engine runs it every time a scene finishes loading 
    private void OnDisable() { SceneManager.sceneLoaded -= OnSceneLoaded; } //when component turns off, removes OnSceneLoaded. stops destroyed objects from being called
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (Instance != this) return;
        LevelStart();
    }

    // Called just before we leave the level
    public void SaveFinalScore()
    {
        FinalScore = CurrentScore;
    }


    // Anyone can read the score, only ScoreManager can change it
    public int CurrentScore { get; private set; }


    public void LevelStart()
    {
        ScoreAtStartOfLevel = FinalScore;
        // Debug.Log("Level loaded, score at start = " + ScoreAtStartOfLevel);//debug purposes only
    }

    public void RestartLevelScore()
    {
        CurrentScore = ScoreAtStartOfLevel;
        ScoreMultiplier = 1f;
    }
    // Awake runs before any Start(), so Instance is ready before coins need it
    private void Awake()
    {
        // If a ScoreManager already exists, remove this extra component
        // (only the script, not the whole Player)
        if (Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }


        // Otherwise, this is the one and only ScoreManager
        Instance = this;

        DontDestroyOnLoad(gameObject);
    }


    // Called by coins when they're collected
    public void AddScore(int amount)
    {
        CurrentScore += Mathf.RoundToInt(amount * ScoreMultiplier);


        // Print the new total to the Console window
        Debug.Log("Score: " + CurrentScore);
    }
}
