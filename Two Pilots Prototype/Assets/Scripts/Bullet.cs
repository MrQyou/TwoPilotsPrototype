using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour
{
    public float speed;
    public float lifeTime;
    public Vector3 tankVelocity;

    private Rigidbody rb;
    private float startTime;

	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        startTime = Time.time;
	}
	
    void Update()
    {
        if(Time.time > startTime + lifeTime)
        {
            Destroy(gameObject);
        }
    }

	void FixedUpdate ()
    {
        rb.velocity = (transform.forward * speed + tankVelocity).normalized * speed;
	}
    /*
    void OnTriggerEnter (Collider other)
    {
        if (other.tag != "Bullet" && other.tag != "BulletPlayer")
        Destroy(gameObject);
    }
    */
}
