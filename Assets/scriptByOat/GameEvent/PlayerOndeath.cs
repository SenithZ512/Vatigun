using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerOndeath : MonoBehaviour, IOndeath
{
    public void OnDeath()
    {
        SceneManager.LoadScene(1);
    }
}
