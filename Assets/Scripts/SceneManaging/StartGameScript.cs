using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartGameScript : MonoBehaviour
{
    private void Start()
    {
        //var async = SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
        //yield return new WaitUntil(() => async.isDone);
        //SceneManager.SetActiveScene(SceneManager.GetSceneByBuildIndex(2));
        SceneManager.LoadScene(SceneIndices.DuskScene);
    }
}
