using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ItemType { UnKnown = 0, PowerSupply = 1, PowerConsumer = 2, VerticalWire = 3, HorizontalWire = 4, Fan = 5, Battery = 6}

public abstract class Appliance : MonoBehaviour
{
    public ItemData data;

    public bool fixedPosition;
    public bool canInteractOnPlay;

    protected bool powered;

    protected EnType consumerableEnergyType;
    protected List<Energy> producedEnergies;
    protected bool interactableOnPlay;
    protected uint chargingTime;

    private void Awake()
    {
        producedEnergies = new List<Energy>();
    }

    public void EnergiesChanges(List<EnergyTrail> trails, Energy caller)
    {
        if (producedEnergies.Contains(caller))
            return;

        IEnumerable<EnergyTrail> consumedEnergies = trails.Where(trail => trail.type == consumerableEnergyType);

        Direction finalDirection = Direction.None;
        foreach (EnergyTrail trail in consumedEnergies)
        {
            finalDirection |= trail.direction;
        }

        if (finalDirection.HasFlag(Direction.Right) && finalDirection.HasFlag(Direction.Left))
        {
            finalDirection &= ~(Direction.Right | Direction.Left);
        }
        if (finalDirection.HasFlag(Direction.Up) && finalDirection.HasFlag(Direction.Down))
        {
            finalDirection &= ~(Direction.Up | Direction.Down);
        }

        if (finalDirection != Direction.None || (consumerableEnergyType == EnType.Electric && consumedEnergies.Count(trail => trail.type == EnType.Electric) > 0)) // Electric energy has EnDirection.None
            powered = true;
        else if (powered)
            powered = false;
    }

    protected virtual void OnPowerOn()
    {
        powered = true;
    }
    protected virtual void OnPowerOff()
    {
        powered = false;
    }
    public virtual void InteractOnPlay() { }
    protected abstract void Start(); // Forcing inhereted members to implement Start method so they won't forget to inicialize consumable energy type and producing energies
}

public struct ItemData
{
    public bool loaded;
    public ItemType type;
    public Vector2Int gridPos;
    public bool facingRight;
    public object[] itemData;

    public ItemData(bool loaded, ItemType type, Vector2Int gridPos, bool facingRight, object[] itemData)
    {
        this.loaded = loaded;
        this.type = type;
        this.gridPos = gridPos;
        this.facingRight = facingRight;
        this.itemData = itemData;
    }
}