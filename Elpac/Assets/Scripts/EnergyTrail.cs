using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnType { Electric, Light, Wind, Laser, Heat, Steam, Water }
[Flags]
public enum EnDirection { Right = 1, Left = 2, Up = 4, Down = 8 }

public struct EnergyTrail
{
    public EnType Type { get; private set; }
    public EnDirection Direction { get; private set; }
    public Appliance Parent { get; private set; }
}
