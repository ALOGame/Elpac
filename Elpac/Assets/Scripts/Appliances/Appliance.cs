using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ItemType { unknown, PowerSupply, PowerConsumer, VerticalWire, HorizontalWire }

public abstract class Appliance : MonoBehaviour
{
    public ItemInfo info;
    
    public bool powered;

    protected EnType consumerableEnergyType;
    protected List<Energy> producedEnergies;
    protected bool interactableOnPlay;

    public bool fixedPosition;
    public bool canInteractOnPlay;

    private void Awake()
    {
        producedEnergies = new List<Energy>();
    }

    public void EnergiesChanges(List<EnergyTrail> trails, Energy caller)
    {
        if (producedEnergies.Contains(caller))
            return;

        IEnumerable<EnergyTrail> consumedEnergies = trails.Where(trail => trail.type == consumerableEnergyType);

        if (consumedEnergies.Count() == 0)
            return;

        EnDirection finalDirection = EnDirection.None;
        foreach (EnergyTrail trail in consumedEnergies)
        {
            finalDirection |= trail.direction;
        }

        if (finalDirection.HasFlag(EnDirection.Right) && finalDirection.HasFlag(EnDirection.Left))
        {
            finalDirection &= ~(EnDirection.Right | EnDirection.Left);
        }
        if (finalDirection.HasFlag(EnDirection.Up) && finalDirection.HasFlag(EnDirection.Down))
        {
            finalDirection &= ~(EnDirection.Up | EnDirection.Down);
        }

        if (finalDirection != EnDirection.None || (consumerableEnergyType == EnType.Electric && consumedEnergies.Count(trail => trail.type == EnType.Electric) > 0))
        {
            Debug.Log("consuming energy");
        }
    }

    public virtual void InteractOnPlay() { }
}

[System.Serializable]
public struct ItemInfo
{
    public ItemType type;
    public int gridX, gridY;
    public bool facingRight;

    public ItemInfo(ItemType type, int gridX, int gridY, bool facingRight)
    {
        this.type = type;
        this.gridX = gridX;
        this.gridY = gridY;
        this.facingRight = facingRight;
    }
}