using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatSink : Appliance
{
    protected override void Start()
    {
        consumerableEnergyType = EnType.Heat;
        producedEnergies.Add(new Electricity(data.gridPos));
    }

    protected override void OnPowerOn()
    {
        producedEnergies[0].Spread();
    }

    protected override void OnPowerOff()
    {
        producedEnergies[0].StopSpreading();
    }
}
