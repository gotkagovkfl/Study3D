using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    Rigidbody rb;
    

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }


    void Start()
    {
        float speed = 10f;
        rb.velocity = transform.forward * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        Destroy(gameObject);
    }
}
