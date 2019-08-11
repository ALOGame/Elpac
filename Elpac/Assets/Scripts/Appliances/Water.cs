using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : Energy
{
    private int gravity;
    private Vector2Int headOfTrail;

    public Water(Vector2Int gridPos) : base(EnType.Water, gridPos) { }

    public override void Spread()
    {
        
    }

    private IEnumerator MoveHeadOfTrail()
    {

    }

    public override void UpdateTrail(List<EnergyTrail> trails)
    {
        
    }

    public override void StopSpreading()
    {
        base.StopSpreading();

    }
}
