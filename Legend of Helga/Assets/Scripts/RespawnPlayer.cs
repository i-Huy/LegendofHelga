using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RespawnPlayer : MonoBehaviour
{
    // private GameObject sceneChangerObject;
    // private SceneChanger sceneChanger;

    void Start()
    {
        // sceneChangerObject = GameObject.Find("SceneChanger");
        // sceneChanger = sceneChangerObject.GetComponent<SceneChanger>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider c) {
        if (c.tag == "Player")
        {
            SceneManager.LoadScene(
            SceneManager.GetActiveScene().name, LoadSceneMode.Single);
        }
    }
}
