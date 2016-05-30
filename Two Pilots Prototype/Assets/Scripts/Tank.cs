using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tank : MonoBehaviour {

    public int lifes = 5;
	public float speed = 1f;
    public float energyMax;
    public float shieldCost;
    public float dodgeDuration;
    public float dodgeSpeed;
    public bool iFrames;
    public GameObject body;
    public GameObject turret;
    public GameObject shield;
    public Text lifesText;
    public Image energyBar;
    public Image shieldIcon;
    public Image damageScreen;
    public Material tankMaterial;
    public Ability[] abilities = new Ability[4];

    float energyCurrent;
    bool invincible = false;
    bool dodging = false;
    float dodgeStart;
    Vector3 movement;
    Rigidbody rb;

    void Start ()
    {
        rb = this.GetComponent<Rigidbody>();
        lifesText.text = "Lifes: " + lifes;
        damageScreen.enabled = false;
        StopInvinvibility();
    }

    void Update()
    {
        energyBar.transform.localScale = new Vector3(energyCurrent / energyMax, 1, 1);

        if(energyCurrent >= shieldCost)
        {
            shieldIcon.transform.localScale = Vector3.one;
        }
        else
        {
            shieldIcon.transform.localScale = Vector3.zero;
        }

        if (Input.GetKeyDown("e"))
        {
            if(energyCurrent >= shieldCost)
            {
                energyCurrent -= shieldCost;
                UseShield();
            }
        }

        if (Input.GetKeyDown("space"))
        {
            dodging = true;
            dodgeStart = Time.time;
            if (iFrames)
            {
                Invincibility(dodgeDuration);
                tankMaterial.color = new Color(0.17f, 0.51f, 0.27f, 0.5f);
            }
        }
    }

	void FixedUpdate ()
	{
        if (dodging == false)
        {
            movement = new Vector3(Input.GetAxis("Horizontal1"), 0f, Input.GetAxis("Vertical1"));
            Move(movement);
            body.transform.LookAt(transform.position + movement * 100);
        }
        else
        {
            Dodge();

            if (Time.time > dodgeStart + dodgeDuration)
            {
                dodging = false;
            }
        }
	}

	void Move(Vector3 movement)
	{
        rb.velocity = movement.normalized * speed;
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet" && invincible == false)
        {
            lifes -= 1;
            lifesText.text = "Lifes: " + lifes;
            BlinkDamageScreen();
            tankMaterial.color = new Color(0.17f, 0.51f, 0.27f, 0.5f);
            Invincibility(1f);
            Destroy(other.gameObject);
        }
        else if (other.tag == "PickUp")
        {
            turret.GetComponent<Turret>().ReduceCooldowns(1);
            Destroy(other.gameObject);
        }
        else if(other.tag == "Enemy" && invincible == false)
        {
            lifes -= 1;
            lifesText.text = "Lifes: " + lifes;
            BlinkDamageScreen();
            tankMaterial.color = new Color(0.17f, 0.51f, 0.27f, 0.5f);
            Invincibility(1f);
        }
    }

    void Invincibility(float duration)
    {
        invincible = true;
        Invoke("StopInvinvibility", duration);
    }

    void StopInvinvibility()
    {
        invincible = false;
        tankMaterial.color = new Color(0.17f, 0.51f, 0.27f, 1f);
    }

    public void AddEnergy(int energyAdded)
    {
        energyCurrent += energyAdded;
        energyCurrent = Mathf.Clamp(energyCurrent, 0, energyMax);
    }

    void Dodge()
    {
        rb.velocity = movement.normalized * dodgeSpeed;
    }

    void UseShield()
    {
        Instantiate(shield, transform.position, transform.rotation);
    }

    void BlinkDamageScreen()
    {
        float blinkTime = 0.1f;
        damageScreen.enabled = true;
        Invoke("HideDamageScreen", blinkTime);
    }

    void HideDamageScreen()
    {
        damageScreen.enabled = false;
    }
}
