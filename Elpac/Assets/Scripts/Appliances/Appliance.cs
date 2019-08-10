using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ItemType { UnKnown = 0, PowerSupply = 1, PowerConsumer = 2, VerticalWire = 3, HorizontalWire = 4, Fan = 5, Battery = 6, Heater = 7, Heatsink = 8, LaserGun = 9, LaserFeeder = 10, LaserMirror = 11 }

public abstract class Appliance : MonoBehaviour
{
    public ItemData data;

    public bool fixedPosition;
    public bool canInteractOnPlay;

    protected bool powered;

    protected Direction consumedEnergyDir;
    protected EnType consumerableEnergyType;
    protected List<Energy> producedEnergies;
    protected bool interactableOnPlay;
    protected float chargingTime = 0.6f;

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
        bool canInfluenceSameType = trails.Count > 0 ? trails[0].energy.canInfluenceSameType : true;

        if (canInfluenceSameType)
        {
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
        }

        if (!powered && consumedEnergies.Count() > 0 && (finalDirection != Direction.None || !canInfluenceSameType))
        {
            Invoke("PowerOn", chargingTime);
            consumedEnergyDir = finalDirection;
        }
        else if (powered && consumedEnergies.Count() == 0)
            PowerOff();
        else
            CancelInvokes();
    }

    private void PowerOn()
    {
        OnPowerOn();
        powered = true;
    }
    private void PowerOff()
    {
        OnPowerOff();
        powered = false;
        consumedEnergyDir = Direction.None;
    }
    private void CancelInvokes()
    {
        CancelInvoke();
    }
    protected abstract void OnPowerOn();
    protected abstract void OnPowerOff();
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