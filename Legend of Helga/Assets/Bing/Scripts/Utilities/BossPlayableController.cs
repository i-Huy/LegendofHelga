using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.AI;

public class BossPlayableController : MonoBehaviour
{
    public PlayerControl.PlayerController pController;
    public PlayerControl.InputController ipController;
    public CannonController bossController;
    public LaunchProjectile ballController;
    public NavMeshAgent agent;
    public CannonCollision coll;
    public GameObject cam;
    private PlayableDirector director;

    // Start is called before the first frame update
    void Awake()
    {
        director = GetComponent<PlayableDirector>();
    }

    void Start()
    {
        pController.followCamera.SetActive(false);
        ipController.enabled = false;
        pController.SetMoveInput(Vector3.zero);
        pController.enabled = false;
        bossController.enabled = false;
        ballController.enabled = false;
        coll.enabled = true;
        cam.SetActive(true);

    }

    void Reset(PlayableDirector d)
    {
        cam.SetActive(false);
        pController.enabled = true;
        ipController.enabled = true;
        pController.followCamera.SetActive(true);
        bossController.enabled = true;
        ballController.enabled = true;
        agent.enabled = true;
    }

    // Update is called once per frame
    void OnEnable()
    {
        director.stopped += Reset;
    }

    void OnDisable()
    {
        director.stopped -= Reset;
    }
}
