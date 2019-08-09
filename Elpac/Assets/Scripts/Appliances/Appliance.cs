using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ItemType { unknown, PowerSupply, PowerConsumer, VerticalWire, HorizontalWire, VerticalDecorativeWire, HorizontalDecorativeWire}

public abstract class Appliance : MonoBehaviour
{
    public ItemInfo info;
    
    protected bool powered;

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
            powered = true;
        }
        else
        {
            powered = false;
        }

        PowerStateChanged();
    }

    protected virtual void PowerStateChanged() { }
    public virtual void InteractOnPlay() { }
}

[System.Serializable]
public struct ItemInfo
{
    public bool loaded;
    public ItemType type;
    public Vector2Int gridPos;
    public bool facingRight;
    public object[] itemData;

    public ItemInfo(bool loaded, ItemType type, Vector2Int gridPos, bool facingRight, object[] itemData)
    {
        this.loaded = loaded;
        this.type = type;
        this.gridPos = gridPos;
        this.facingRight = facingRight;
        this.itemData = itemData;
    }
}