using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGun : Appliance
{
    protected override void Start()
    {
        consumerableEnergyType = EnType.Electric;
        producedEnergies.Add(new Laser(data.gridPos, data.facingRight ? Direction.Right : Direction.Left));
        chargingTime = 0.1f;
    }

    protected override void OnPowerOff()
    {
        producedEnergies[0].StopSpreading();
    }

    protected override void OnPowerOn()
    {
        producedEnergies[0].Spread();
    }
}
