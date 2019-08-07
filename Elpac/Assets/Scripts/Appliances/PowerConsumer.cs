using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerConsumer : Appliance
{
    protected override void PowerStateChanged()
    {
        if (powered)
        {
            PowerOn();
        } else
        {
            PowerOff();
        }
    }

    private void PowerOn()
    {
        Debug.Log("power consumer powered");
    }

    private void PowerOff()
    {
        Debug.Log("power consumer unpowered");
    }
}
