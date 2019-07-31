using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum WireDirection { Right = 1, Left = 2, Up = 4, Down = 8 }

public class Slot : MonoBehaviour
{
    private Appliance appliance;
    public WireDirection wire { get; private set; }

    public bool isOccupied { get { return appliance != null; } }

    public void SetAppliance(Appliance appliance)
    {
        this.appliance = appliance;
    }

    public void PowerOn()
    {
        appliance.PowerOn();
    }

    public void PowerOff()
    {
        appliance.PowerOff();
    }
}
