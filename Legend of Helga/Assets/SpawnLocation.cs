using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnLocation : MonoBehaviour
{
    public Transform Location1;
    public Transform Location2;
    public Transform Location3;
    public Transform Location4;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        if(GlobalControl.Instance.Scene == "FireContinent")
        {
            player.transform.position = Location2.position;
            player.transform.rotation = Location2.rotation;
        }
        else if (GlobalControl.Instance.Scene == "WindContinent")
        {
            player.transform.position = Location3.position;
            player.transform.rotation = Location3.rotation;
        }
        else if (GlobalControl.Instance.Scene == "BossContinent")
        {
            player.transform.position = Location4.position;
            player.transform.rotation = Location4.rotation;
        }else
        {
            player.transform.position = Location1.position;
            player.transform.rotation = Location1.rotation;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
