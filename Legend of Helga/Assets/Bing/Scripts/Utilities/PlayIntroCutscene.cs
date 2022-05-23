using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayIntroCutscene : MonoBehaviour
{
    public GameObject player;
    public GameObject cutscene;
    public GameObject cam;
    private bool played;
    private bool isPlaying;
    private HealthPotionUtility hpu;

    // Start is called before the first frame update
    void Awake()
    {
        cutscene.SetActive(false);
        cam.SetActive(false);
        played = false;
        isPlaying = false;
        hpu = GetComponent<HealthPotionUtility>();
    }

    // Update is called once per frame
    void Update()
    {
        if (played) return;
        if (isPlaying)
        {
            isPlaying = false;
            cam.SetActive(false);
            cutscene.SetActive(false);
            played = true;

            PlayerControl.PlayerController controller =
                player.GetComponent<PlayerControl.PlayerController>();
            PlayerControl.InputController ict =
                player.GetComponent<PlayerControl.InputController>();
            Animator ani = player.GetComponentInChildren<Animator>();

            controller.enabled = true;
            controller.followCamera.SetActive(true);
            ict.enabled = true;
            // ani.enabled = true;

            hpu.DeRegisterObject();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (played)
            return;
        if (other.gameObject.layer != 11)
            return;

        PlayerControl.PlayerController controller =
            player.GetComponent<PlayerControl.PlayerController>();
        PlayerControl.InputController ict =
            player.GetComponent<PlayerControl.InputController>();
        Animator ani = player.GetComponentInChildren<Animator>();

        // ani.enabled = false;
        ict.enabled = false;
        controller.followCamera.SetActive(false);
        controller.SetMoveInput(Vector3.zero);
        controller.enabled = false;

        cam.SetActive(true);
        cutscene.SetActive(true);
    }

    public void SetIsPlaying(bool v)
    {
        isPlaying = v;
    }
}
