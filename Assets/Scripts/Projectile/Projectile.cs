using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
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
        float speed = 40f;
        rb.velocity = transform.forward * speed;
    }

    void OnTriggerEnter(Collider other)
    {
        
        
        // if (other.TryGetComponent(out ProjectTarget projectTarget ))
        // {
        //     Debug.Log("타겟이당");
        // }
        // else
        // {
        //     Debug.Log("타겟이 아니당");
        // }
        
        // //
        // if ( ! other.gameObject.CompareTag("GoodProjectile"))
        // {
        //     Destroy(gameObject);
        // }
        
    }


}
