using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Energy
{
    public EnType energyType { get; private set; }
    public bool canInfluenceSameType { get; protected set; }
    protected Vector2Int gridPos;
    protected List<EnergyTrail> trails;
    protected List<EnType> affectingEnergies; // Types of energies that affect this energy

    public Energy(EnType type, Vector2Int gridPos)
    {
        energyType = type;
        this.gridPos = gridPos;
        trails = new List<EnergyTrail>();
        affectingEnergies = new List<EnType>();
        canInfluenceSameType = true;
    }

    public abstract void Spread();
    public abstract void UpdateTrail();
    public virtual void UpdatePath() { }
    public virtual void StopSpreading()
    {
        SlotGrid.RemoveEnergyTrails(trails);

        trails.Clear();
    }
}