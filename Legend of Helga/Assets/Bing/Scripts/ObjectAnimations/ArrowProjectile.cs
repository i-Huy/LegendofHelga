using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowProjectile : MonoBehaviour
{
    private Vector3 projectileDir;
    private Vector3 hitDest;
    private float projectileForwardSpeed;
    private float releasedTime;
    public float lifetime;
    private bool _released;
    public bool released
    {
        get { return _released; }
        set { _released = value; }
    }
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        projectileDir = Vector3.zero;
        projectileForwardSpeed = 0f;
        _released = false;
        releasedTime = 0f;
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (released)
        {
            Vector3 newPos = transform.position +
                projectileDir * projectileForwardSpeed * Time.deltaTime;
            // transform.position = newPos;
            rb.MovePosition(newPos);
            releasedTime += Time.deltaTime;

            if (releasedTime >= lifetime)
            {
                Destroy(gameObject);
            }
        }
    }

    public void SetParameters(Vector3 direction, float speed)
    {
        // projectileDir = direction;
        // projectileDir.Normalize();
        projectileForwardSpeed = speed;
    }

    public void SetDest(Vector3 hitPoint)
    {
        hitDest = hitPoint;

        projectileDir = hitDest - transform.position;
        projectileDir.Normalize();

        transform.right = -projectileDir;
    }

    void OnCollisionEnter(Collision other)
    {
        // Debug.LogError(other.gameObject.name);
        Destroy(gameObject);
    }
}
