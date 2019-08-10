using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserFeeder : Appliance
{
    protected override void Start()
    {
        consumerableEnergyType = EnType.Laser;
        producedEnergies.Add(new Electricity(data.gridPos));
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
