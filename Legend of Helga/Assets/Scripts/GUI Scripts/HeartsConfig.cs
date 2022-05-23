// Huy
// TODO: most of this is placeholder for implementing icons/visual hp indicator
// trigger death animation + display death screeen when HP reaches 0
// script is from: https://www.youtube.com/watch?v=3uyolYVsiWc ; modified by me

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PlayerControl;

[RequireComponent(typeof(CanvasGroup))]
public class HeartsConfig : MonoBehaviour
{
    public PlayerStatus status;
    public GameObject player;
    public GameObject cannon;
    public CannonCollision cannonStatus;

    // public int health;
    //public int numOfHearts;

    public Image[] hearts;
    public Sprite fullHeart;
    public Sprite emptyHeart;

    private GameObject sceneChangerObject;
    private SceneChanger sceneChanger;

    //private CanvasGroup canvasGroup;

    //void Awake()
    //{
    //    canvasGroup = GetComponent<CanvasGroup>();
    //}

    // Start is called before the first frame update
    void Start()
    {
        if(player != null)
            status = player.GetComponent<PlayerStatus>();
        if(cannon != null)
            cannonStatus = cannon.GetComponent<CannonCollision>();
        sceneChangerObject = GameObject.Find("SceneChanger");
        sceneChanger = sceneChangerObject.GetComponent<SceneChanger>();
    }

    // Update is called once per frame
    void Update()
    {
        // test hp reduction using space as placeholder
        //if (Input.GetKeyDown(KeyCode.L) && status != null && status.currHP > 0)
        //{
        //    status.currHP--;
        //}

        //// test hp recovery
        //if (Input.GetKeyDown(KeyCode.O) && status != null && status.currHP > 0)
        //{
        //    status.currHP++;
        //}

        //// test hp max increase
        //if (Input.GetKeyDown(KeyCode.I) && status != null && status.currHP > 0)
        //{
        //    status.initialHP++;
        //}

        //// test hp max decrease
        //if (Input.GetKeyDown(KeyCode.J) && status != null && status.currHP > 0)
        //{
        //    status.initialHP--;
        //}

        // death condition & resolution
        if (status != null && status.currHP <= 0) // had else statement before (and it worked) but stopped working :/
        {
            // TODO: change MainMenu to DeathMenu once scene is made
            sceneChanger.FadetoScene("DeathMenu");

            //if (canvasGroup.interactable)
            //{
            //    canvasGroup.interactable = false; canvasGroup.blocksRaycasts = false; canvasGroup.alpha = 0f;
            //    // Time.timeScale = 1f;
            //}
            //else
            //{
            //    canvasGroup.interactable = true; canvasGroup.blocksRaycasts = true; canvasGroup.alpha = 1f;
            //    // Time.timeScale = 0f;
            //}
            // HPText.text = "DEAD :(";
            // placeholder, trigger death animation + screen
        }
        // if(status != null)
        //     Debug.Log("Hearts health ::::::::: " + status.currHP);
        // display system for hearts on/off (full/empty)
        for (int i = 0; i < hearts.Length; i++)
        {
            int currHP = 0;
            int initialHP = 0;
            if(status != null)
            {
                currHP = status.currHP;
                initialHP = status.initialHP;
            }
            else if(cannonStatus != null) {
                currHP = cannonStatus.currHP;
                initialHP = cannonStatus.initialHP;
            }
            if (currHP > initialHP)
            {
                if(status != null)
                    status.currHP = status.initialHP;
                if(cannonStatus != null)
                    cannonStatus.currHP = cannonStatus.initialHP;
            }

            if (i < currHP)
            {
                hearts[i].sprite = fullHeart;
            }
            else
            {
                hearts[i].sprite = emptyHeart;
            }

            if (i < initialHP)
            {
                hearts[i].enabled = true;
            }
            else
            {
                hearts[i].enabled = false;
            }
        }
    }
}
