using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Energy
{
    protected EnType energyType;
    protected int xPosGrid, yPosGrid;
    protected List<EnergyTrail> trail;

    public Energy(EnType type, int xPosGrid, int yPosGrid)
    {
        energyType = type;
        this.xPosGrid = xPosGrid;
        this.yPosGrid = yPosGrid;
        trail = new List<EnergyTrail>();
    }

    public abstract void Spread();
    public abstract void StopSpreading();
    public abstract void UpdateTrail(); // Cannot be called "Update" because this class inherits from MonoBehaviour and already has "Update" method
}