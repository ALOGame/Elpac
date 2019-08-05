using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnType { Electric, Light, Wind, Laser, Heat, Steam, Water }
[Flags]
public enum EnDirection { None = 0, Right = 1, Left = 2, Up = 4, Down = 8 }

public struct EnergyTrail
{
    public EnType Type { get; private set; }
    public EnDirection Direction { get; private set; }
    public Energy Energy { get; private set; }

    public EnergyTrail(EnType type, EnDirection direction, Energy energy)
    {
        Type = type;
        Direction = direction;
        Energy = energy;
    }
}
