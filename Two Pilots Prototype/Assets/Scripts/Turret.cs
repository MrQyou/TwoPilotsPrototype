using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Turret : MonoBehaviour
{
    public float rateOfFire = 1f;
    public float fireEnergyMax = 1f;
    public float fireCost = 1f;
    public float energyReg = 1f;

    public GameObject bullet;
    public Transform fireHole;
    public GameObject tank;
    public Image fireEnergyBar;
    public Image abilityBar1;
    public Image abilityBar2;
    public Image abilityBar3;
    public Image abilityBar4;
    public Text chargeCounter1;
    public Text chargeCounter2;
    public Text chargeCounter3;
    public Text chargeCounter4;
    public Image[] icons = new Image[4];

    Ability[] abilities = new Ability[] { new AbilityBoomerang(), new AbilityBoomerang(), new AbilityBoomerang(), new AbilityBoomerang()};
    float fireEnergy;
    bool shooting = false;
    float nextShot = 0;


    void Start()
    {
        foreach(Ability ability in abilities)
        {
            ability.SetUp();
        }

        SetIcons();

        fireEnergy = fireEnergyMax;
        UpdateChargeCounters();
        UpdateAbilityBars();
    }

    void Update ()
    {
        Vector3 rot = new Vector3(Input.GetAxis("Vertical2"), 0f, Input.GetAxis("Horizontal2"));
        transform.LookAt(transform.position + rot * 10);

        if (Input.GetButtonDown("Shoot"))
        {
            shooting = true;
        }

        if (Input.GetButtonUp("Shoot"))
        {
            shooting = false;
        }

        if(shooting == true)
        {
            if (fireEnergy >= fireCost)
            {
                if (Time.time > nextShot)
                {
                    Shoot();
                    fireEnergy -= fireCost;
                    nextShot = Time.time + rateOfFire;
                }                
            }
            else
            {
                shooting = false;
            }
        }

        else
        {
            RegenerateEnergy();
        }

        fireEnergyBar.rectTransform.localScale = new Vector3(fireEnergy / fireEnergyMax, 1, 1);

        if (Input.GetButtonDown("Ability1")) { abilities[0].Use(fireHole.transform); UpdateChargeCounters(); }
        if (Input.GetButtonDown("Ability2")) { abilities[1].Use(fireHole.transform); UpdateChargeCounters(); }
        if (Input.GetButtonDown("Ability3")) { abilities[2].Use(fireHole.transform); UpdateChargeCounters(); }
        if (Input.GetButtonDown("Ability4")) { abilities[3].Use(fireHole.transform); UpdateChargeCounters(); }
    }

    void Shoot()
    {
        GameObject bulletClone = Instantiate(bullet, fireHole.position, fireHole.rotation) as GameObject;
        bulletClone.GetComponent<Bullet>().tankVelocity = tank.GetComponent<Rigidbody>().velocity;
    }

    void RegenerateEnergy()
    {
        if(fireEnergy < fireEnergyMax)
        {
            fireEnergy += energyReg * Time.deltaTime;
            fireEnergy = Mathf.Clamp(fireEnergy, 0, fireEnergyMax);
        }
    }

    public void ReduceCooldowns(int amount)
    {
        foreach (Ability ability in abilities)
        {
            ability.ReduceCooldown(amount);
        }

        UpdateAbilityBars();
        UpdateChargeCounters();
    }

    void UpdateChargeCounters()
    {
        chargeCounter1.text = abilities[0].currentCharges + "/" + abilities[0].maxCharges;
        chargeCounter2.text = abilities[1].currentCharges + "/" + abilities[1].maxCharges;
        chargeCounter3.text = abilities[2].currentCharges + "/" + abilities[2].maxCharges;
        chargeCounter4.text = abilities[3].currentCharges + "/" + abilities[3].maxCharges;
    }

    void UpdateAbilityBars()
    {
        abilityBar1.transform.localScale = new Vector3((float)abilities[0].currentCooldown / (float)abilities[0].maxCooldown, 1, 1);
        abilityBar2.transform.localScale = new Vector3((float)abilities[1].currentCooldown / (float)abilities[1].maxCooldown, 1, 1);
        abilityBar3.transform.localScale = new Vector3((float)abilities[2].currentCooldown / (float)abilities[2].maxCooldown, 1, 1);
        abilityBar4.transform.localScale = new Vector3((float)abilities[3].currentCooldown / (float)abilities[3].maxCooldown, 1, 1);
    }

    void SetIcons()
    {
        for(int i = 0; i < icons.Length; i++)
        {
            icons[i].sprite = abilities[i].icon;
        }
    }
}
