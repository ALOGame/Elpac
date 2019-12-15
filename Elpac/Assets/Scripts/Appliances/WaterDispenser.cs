using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterDispenser : Appliance
{
    protected override void Start()
    {
        IEnumerator updateMethod;
        producedEnergies.Add(new Water(data.gridPos, out updateMethod));
        StartCoroutine(updateMethod);

        consumerableEnergyType = EnType.Electric;
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
