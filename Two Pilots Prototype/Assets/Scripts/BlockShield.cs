using UnityEngine;
using System.Collections;

public class BlockShield : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            Destroy(other.gameObject);
        }
    }
}
