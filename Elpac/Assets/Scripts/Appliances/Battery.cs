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
        producedEnergies[0].StopSpreading();
        Debug.Log("battery: stopped spreading");
    }

    protected override void OnPowerOff()
    {
        Invoke("SpreadElectricity", chargingTime);
    }

    private void SpreadElectricity()
    {
        if (powered) // Battery got powered again -> stop producing energy
            return;
        producedEnergies[0].Spread();
        Debug.Log("bettery: started spreging");
    }
}