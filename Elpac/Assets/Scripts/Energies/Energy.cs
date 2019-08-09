using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum Direction { None = 0, Right = 1, Left = 2, Up = 4, Down = 8 }

public abstract class Energy
{
    public EnType energyType;
    protected Vector2Int gridPos;
    protected List<EnergyTrail> trails;
    protected List<EnType> affectingEnergies;
    protected bool spreaded;

    public Energy(EnType type, Vector2Int gridPos)
    {
        energyType = type;
        this.gridPos = gridPos;
        trails = new List<EnergyTrail>();
        affectingEnergies = new List<EnType>();
    }

    public abstract void Spread();
    public abstract void UpdateTrail();
    public void StopSpreading()
    {
        foreach (EnergyTrail trail in trails)
        {
            SlotGrid.RemoveEnergyTrail(trail.gridPos.x, trail.gridPos.y, trail);
        }

        trails.Clear();

        spreaded = false;
    }
}