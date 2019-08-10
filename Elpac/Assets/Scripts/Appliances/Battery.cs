using System;
using System.Collections.Generic;
using UnityEngine;

public class Battery : Appliance
{
    protected override void Start()
    {
        consumerableEnergyType = EnType.Electric;
        Electricity electricity = new Electricity(data.gridPos);
        producedEnergies.Add(electricity);
    }

    protected override void OnPowerOn()
    {
        Debug.Log("battery powered");
        producedEnergies[0].StopSpreading();
    }

    protected override void OnPowerOff()
    {
        Debug.Log("battery unpowered");
        if (powered)
        {
            producedEnergies[0].Spread();
            Debug.Log("battery producing electricity");
            //producedEnergies[0].StopSpreading();
            //Debug.Log("forced to stop producing");
        }
    }
}