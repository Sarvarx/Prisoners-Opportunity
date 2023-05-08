using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class SceneLoader : MonoBehaviour
{
    public GameObject loadingScreen;
    public Slider slider;
    public TMP_Text text;
    private void Awake()
    {
        loadingScreen.SetActive(false);
    }
    public void LoadScreen(int buildIndex)
    {
        loadingScreen.SetActive(true);
        StartCoroutine(LoadScene(buildIndex));
    }

    IEnumerator LoadScene(int buildIndex)
    {
        yield return null;

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(buildIndex);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        Debug.Log("Pro :" + asyncOperation.progress);
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            //Output the current progress
            text.text = "Loading: " + ((asyncOperation.progress / 0.9f) * 100) + "%";
            slider.value = Mathf.Clamp01(asyncOperation.progress/0.9f);
            if (asyncOperation.progress / 0.9f == 1)
            {
                asyncOperation.allowSceneActivation = true;
            }
            // Check if the load has finished
            //if (asyncOperation.progress >= 0.9f)
            //{
            //    //Change the Text to show the Scene is ready
            //    text.text = "Press the space bar to continue";
            //    //Wait to you press the space key to activate the Scene
            //    if (Input.GetKeyDown(KeyCode.Space))
            //        //Activate the Scene
            //        asyncOperation.allowSceneActivation = true;
            //}

            yield return null;
        }
    }
}
