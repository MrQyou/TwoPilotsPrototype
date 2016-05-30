using UnityEngine;
using System.Collections;

public class Shield : MonoBehaviour
{
    public float growthSpeed = 0.01f;
    public float lifeTime;

    float startTime;

    void Start()
    {
        startTime = Time.time;
    }

    void FixedUpdate()
    {
        transform.localScale += Vector3.one * growthSpeed * Time.deltaTime;

        if(Time.time > startTime + lifeTime)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Bullet")
        {
            Destroy(other.gameObject);
        }
    }
}
