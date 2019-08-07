using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Flags]
public enum WireDirection { None = 0, Right = 1, Left = 2, Up = 4, Down = 8 }

public class Wire : MonoBehaviour
{
    public Sprite horizontalWireOn;
    public Sprite horizontalWireOff;
    public Sprite verticalWireOn;
    public Sprite verticalWireOff;

    [HideInInspector]
    public ItemInfo info;
    [HideInInspector]
    public bool fixedPosition;
    [HideInInspector]
    public bool horizontal;

    public void EnergiesChanged(List<Energy> energies)
    {
        if (energies.Count(energy => energy.energyType == EnType.Electric) == 0)
            PowerOff();
        else
            PowerOn();
    }

    private void PowerOff()
    {

    }

    private void PowerOn()
    {

    }
}