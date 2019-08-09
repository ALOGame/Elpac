using System;
using System.Collections.Generic;
using UnityEngine;

public class Steam : Energy
{


    public Steam(Vector2Int gridPos) : base(EnType.Steam, gridPos)
    {
        affectingEnergies.Add(EnType.Wind);
    }

    public override void Spread()
    {
        if (spreaded)
            return;
    }

    public override void UpdateTrail()
    {
        
    }

    public void UpdateTrailPath()
    {

    }
}