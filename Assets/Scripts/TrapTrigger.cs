using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapTrigger : MonoBehaviour
{
    [SerializeField] private Rigidbody body;
    public float force;

    private void OnTriggerEnter(Collider other)
    {
        if (body.isKinematic)
        {
            body.isKinematic = false;
            body.velocity = new Vector3(0,0,force);
        }
    }
}
