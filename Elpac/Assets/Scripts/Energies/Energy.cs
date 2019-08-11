using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum Direction { None = 0, Right = 1, Left = 2, Up = 4, Down = 8 }

public abstract class Energy : MonoBehaviour
{
    public EnType energyType { get; private set; }
    public bool canInfluenceSameType { get; protected set; }
    protected Vector2Int gridPos;
    protected List<EnergyTrail> trails;
    protected List<EnType> affectingEnergies;

    public Energy(EnType type, Vector2Int gridPos)
    {
        energyType = type;
        this.gridPos = gridPos;
        trails = new List<EnergyTrail>();
        affectingEnergies = new List<EnType>();
        canInfluenceSameType = true;
    }

    public abstract void Spread();
    public abstract void UpdateTrail(List<EnergyTrail> trails);
    public virtual void StopSpreading()
    {
        SlotGrid.RemoveEnergyTrails(trails);

        trails.Clear();
    }
}