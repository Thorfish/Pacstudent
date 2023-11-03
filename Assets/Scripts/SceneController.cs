using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void ExitLevel()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
    public void GoToLevel()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }
}
