using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : Appliance
{
    protected override void Start()
    {
        consumerableEnergyType = EnType.Electric;
    }
    protected override void OnPowerOn()
    {
        Debug.Log("PowerConsumer: powered");
    }

    protected override void OnPowerOff()
    {
        Debug.Log("PowerConsumer: unpowered");
    }
}
