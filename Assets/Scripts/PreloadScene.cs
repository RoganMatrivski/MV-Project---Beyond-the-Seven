using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

using UnityEngine.UI;

using TMPro;

public class PreloadScene : MonoBehaviour {

    AsyncOperation sceneAO;

    // the actual percentage while scene is fully loaded
    private const float LOAD_READY_PERCENTAGE = 0.9f;

    private void Start()
    {
        StartCoroutine(LoadingSceneRealProgress("ConfigurationMenu"));
    }

    IEnumerator LoadingSceneRealProgress(string sceneName)
    {
        //yield return new WaitForSeconds(1);

        sceneAO = SceneManager.LoadSceneAsync(sceneName);

        // disable scene activation while loading to prevent auto load
        sceneAO.allowSceneActivation = false;

        while (!sceneAO.isDone)
        {
            Debug.Log("Progress : " + sceneAO.progress);

            if (sceneAO.progress >= LOAD_READY_PERCENTAGE)
            {
                sceneAO.allowSceneActivation = true;
            }
            Debug.Log(sceneAO.progress);
            yield return null;
        }
    }
}