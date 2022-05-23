using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaunchProjectile : MonoBehaviour
{
    public GameObject projectile;
    public GameObject explodingProjectile;
    public GameObject target;
    public GameObject platform;
    public GameObject bossParent;
    public GameObject aoeEffect;
    public Vector3 offset = new Vector3(0f, 0.5f, 0f);
    public float launchVelocity = 10f;
    private float timeLastShot = 0f;
    private bool waitUp = false;

    GameObject audioEventManager_obj;
    AudioEventManager audioEventManager;

    // Start is called before the first frame update
    IEnumerator Start()
    {
        yield return new WaitForSeconds(2);
        waitUp = true;

        audioEventManager_obj = GameObject.Find("AudioEventManager");
        audioEventManager = audioEventManager_obj.GetComponent<AudioEventManager>();
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        bool inSight = false;
        if (Physics.Raycast(transform.position, target.transform.position - transform.position, out hit) && waitUp)
        {
            inSight = (hit.transform.gameObject == target);
        }
        int cannonHealth = bossParent.GetComponent<CannonCollision>().currHP;
        float distance = Vector3.Distance(target.transform.position, transform.position);

        if (Time.time > 1.75 + timeLastShot && cannonHealth >= 5 && inSight && waitUp)
        {          
            GameObject ball = Instantiate(projectile, transform.position, transform.rotation);
            audioEventManager.CannonLaunchEvent();
            StartCoroutine(Wait());
            ball.GetComponent<Rigidbody>().AddForce((target.transform.position + offset - this.transform.position) * 50f, ForceMode.Impulse);
            timeLastShot = Time.time;
        }
        else if (Time.time > 3 + timeLastShot && cannonHealth < 5 && cannonHealth > 0 && inSight && waitUp)
        {
            Vector3 updatedVec = new Vector3(target.transform.position.x, target.transform.position.y, target.transform.position.z);
            GameObject aoe = Instantiate(aoeEffect, updatedVec, Quaternion.identity);
            GameObject explodingBall = Instantiate(explodingProjectile, transform.position, transform.rotation);
            audioEventManager.ExplosionEvent();
            explodingBall.GetComponent<Rigidbody>().AddForce((target.transform.position + offset - this.transform.position) * 3.5f, ForceMode.Impulse);
            Destroy(aoe, 2);

            timeLastShot = Time.time;
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(5);
    }
}
