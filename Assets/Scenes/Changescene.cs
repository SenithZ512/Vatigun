using UnityEngine;
using UnityEngine.SceneManagement;

public class Changescene : MonoBehaviour
{
    public void warp(int index)
    {
        SceneManager.LoadScene(index);
    }
}
