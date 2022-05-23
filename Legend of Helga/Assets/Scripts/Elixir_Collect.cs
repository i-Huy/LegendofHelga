using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elixir_Collect : MonoBehaviour
{
    private GameObject globalControlObject;
    private GlobalControl globalControl;
    private HealthPotionUtility hpu;

    void Start()
    {
        globalControlObject = GameObject.Find("GlobalObject");
        globalControl = globalControlObject.GetComponent<GlobalControl>();
        hpu = GetComponent<HealthPotionUtility>();
    }

    void OnTriggerEnter(Collider c)
    {

        if (c.gameObject.name == "PlayerTest")
        {
            GetComponent<Renderer>().enabled = false;
            // TODO: add logic
            hpu.DeRegisterObject();
            globalControl.InitialHP += 1;
            globalControl.HP = globalControl.InitialHP;

            PlayerControl.PlayerStatus ps =
                c.gameObject.GetComponent<PlayerControl.PlayerStatus>();
            ps.initialHP = globalControl.InitialHP;
            ps.currHP = globalControl.HP;
        }
    }
}
