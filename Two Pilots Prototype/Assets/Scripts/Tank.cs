using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Tank : MonoBehaviour {

    public int lifes = 5;
	public float speed = 1f;
    public float comboTimeMax;
    public float energyMax;
    public float shieldCost;
    public float dodgeDuration;
    public float dodgeSpeed;
    public bool iFrames;
    public float rushSpeed;
    public GameObject body;
    public GameObject turret;
    public GameObject shield;
    public GameObject blockShield;
    public Text lifesText;
    public Image energyBar;
    public Image damageScreen;
    public Material tankMaterial;
    public Ability[] abilities = new Ability[4];
    public Color invincibilityColor;
    public Text comboTextMover;
    public Text comboTextShooter;
    public Image comboBarShooter;

    int comboCounterMover = 0;
    int comboCounterShooter = 0;
    float comboTimeCurrent = 0;
    float currentSpeed;
    float energyCurrent;
    bool invincible = false;
    bool dodging = false;
    float dodgeStart;
    Vector3 movement;
    Rigidbody rb;
    Color tankColor;

    void Start ()
    {
        currentSpeed = speed;
        rb = this.GetComponent<Rigidbody>();
        lifesText.text = "Lifes: " + lifes;
        damageScreen.enabled = false;
        tankColor = tankMaterial.color;
        StopInvinvibility();
        blockShield.SetActive(false);
        UpdateComboTextMover();
        UpdateComboTextShooter();
    }

    void Update()
    {
        energyBar.transform.localScale = new Vector3(energyCurrent / energyMax, 1, 1);

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
            }
        }

        if (Input.GetKeyDown("c")) { currentSpeed = rushSpeed; }
        if (Input.GetKeyUp("c")){ currentSpeed = speed; }

        if (Input.GetKeyDown("v")) { currentSpeed = 0; blockShield.SetActive(true); }
        if (Input.GetKeyUp("v")) { currentSpeed = speed; blockShield.SetActive(false); }

        ReduceComboTime();
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
        rb.velocity = movement.normalized * currentSpeed;
	}

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet" && invincible == false)
        {
            TakeDamage(1);
            Destroy(other.gameObject);
        }
        else if (other.tag == "PickUp")
        {
            comboCounterMover++;
            UpdateComboTextMover();
            turret.GetComponent<Turret>().ReduceCooldowns(comboCounterMover);
            Destroy(other.gameObject);
        }
        else if(other.tag == "Enemy" && invincible == false)
        {
            TakeDamage(1);
        }
    }

    void Invincibility(float duration)
    {
        invincible = true;
        tankMaterial.color = invincibilityColor;
        Invoke("StopInvinvibility", duration);
    }

    void StopInvinvibility()
    {
        invincible = false;
        tankMaterial.color = tankColor;
    }

    void AddEnergy(int energyAdded)
    {
        energyCurrent += energyAdded;
        energyCurrent = Mathf.Clamp(energyCurrent, 0, energyMax);
    }

    public void AddShooterCombo()
    {
        comboCounterShooter++;
        comboTimeCurrent = comboTimeMax;
        AddEnergy(comboCounterShooter);
        UpdateComboTextShooter();
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

    void TakeDamage(int damage)
    {
        lifes -= damage;
        lifesText.text = "Lifes: " + lifes;
        BlinkDamageScreen();
        Invincibility(1f);
        comboCounterMover = 0;
        UpdateComboTextMover();
    }

    void UpdateComboTextMover()
    {
        comboTextMover.text = "Combo: " + comboCounterMover;
    }

    void UpdateComboTextShooter()
    {
        comboTextShooter.text = "Combo: " + comboCounterShooter;
    }

    void ReduceComboTime()
    {
        if(comboCounterShooter > 0)
        {
            comboTimeCurrent -= Time.deltaTime;

            if(comboTimeCurrent <= 0)
            {
                comboCounterShooter--;
                comboTimeCurrent = comboTimeMax;
                UpdateComboTextShooter();
            }
            comboTimeCurrent = Mathf.Clamp(comboTimeCurrent, 0, comboTimeMax);
            comboBarShooter.transform.localScale = new Vector3(comboTimeCurrent / comboTimeMax, 1, 1);
        }
    }
}
