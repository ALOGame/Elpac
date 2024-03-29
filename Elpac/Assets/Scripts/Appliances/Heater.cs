﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heater : Appliance
{
    protected override void Start()
    {
        consumerableEnergyType = EnType.Electric;
        producedEnergies.Add(new Heat(data.gridPos, 2));
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
