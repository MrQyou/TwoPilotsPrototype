using UnityEngine;
using System.Collections;

public class EnemyAttack01 : MonoBehaviour
{
    public GameObject bullet;
    public Transform fireHole;

    public float range;
    public float shootRate;
    public int numberOfBullets;

    float nextShot = 0;
    GameObject player;

	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	void Update ()
    {
        transform.LookAt(player.transform.position);

        if(Mathf.Abs((transform.position - player.transform.position).magnitude) <= range)
        {
            if(Time.time > nextShot)
            {
                Shoot();
                nextShot = Time.time + shootRate;
            }
            
        }
	}

    void Shoot()
    {
        int spread = 10;
        int angle = -spread;
        

        for(int i = 0; i < numberOfBullets; i++)
        {
            angle *= -1;
            spread *= -1;
            angle += spread;
            Instantiate(bullet, fireHole.position, transform.rotation * Quaternion.Euler(Vector3.up * angle));
        }
    }
}
