using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    public void LoadLevel(bool isReset)
    {
        if (isReset) PlayerPrefs.SetInt("CurLevel", MapGenerator.Instance.curMap);
        else PlayerPrefs.SetInt("CurLevel", MapGenerator.Instance.curMap + 1);
        // Get the current scene name
        string sceneName = SceneManager.GetActiveScene().name;

        // Load the scene with the same name
        SceneManager.LoadScene(sceneName);
    }
}
