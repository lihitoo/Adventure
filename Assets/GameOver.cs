using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // Start is called before the first frame update
    public Canvas canvas;
    public void Restart()
    {
        SceneManager.LoadScene("GameplayScene");
        Time.timeScale = 1;
    }
}
