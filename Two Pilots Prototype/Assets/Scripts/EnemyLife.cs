using UnityEngine;
using System.Collections;

public class EnemyLife : MonoBehaviour
{
    public int hp = 3;
    public int energyValue;
    public float dropChance;
    public GameObject pickUp;

    GameObject player;

    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        player = GameObject.Find("Tank");
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BulletPlayer")
        {
            hp -= 1;
            Destroy(other.gameObject);
            if (hp <= 0)
            {
                Destroy();
            }
        }
        else
        {
            if (other.tag == "Missle")
            {
                Destroy();
            }
        }
    }

    void Destroy()
    {
        player.GetComponent<Tank>().AddEnergy(energyValue);

        if (Random.value >= dropChance)
        {
            Instantiate(pickUp, transform.position, transform.rotation);
        }

        Destroy(gameObject.transform.parent.gameObject);
    }
}
