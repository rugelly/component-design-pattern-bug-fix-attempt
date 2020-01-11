using UnityEngine;

public class ReworkMenuControl : MonoBehaviour
{
    public GameObject menuobj;
    private bool menu = false;

    private void Awake()
    {
        CloseMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            menu = !menu;
            if (menu)
                OpenMenu();
            else
                CloseMenu();
        }
    }

    public void OpenMenu()
    {
        Time.timeScale = 0;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        menuobj.SetActive(true);
    }

    public void CloseMenu()
    {
        Time.timeScale = 1;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        menuobj.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
