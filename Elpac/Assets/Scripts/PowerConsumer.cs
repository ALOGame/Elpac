﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerConsumer : Appliance
{
    private void Start()
    {
        consumerableEnergyType = EnType.Electric;
    }

    private void Update()
    {
        Debug.Log("powering consumer!!!");
    }
}
