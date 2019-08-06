using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Energy
{
    public EnType energyType;
    protected int xPosGrid, yPosGrid;
    protected List<EnergyTrail> trails;
    protected List<Energy> influatingEnergies; // We couldn't find the proper variable name for it. It's a list of energies that influence this energy

    public Energy(EnType type, int xPosGrid, int yPosGrid)
    {
        energyType = type;
        this.xPosGrid = xPosGrid;
        this.yPosGrid = yPosGrid;
        trails = new List<EnergyTrail>();
        influatingEnergies = new List<Energy>();
    }

    protected void ClearTrailList()
    {
        trails.Clear();
    }

    public abstract void Spread();
    public abstract void StopSpreading();
    public abstract void UpdateTrail();
}