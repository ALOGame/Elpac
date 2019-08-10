using System;
using System.Collections.Generic;
using UnityEngine;

public class Heat : Energy
{
    private int power;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="gridPos"></param>
    /// <param name="power">Range from 1 to maximum size of int</param>
    public Heat(Vector2Int gridPos, int power) : base(EnType.Heat, gridPos)
    {
        canInfluenceSameType = false;
        this.power = power;
    }
    public override void Spread()
    {
        List<EnergyTrail> trails = new List<EnergyTrail>();

        int xPos, yPos;
        for (int i = -power; i <= power; i++)
        {
            for (int j = -power; j <= power; j++)
            {
                xPos = gridPos.x + i;
                yPos = gridPos.y + j;
                if (SlotGrid.PositionInsideGrid(xPos, yPos))
                {
                    EnergyTrail trail = new EnergyTrail(new Vector2Int(xPos, yPos), EnType.Heat, Direction.None, this);
                    this.trails.Add(trail);
                    trails.Add(trail);
                }
            }
        }

        SlotGrid.AddEnergyTrails(trails);
    }

    public override void UpdateTrail(List<EnergyTrail> trails) { }
}