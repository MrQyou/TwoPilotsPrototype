using UnityEngine;
using System.Collections;

public class EnemyLife : MonoBehaviour
{
    public int hp = 3;
    //public int energyValue;
    public int dropsMin;
    public int dropsMax;

    GameObject pickUp;
    GameObject player;

    void Start()
    {
        //player = GameObject.FindGameObjectWithTag("Player");
        player = GameObject.Find("Tank");
        pickUp = Resources.Load("Shard") as GameObject;
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
        player.GetComponent<Tank>().AddShooterCombo();

        int drops = Random.Range(dropsMin, dropsMax);

        for(int i = 0; i < drops; i++)
        {
            Instantiate(pickUp, transform.position, Quaternion.Euler(Vector3.zero));
        }

        Destroy(gameObject.transform.parent.gameObject);
    }
}
