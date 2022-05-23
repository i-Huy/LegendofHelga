using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PlayerControl;

public class SceneChanger : MonoBehaviour
{
    public Animator anim;
    public GameObject player;
    private PlayerStatus pStatus;
    string nextScene;
    string currScene;

    private void Start()
    {
        if (player != null)
        {
            pStatus = player.GetComponent<PlayerStatus>();
        }
        Scene scene = SceneManager.GetActiveScene();
        currScene = scene.name;
    }

    public void FadetoScene(string sceneString)
    {
        // Debug.Log("Fadestart");
        nextScene = sceneString;
        anim.SetTrigger("FadeOut");
    }
    public void OnFadeComplete()
    {
        // Debug.Log("Fade complete");
        if (player != null)
        {
            pStatus.SavePlayer();
        }
        if (currScene.Contains("Continent"))
        {
            // Debug.Log("contain continent");
            GlobalControl.Instance.Scene = currScene;
        }
        GlobalControl.Instance.HP = GlobalControl.Instance.InitialHP;
        // Debug.Log("Before load scene");
        SceneManager.LoadScene(nextScene);
        // Debug.Log("After load scene");
    }
}
