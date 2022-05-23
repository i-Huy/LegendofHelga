using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenuScript : MonoBehaviour
{
    private GameObject sceneChangerObject;
    private SceneChanger sceneChanger;
    private GameObject globalControlObject;
    private GlobalControl globalControl;
    private GameObject player;

    private void Start()
    {
        sceneChangerObject = GameObject.Find("SceneChanger");
        sceneChanger = sceneChangerObject.GetComponent<SceneChanger>();
        globalControlObject = GameObject.Find("GlobalObject");
        globalControl = globalControlObject.GetComponent<GlobalControl>();
    }

    public void StartGame()
    {
        // Debug.Log("StartGame");
        sceneChanger.FadetoScene("BeginningIsland");
        Time.timeScale = 1f;
    }

    public void RespawnAtCheckpoint()
    {
        SceneManager.LoadScene(globalControl.Scene);
    }

    public void ResetAtCheckpoint()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().name, LoadSceneMode.Single);
    }

    public void GoToMenu()
    {
        Time.timeScale = 1f;
        globalControl.ResetGlobalObject();
        sceneChanger.FadetoScene("MainMenu");
    }

    public void GotoMenufromCredits()
    {
        Time.timeScale = 1f;
        sceneChanger.FadetoScene("MainMenu");
    }

    public void GoToCredits()
    {
        Time.timeScale = 1f;
        globalControl.ResetGlobalObject();
        sceneChanger.FadetoScene("CreditsMenu");
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
         Application.Quit();
#endif
    }

}