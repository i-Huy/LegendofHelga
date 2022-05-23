using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemyAI
{
    public class ColliderChildDelegate : MonoBehaviour
    {
        private WalkableEnemyDeath status;

        // Start is called before the first frame update
        void Start()
        {
            status = GetComponentInParent<WalkableEnemyDeath>();
            if (status == null)
            {
                Debug.LogError("Cannot find Enemy AI death module");
            }
        }

        // Update is called once per frame
        void Update()
        {

        }

        void OnTriggerEnter(Collider other)
        {
            status.OnTriggerEnterChild(other);
        }
    }
}
