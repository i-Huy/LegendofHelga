using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneSwitchBoss : MonoBehaviour
{
    public SceneChanger sceneChanger;

    private void OnTriggerEnter(Collider other)
    {
        sceneChanger.FadetoScene("BossContinent");
    }
}
