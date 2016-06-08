using UnityEngine;
using System.Collections;

public class Shard : MonoBehaviour
{
    public float speed;
    public float duration;

    float stopTime;

    void Start()
    {
        stopTime = Time.time + duration;
        transform.Rotate(new Vector3(0, Random.Range(0, 360), 0));
    }
	
	void Update ()
    {
        if(Time.time <= stopTime)
        {
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }
	}
}
