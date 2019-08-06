using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Energy
{
    public EnType energyType;
    protected Vector2Int gridPos;
    protected List<EnergyTrail> trails;
    protected List<Energy> affectingEnergies;

    public Energy(EnType type, Vector2Int gridPos)
    {
        energyType = type;
        this.gridPos = gridPos;
        trails = new List<EnergyTrail>();
        affectingEnergies = new List<Energy>();
    }

    protected void ClearTrailList()
    {
        trails.Clear();
    }

    public abstract void Spread();
    public abstract void StopSpreading();
    public abstract void UpdateTrail();
}