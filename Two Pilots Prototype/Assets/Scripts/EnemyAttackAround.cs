using UnityEngine;
using System.Collections;

public class EnemyAttackAround : MonoBehaviour
{
    public float rateOfFire;
    public int numberOfBullets;
    public float range;
    public GameObject bullet;

    float nextShot;
    GameObject player;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if((player.transform.position - transform.position).magnitude <= range)
        {
            if (Time.time > nextShot)
            {
                nextShot = Time.time + rateOfFire;
                Shoot();
            }
        }
    }

    void Shoot()
    {
        float angle = 360 / numberOfBullets;
        float spread = 0;

        for(int i = 0; i < numberOfBullets; i++)
        {
            Instantiate(bullet, transform.position, transform.rotation * Quaternion.Euler(0, spread, 0));
            spread += angle;
        }
    }
}
