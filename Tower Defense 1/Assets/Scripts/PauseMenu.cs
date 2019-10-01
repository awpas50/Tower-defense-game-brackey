using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public GameObject UI;
    void Awake()
    {
        UI.SetActive(false);
    }
    private void Update()
    {
        if ( (Input.GetKeyDown(KeyCode.Escape)) || (Input.GetKeyDown(KeyCode.P)) ) {
            TogglePauseMenu();
        }
    }

    public void TogglePauseMenu()
    {
        // inverse the state
        UI.SetActive(!UI.activeSelf);
        if (UI.activeSelf)
        {
            //freeze the game & freeze fixedDeltaTime
            Time.timeScale = 0f;
            // don't need to adjust fixedDeltaTime
            //Time.fixedDeltaTime;
        }
        else
        {
            Time.timeScale = 1f;
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        TogglePauseMenu();
    }

    public void Menu()
    {

    }
}
