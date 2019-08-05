using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ItemType { PowerSupply, PowerConsumer, VerticalWire, HorizontalWire }

public abstract class Appliance : MonoBehaviour
{
    public ItemInfo info;
    
    public bool powered;

    protected EnType consumerableEnergyType;
    protected List<Energy> energies;
    protected bool interactableOnPlay;

    public bool fixedPosition;
    public bool canInteractOnPlay;

    private void Awake()
    {
        energies = new List<Energy>();
    }

    public void EnergiesChanges(List<EnergyTrail> trails)
    {
        IEnumerable<EnergyTrail> consumedEnergies = trails.Where(trail => (trail.Type == consumerableEnergyType));

        EnDirection finalDirection = EnDirection.None;
        foreach (EnergyTrail trail in consumedEnergies)
        {
            finalDirection |= trail.Direction;
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