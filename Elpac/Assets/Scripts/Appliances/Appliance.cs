using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ItemType { UnKnown, PowerSupply, PowerConsumer, VerticalWire, HorizontalWire, Fan}

public abstract class Appliance : MonoBehaviour
{
    public ItemData data;
    
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
        {
            PowerOn();
        }
        else
        {
            PowerOff();
        }
    }

    protected virtual void PowerOn() { }
    protected virtual void PowerOff() { }
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