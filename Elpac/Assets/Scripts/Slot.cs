using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private Appliance appliance;
    private Wire wire;

    public bool isOccupied { get { return appliance != null; } }

    public void SetAppliance(Appliance appliance)
    {
        this.appliance = appliance;
    }

    public void SetWire(Wire wire)
    {
        this.wire = wire;
    }

    public void PowerOn()
    {
        wire.PowerOn();
        appliance.PowerOn();
    }

    public void PowerOff()
    {
        wire.PowerOff();
        appliance.PowerOff();
    }
}
