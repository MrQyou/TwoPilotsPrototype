using UnityEngine;
using System.Collections;

public class Missle : MonoBehaviour
{
    public float speed;
    public float lifeTime;

    float startTime;
    Rigidbody rb;

    void Start()
    {
        startTime = Time.time;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Time.time > startTime + lifeTime)
        {
            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        rb.velocity = transform.forward * speed;
    }
}
