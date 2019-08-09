using System;
using System.Collections.Generic;
using UnityEngine;

public class Electricity : Energy
{
    public Electricity (Vector2Int gridPos) : base(EnType.Electric, gridPos) { }

    public override void Spread()
    {
        if (spreaded)
            return;
        spreaded = true;

        Queue<Vector2Int> openSet = new Queue<Vector2Int>();
        List<Vector2Int> closedSet = new List<Vector2Int>();
        openSet.Enqueue(gridPos);

        Direction wireDirection;
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
            EnergyTrail newTrail = new EnergyTrail(position, energyType, Direction.None, this);
            trails.Add(newTrail);
            SlotGrid.AddEnergyTrail(position.x, position.y, newTrail);
            Debug.Log("added trail: " + position);
        }
    }

    private List<Vector2Int> GetConnectedSlotPositions(Vector2Int position, Direction wireDirection)
    {
        List<Vector2Int> connectedPositions = new List<Vector2Int>();

        if (wireDirection.HasFlag(Direction.Up))
        {
            Vector2Int newPos = new Vector2Int(position.x, position.y - 1);
            connectedPositions.Add(newPos);
        }
        if (wireDirection.HasFlag(Direction.Down))
        {
            Vector2Int newPos = new Vector2Int(position.x, position.y + 1);
            connectedPositions.Add(newPos);
        }
        if (wireDirection.HasFlag(Direction.Right))
        {
            Vector2Int newPos = new Vector2Int(position.x + 1, position.y);
            connectedPositions.Add(newPos);
        }
        if (wireDirection.HasFlag(Direction.Left))
        {
            Vector2Int newPos = new Vector2Int(position.x - 1, position.y);
            connectedPositions.Add(newPos);
        }

        return connectedPositions;
    }

    public override void UpdateTrail()
    {
        
    }
}
