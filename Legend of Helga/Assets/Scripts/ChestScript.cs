using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerControl;
using TMPro;

public class ChestScript : MonoBehaviour
{
    [SerializeField] public Animator myAnimationController;
    //[SerializeField] private GameObject hiddenTextBox;
    public GameObject rewardPrefab;
    GameObject emptybox;
    [HideInInspector] public GameObject reward;
    RotateSword reward_cw;
    public float rotate_speed = 30;
    public float rotatex = -90;
    public float rotatey = 0;
    public float rotatez = 0;
    public Vector3 pivot = new Vector3(-0.1f, -0.5f, -0.1f);
    public Vector3 offset = new Vector3(0.1f, 0.7f, 0);
    private BoxCollider ChestCol;
    [SerializeField] private GameObject hiddenTextBoxOpen;
    [SerializeField] private GameObject hiddenTextBoxCollect;

    public string index;


    private bool textAppeared;
    private bool chestOpened;
    private bool inTriggerArea;

    private bool donotinteract = false;
    private HealthPotionUtility hpu;

    private GameObject player;

    void Start()
    {
        hiddenTextBoxOpen.SetActive(false);
        hiddenTextBoxCollect.SetActive(false);

        //Setup Global Control
        player = GameObject.Find("PlayerTest");

        // Setup Sword
        ChestCol = gameObject.GetComponent<BoxCollider>();
        reward = Instantiate(rewardPrefab, transform) as GameObject;
        emptybox = new GameObject("BoxCollider");
        emptybox.transform.parent = this.gameObject.transform;
        emptybox.transform.position = gameObject.transform.position;
        emptybox.transform.rotation = gameObject.transform.rotation;
        BoxCollider bCol = emptybox.AddComponent<BoxCollider>();
        bCol.center = ChestCol.center;
        bCol.isTrigger = true;
        bCol.size = ChestCol.size;
        emptybox.AddComponent<ItemScript>();
        hpu = emptybox.AddComponent<HealthPotionUtility>();
        int ret = hpu.RegisterObject(index);
        if (ret == 0)
        {
            Debug.Log("Yikes");
            myAnimationController.SetBool("ForceOpen", true);
            donotinteract = true;

        }
        else
        {
            Debug.Log("As Normal");
            reward.AddComponent<RotateSword>();
            reward_cw = reward.GetComponent<RotateSword>();
            reward_cw.rotation_speed = rotate_speed;
            reward.transform.position = gameObject.transform.position + offset;
            reward_cw.pivot = reward.transform.position + pivot;
            reward.transform.Rotate(rotatex, rotatey, rotatez);
        }
        reward.SetActive(false);
        emptybox.SetActive(false);

    }

    private void Update()
    {
        if (reward !=null && reward.activeSelf && inTriggerArea && !donotinteract)
        {
            hiddenTextBoxOpen.SetActive(false);
            hiddenTextBoxCollect.SetActive(true);
        }
        else
        {
            hiddenTextBoxCollect.SetActive(false);
        }
       
    }


    public void Interact()
    {
        Debug.Log("Interact in Chest");

        if (textAppeared && inTriggerArea)
        {
            Debug.Log("Opening");
            myAnimationController.SetBool("Open", true);

            hiddenTextBoxOpen.SetActive(false);
            textAppeared = false;

        }
    }
    
    public void PickUp() {
        if (!donotinteract)
        {
            Debug.Log("Picking Up in Chest");

            PlayerControl.PlayerStatus ps = player.GetComponent<PlayerControl.PlayerStatus>();
            if (reward.name.Contains("Sword"))
            {
                ps.hasSword = true;
                Destroy(reward);

                hpu.DeRegisterObject(index);
                GlobalControl.Instance.InitialHP += 1;
                GlobalControl.Instance.HP = GlobalControl.Instance.InitialHP;

                ps.initialHP = GlobalControl.Instance.InitialHP;
                ps.currHP = GlobalControl.Instance.HP;
            }

            if (reward.name.Contains("Bow"))
            {
                ps.hasBow = true;
                Destroy(reward);

                hpu.DeRegisterObject(index);
                GlobalControl.Instance.InitialHP += 1;
                GlobalControl.Instance.HP = GlobalControl.Instance.InitialHP;

                ps.initialHP = GlobalControl.Instance.InitialHP;
                ps.currHP = GlobalControl.Instance.HP;
            }
        }

    }

    void OnTriggerEnter(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            textAppeared = false;
            inTriggerArea = true;
            PlayerController p = c.GetComponent<PlayerController>();
            p.interact_item = gameObject;
            Debug.Log("Setting Item");
        }

    }

    void OnTriggerExit(Collider c)
    {
        if (c.CompareTag("Player"))
        {
            PlayerController p = c.GetComponent<PlayerController>();
            p.interact_item = null;
            Debug.Log("Clearing Item");
        }
        hiddenTextBoxOpen.SetActive(false);
        hiddenTextBoxCollect.SetActive(false);
        textAppeared = false;
        inTriggerArea = false;
    }

    public void ChestTextAppear(string message)
    {
        if (message.Equals("HalfOpenAnimationEnded"))
        {
            hiddenTextBoxOpen.SetActive(true);
            textAppeared = true;
        }
    }

    public void ChestOpenEnd(string message)
    {
        if (message.Equals("ChestOpenAnimationEnded") && !donotinteract)
        {
            Debug.Log("Open Ended");
            reward.SetActive(true);
            emptybox.SetActive(true);
            chestOpened = true;

        }
    }


}