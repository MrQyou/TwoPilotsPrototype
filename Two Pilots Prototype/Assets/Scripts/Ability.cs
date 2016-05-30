using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Ability
{
    public int maxCooldown = 10; 
    public int maxCharges = 10;
    public int currentCooldown = 5;
    public int currentCharges = 5;
    public Sprite icon;

    public virtual void SetUp() { }

    public void Use(Transform trans)
    {
        if (currentCharges > 0)
        {
            currentCharges--;
            Execute(trans);
        }
    }

    protected virtual void Execute(Transform trans) { }

    public void ReduceCooldown(int amount)
    {
        currentCooldown += amount;

        if (currentCooldown >= maxCooldown && currentCharges < maxCharges)
        {
            currentCooldown -= maxCooldown;
            currentCharges++;
        }

        currentCooldown = Mathf.Clamp(currentCooldown, 0, maxCooldown);
    }
}
