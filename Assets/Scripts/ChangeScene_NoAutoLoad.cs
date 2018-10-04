using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class ChangeScene_NoAutoLoad : MonoBehaviour {

    AsyncOperation sceneAO;
    public float loadProgress;

    public bool DEBUG_LoadScene;

    [SerializeField]
    private string loadSceneString;

    // the actual percentage while scene is fully loaded
    private const float LOAD_READY_PERCENTAGE = 0.9f;

    private void Start()
    {
        if (DEBUG_LoadScene)
            StartCoroutine(LoadingSceneRealProgress(loadSceneString));
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
            loadProgress = sceneAO.progress;

            if (sceneAO.progress >= LOAD_READY_PERCENTAGE)
            {
                loadProgress = 1f;
            }
            Debug.Log(sceneAO.progress);
            yield return null;
        }
    }
}
