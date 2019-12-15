using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MovableEnergy
{
    public Water(Vector2Int gridPos, out IEnumerator updateMethod) : base(EnType.Water, gridPos, new Vector2Int(0, 1), 0.5f, out updateMethod)
    {
        canInfluenceSameType = false;
    }

    public override void Spread()
    {
        base.Spread();
        paths.Add(new EnergyPath(gridPos, movement, this));
    }

    protected override void OnUpdatePeriod()
    {
        for (int i = 0; i < paths.Count; i++)
        {
            Debug.Log(paths[i].powered);
            if (paths[i].powered)
            {
                // Calculate new startPos
                paths[i].MoveStartPos();
            } else
            {
                // Calculate new endPos
                bool pathVanished = false;
                paths[i].MoveEndPos(ref pathVanished);

                if (pathVanished)
                {
                    paths.RemoveAt(i);
                    i--;
                }
            }
        }
    }

    public override void UpdateTrail() { }

    public override void StopSpreading()
    {
        base.StopSpreading();

        // Stupid structs...
        for (int i = 0; i < paths.Count; i++)
        {
            EnergyPath path = paths[i];
            path.powered = false;
            paths[i] = path;
        }
    }
}
