using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnType { Electric, Light, Wind, Laser, Heat, Steam, Water, Mechanical }
[Flags]
public enum EnDirection { None = 0, Right = 1, Left = 2, Up = 4, Down = 8 }

public struct EnergyTrail
{
    public Vector2Int gridPos { get; private set; }
    public EnType type { get; private set; }
    public EnDirection direction { get; private set; }
    public Energy energy { get; private set; }
    

    public EnergyTrail(Vector2Int gridPos, EnType type, EnDirection direction, Energy energy)
    {
        this.gridPos = gridPos;
        this.type = type;
        this.direction = direction;
        this.energy = energy;
    }
}
