using System;
using System.Collections.Generic;
using UnityEngine;

public class Electricity : Energy
{
    private bool spreaded;

    public Electricity (int xPosGrid, int yPosGrid) : base(EnType.Electric, xPosGrid, yPosGrid) { }

    public override void Spread()
    {
        if (spreaded)
            return;
        spreaded = true;

        Queue<Vector2Int> openSet = new Queue<Vector2Int>();
        List<Vector2Int> closedSet = new List<Vector2Int>();
        openSet.Enqueue(new Vector2Int(xPosGrid, yPosGrid));

        WireDirection wireDirection;
        Vector2Int currPos;

        while (openSet.Count > 0)
        {
            currPos = openSet.Dequeue();
            wireDirection = SlotGrid.GetWireDirection(currPos.x, currPos.y);
            closedSet.Add(currPos);

            foreach (Vector2Int position in GetConnectedSlotPositions(currPos, wireDirection))
            {
                if (!closedSet.Contains(position))
                {
                    openSet.Enqueue(position);
                }
            }
        }

        foreach (Vector2Int position in closedSet)
        {
            EnergyTrail newTrail = new EnergyTrail(position, energyType, EnDirection.None, this);
            trails.Add(newTrail);
            SlotGrid.AddEnergyTrailToSlot(position.x, position.y, newTrail, this);
            Debug.Log("added energy");
        }
    }

    private List<Vector2Int> GetConnectedSlotPositions(Vector2Int position, WireDirection wireDirection)
    {
        List<Vector2Int> connectedPositions = new List<Vector2Int>();

        if (wireDirection.HasFlag(WireDirection.Up))
        {
            Vector2Int newPos = new Vector2Int(position.x, position.y - 1);
            connectedPositions.Add(newPos);
        }
        if (wireDirection.HasFlag(WireDirection.Down))
        {
            Vector2Int newPos = new Vector2Int(position.x, position.y + 1);
            connectedPositions.Add(newPos);
        }
        if (wireDirection.HasFlag(WireDirection.Right))
        {
            Vector2Int newPos = new Vector2Int(position.x + 1, position.y);
            connectedPositions.Add(newPos);
        }
        if (wireDirection.HasFlag(WireDirection.Left))
        {
            Vector2Int newPos = new Vector2Int(position.x - 1, position.y);
            connectedPositions.Add(newPos);
        }

        return connectedPositions;
    }

    public override void StopSpreading()
    {
        foreach (EnergyTrail trail in trails)
        {
            SlotGrid.RemoveEnergyTrailFromSlot(trail.gridPos.x, trail.gridPos.y, trail);
        }

        ClearTrailList();

        spreaded = false;
    }

    public override void UpdateTrail()
    {
        Debug.Log("trail updated");
    }
}
