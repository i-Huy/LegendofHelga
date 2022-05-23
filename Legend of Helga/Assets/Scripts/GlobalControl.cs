using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GlobalControl : MonoBehaviour
{
    public static GlobalControl Instance;

    public int InitialHP = 3;
    public int HP;
    public bool hasSword;
    public bool hasBow;
    public string Scene;
    [HideInInspector] public Dictionary<string, bool> globalRegisteredObjects;
    [HideInInspector] public Vector3 globalCheckpoint;
    [HideInInspector] public string checkpointScene;
    [HideInInspector] public bool storycompleted;

    [HideInInspector] public bool beginPanPlayed = false;
    [HideInInspector] public bool windPanPlayed = false;
    public GameObject currentActivedCheckpoint;


    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }

        HP = InitialHP;
        hasSword = false;
        hasBow = false;
        globalRegisteredObjects = new Dictionary<string, bool>();

        Scene = SceneManager.GetActiveScene().name;
        currentActivedCheckpoint = new GameObject();
        windPanPlayed = false;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // Debug.LogError(Scene);
    }

    public void ResetGlobalObject()
    {
        InitialHP = 3;
        HP = InitialHP;
        hasSword = false;
        hasBow = false;
        globalRegisteredObjects = new Dictionary<string, bool>();
        globalCheckpoint = Vector3.zero;
        checkpointScene = null;
        currentActivedCheckpoint = new GameObject();
        windPanPlayed = false;
    }

    public void RegisterObject(string index, bool value)
    {
        if (!globalRegisteredObjects.ContainsKey(index))
        {
            globalRegisteredObjects.Add(index, value);
        }
    }

    public void SetGlobalCheckpoint(Vector3 pos, string scenename, GameObject checkpoint = default(GameObject))
    {
        globalCheckpoint = pos;
        checkpointScene = scenename;
        currentActivedCheckpoint = checkpoint;
    }
}
