using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ASyncLoader : MonoBehaviour
{
    [SerializeField] string scene;
    [SerializeField] Image fill;
    private void Start()
    {
        StartCoroutine(LoadLevelAsync(scene));
    }

    public IEnumerator LoadLevelAsync(string leveltoLoad)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(leveltoLoad);

        while (!loadOperation.isDone)
        {
            float progressValue = Mathf.Clamp01(loadOperation.progress / .9f);
            fill.fillAmount = progressValue;
            yield return null;
        }
    }
}
