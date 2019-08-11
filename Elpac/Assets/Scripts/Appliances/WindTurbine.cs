using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindTurbine : Appliance
{
    protected override void Start()
    {
        consumerableEnergyType = EnType.Wind;
        producedEnergies.Add(new Electricity(data.gridPos));
    }

    protected override void OnPowerOff()
    {
        producedEnergies[0].StopSpreading();
        Debug.Log("wind turbine: stopped spreading");
    }

    protected override void OnPowerOn()
    {
        producedEnergies[0].Spread();
        Debug.Log("wind turbine: spreaded");
    }
}
