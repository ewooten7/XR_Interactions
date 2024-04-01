using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Rigidbody))]
public class WandProjectile : MonoBehaviour
{
    [SerializeField] int power = 200;
    [SerializeField] float destroyTime = 5.0f;
    private Rigidbody rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.AddForce(transform.forward * power);
        Destroy(gameObject, destroyTime);
    }

}

