using UnityEngine;
using System.Collections;

public class Rotate : MonoBehaviour
{
    public float speed;

    void Update()
    {
        transform.rotation *= Quaternion.Euler(Vector3.up * speed * Time.deltaTime);
    }
}
