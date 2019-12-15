﻿using System;
using System.Collections.Generic;
using UnityEngine;

public class Wind : Energy
{
    private Direction spreadDirection;

    public Wind(Vector2Int gridPos, Direction spreadDirection) : base(EnType.Wind, gridPos)
    {
        this.spreadDirection = spreadDirection;
    }

    public override void Spread()
    {
        int moveX = 0;
        int moveY = 0;
        if (spreadDirection == Direction.Right)
            moveX = 1;
        else if (spreadDirection == Direction.Left)
            moveX = -1;

        if (spreadDirection == Direction.Down)
            moveY = 1;
        else if (spreadDirection == Direction.Up)
            moveY = -1;

        Vector2Int trailPos = new Vector2Int(gridPos.x, gridPos.y);

        do
        {
            trailPos.x += moveX;
            trailPos.y += moveY;

            EnergyTrail trail = new EnergyTrail(trailPos, EnType.Wind, spreadDirection, this);
            trails.Add(trail);
        } while (!SlotGrid.IsSlotOccupied(trailPos.x, trailPos.y));

        SlotGrid.AddEnergyTrails(trails);
    }

    public override void UpdateTrail()
    {
        
    }
}