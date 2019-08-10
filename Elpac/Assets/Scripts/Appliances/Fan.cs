using System;
using System.Collections.Generic;
using UnityEngine;

public class Fan : Appliance
{
    protected override void Start()
    {
        Direction direction = data.facingRight ? Direction.Right : Direction.Left;
        producedEnergies.Add(new Wind(data.gridPos, direction));
        consumerableEnergyType = EnType.Electric;
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