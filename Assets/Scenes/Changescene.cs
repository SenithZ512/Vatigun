using UnityEngine;
using UnityEngine.SceneManagement;

public class Changescene : MonoBehaviour
{
    public void WarpToMenu(int index)
    {
        SceneManager.LoadScene(index);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void WarpToGame(int index)
    {
        SceneManager.LoadScene(index);
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}

