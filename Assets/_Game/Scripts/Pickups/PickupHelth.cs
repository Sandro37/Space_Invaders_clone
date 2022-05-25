using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupHelth : Pickup
{
    public override void PickMeUp()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().AddHelth();
        Destroy(gameObject);
    }
}
