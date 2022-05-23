using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessBossSceneShowUp : MonoBehaviour
{
    public CannonCollision bossStatus;
    public SceneChanger sceneChanger;

    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
        this.transform.Find("Motion").gameObject.SetActive(false);
        this.gameObject.GetComponent<BoxCollider>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (bossStatus.currHP <= 0)
        {
            this.gameObject.GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
            this.transform.Find("Motion").gameObject.SetActive(true);
            this.gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }

    void OnTriggerEnter()
    {
        sceneChanger.FadetoScene("TwistEnding");
    }
}
