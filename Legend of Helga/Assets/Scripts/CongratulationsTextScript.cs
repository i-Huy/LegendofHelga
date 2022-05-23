using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerControl;

public class CongratulationsTextScript : MonoBehaviour
{

    [SerializeField] private GameObject hiddenTextBoxSword;
    [SerializeField] private GameObject hiddenTextBoxBow;
    public GameObject player;
    private PlayerStatus pStatus;
    private bool didhaveSword;
    private bool didhaveBow;
    private float time;
    private bool runTimer;
    GameObject audioEventManager_obj;
    AudioEventManager audioEventManager;

    // Start is called before the first frame update
    void Start()
    {
        pStatus = player.GetComponent<PlayerStatus>();
        didhaveSword = GlobalControl.Instance.hasSword;
        didhaveBow = GlobalControl.Instance.hasBow;
        hiddenTextBoxBow.SetActive(false);
        hiddenTextBoxSword.SetActive(false);
        time = 0;

        audioEventManager_obj = GameObject.Find("AudioEventManager");
        audioEventManager = audioEventManager_obj.GetComponent<AudioEventManager>();

    }

    // Update is called once per frame
    void Update()
    {
        if(pStatus.hasBow && !didhaveBow)
        {
            hiddenTextBoxBow.SetActive(true);
            didhaveBow = true;
            runTimer = true;
            audioEventManager.RewardEvent();
        }
        if(pStatus.hasSword && !didhaveSword)
        {
            hiddenTextBoxSword.SetActive(true);
            didhaveSword = true;
            runTimer = true;
            audioEventManager.RewardEvent();
        }
        if (runTimer)
        {
            time = time + Time.deltaTime;
        }
        if(time > 5)
        {
            hiddenTextBoxBow.SetActive(false);
            hiddenTextBoxSword.SetActive(false);
            time = 0;
        }
        
    }

}
