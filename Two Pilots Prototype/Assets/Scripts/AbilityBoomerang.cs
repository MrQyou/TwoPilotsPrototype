using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class AbilityBoomerang : Ability
{
    GameObject boomerang;

    public override void SetUp()
    {
        this.maxCooldown = 10;
        this.maxCharges = 10;

        this.icon = Resources.Load("Boomerang Icon", typeof(Sprite)) as Sprite;
        boomerang = Resources.Load("Missle", typeof(GameObject)) as GameObject;
    }

    protected override void Execute(Transform trans)
    {
        boomerang = MonoBehaviour.Instantiate(Resources.Load("Missle", typeof(GameObject)) as GameObject);
        MonoBehaviour.Instantiate(boomerang, trans.position, trans.rotation);
    }
}
