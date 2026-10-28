using UnityEngine;

public class PauseScreen : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool isPaused = false;
    private bool resumeRequested = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Canvas canvasChild=GetComponentInChildren<Canvas>(true);
        if(canvasChild != null)
        {
            pauseMenu = canvasChild.gameObject;
            pauseMenu.SetActive(isPaused);
        } else
        {
            Debug.LogError("Canvas child not found.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Cancel")) TogglePause();
        //this block is to make sure the player doesn't roll after clicking resume
        if (resumeRequested)
        {
            if (Input.GetButtonUp("Fire1"))
            {
                resumeRequested = false;
                TogglePause();
            }
        }
    }

    public void RequestResume()
    {
        resumeRequested = true;
    }

    public void TogglePause(){
        if(pauseMenu ==null) return;
        isPaused = !isPaused;
        pauseMenu.SetActive(isPaused);
        if (isPaused)
        {
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } 
        else
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
