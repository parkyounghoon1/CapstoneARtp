using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransManager : Singleton<SceneTransManager>
{
    private AsyncOperation sceneAsync;

    IEnumerator LoadScene(string sceneName, List<GameObject> objectsToMove)
    {
        SceneManager.LoadSceneAsync(sceneName);
        SceneManager.sceneLoaded += (newScene, mode) => {
            SceneManager.SetActiveScene(newScene);
            };
        Scene sceneToLoad = SceneManager.GetSceneByName(sceneName);
        foreach (GameObject obj in objectsToMove)
        {
            SceneManager.MoveGameObjectToScene(obj, sceneToLoad);
        }

        yield return null;
    }

    public void GoToScene(string sceneName, List<GameObject> objectsToMove)
    {
        StartCoroutine(LoadScene(sceneName, objectsToMove));
    }
}
