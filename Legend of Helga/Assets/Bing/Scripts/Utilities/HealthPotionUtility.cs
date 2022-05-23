using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotionUtility : MonoBehaviour
{
    private GameObject globalControlObject;
    private GlobalControl globalControl;
    Dictionary<string, bool> registeredDict;
    public string index;

    // Start is called before the first frame update
    void Start()
    {
        if (index == null)
        {
            Debug.LogError("Need to fill in the index");
        }
        else
        {
            globalControlObject = GameObject.Find("GlobalObject");
            globalControl = globalControlObject.GetComponent<GlobalControl>();

            if (!globalControl.globalRegisteredObjects.ContainsKey(index))
            {
                globalControl.RegisterObject(index, false);
            }
            else
            {
                if (globalControl.globalRegisteredObjects[index])
                {
                    OnDeregisterAction_1();
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int RegisterObject(string index_arg)
    {
        if (index_arg == null)
        {
            Debug.LogError("Need to fill in the index");
        }

        globalControlObject = GameObject.Find("GlobalObject");
        globalControl = globalControlObject.GetComponent<GlobalControl>();

        if (!globalControl.globalRegisteredObjects.ContainsKey(index_arg))
        {
            globalControl.RegisterObject(index_arg, false);
            return 1;
        }
        else
        {
            if (globalControl.globalRegisteredObjects[index_arg])
            {
                return 0;
            }
        }
        return 1;
    }

    public void DeRegisterObject(string index_arg)
    {
        globalControl.globalRegisteredObjects[index_arg] = true;
    }

    public void DeRegisterObject()
    {
        globalControl.globalRegisteredObjects[index] = true;
    }

    public void OnDeregisterAction_1()
    {
        this.gameObject.SetActive(false);
        Destroy(this.gameObject);
    }
}
