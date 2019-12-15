using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovableEnergy : Energy
{
    protected new List<EnergyTrail> trails { get => throw new MissingMemberException(); }
    protected List<EnergyPath> paths;
    protected Vector2Int movement;
    protected bool spreading;
    protected float updatePeriod;

    public MovableEnergy(EnType type, Vector2Int gridPos, Vector2Int movement, float updatePeriod, out IEnumerator updateMethod) : base(type, gridPos)
    {
        updateMethod = UpdateTrailPaths();
        paths = new List<EnergyPath>();
        this.movement = movement;
        this.updatePeriod = updatePeriod;
    }

    private IEnumerator UpdateTrailPaths()
    {
        while (true)
        {
            OnUpdatePeriod();

            yield return new WaitForSeconds(updatePeriod);
        }
    }

    protected abstract void OnUpdatePeriod();

    public override void Spread()
    {
        spreading = true;
    }

    public override void StopSpreading()
    {
        spreading = false;
    }
}

public class EnergyPath
{
    private Vector2Int gridPosStart;
    private Vector2Int gridPosEnd;
    private Vector2Int movement;
    private Energy creator;
    private Queue<EnergyTrail> path;

    public bool powered { get; set; }

    public EnergyPath(Vector2Int gridPos, Vector2Int movement, Energy creator)
    {
        gridPosStart = gridPosEnd = gridPos;
        this.movement = movement;
        this.creator = creator;
        path = new Queue<EnergyTrail>();
        powered = true;
    }

    public void MoveStartPos()
    {
        Vector2Int newGridPosStart = gridPosStart + movement;

        if (!SlotGrid.IsSlotOccupied(newGridPosStart.x, newGridPosStart.y))
        {
            EnergyTrail trail = new EnergyTrail(newGridPosStart, creator.energyType, CalculateDirection(gridPosStart, newGridPosStart), creator);
            path.Enqueue(trail);
            SlotGrid.AddEnergyTrail(trail);
            gridPosStart.Set(newGridPosStart.x, newGridPosStart.y);
        }
    }

    public void MoveEndPos(ref bool vanished)
    {
        Vector2Int newGridPosEnd = gridPosEnd + movement;

        EnergyTrail s = path.Dequeue();
        SlotGrid.RemoveEnergyTrail(s);

        if (gridPosStart == newGridPosEnd)
        {
            vanished = true;
            return;
        }

        gridPosEnd = newGridPosEnd;
    }

    private Direction CalculateDirection(Vector2Int from, Vector2Int to)
    {
        float angle = Vector2.SignedAngle(Vector2.up, to - from);

        if (angle < 45 && angle >= -45)
            return Direction.Up;
        if (angle < -45 && angle >= -135)
            return Direction.Right;
        if (angle >= 45 && angle < 135)
            return Direction.Left;
        return Direction.Down;
    }
}