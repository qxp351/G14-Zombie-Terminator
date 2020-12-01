using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour
{
    private void Start()
    {
        //var async = SceneManager.LoadSceneAsync(SceneIndices.DuskScene, LoadSceneMode.Additive);
        //yield return new WaitUntil(() => async.isDone);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(SceneIndices.DuskScene));
        SceneManager.LoadScene(SceneIndices.DuskScene);
    }
}
