using UnityEngine;
using System.Collections;

public class EnemyMovementChaser : MonoBehaviour
{
    public float speed;

    Vector3 shift;
    float changeTime = 2;
    float nextChange;
    float distance;
    GameObject player;
    Rigidbody rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(Time.time > nextChange)
        {
            distance = Mathf.Abs((player.transform.position - transform.position).magnitude); 
            nextChange = Time.time + changeTime;
            shift = new Vector3(1, 0, 1) * Random.Range(-distance / 1.5f, distance / 1.5f);
        }
      
    }

    void FixedUpdate()
    {
        transform.LookAt(player.transform.position + shift);
        rb.velocity = transform.forward * speed;
    }
}
