using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ApplianceType { PowerSupply, PowerConsumer, VerticalWire, HorizontalWire }

public abstract class Appliance : MonoBehaviour
{
    public ApplianceInfo info;

    public bool powered;

    protected List<Energy> energies;
    protected bool interactableOnPlay;

    public bool fixedPosition;
    public bool canInteractOnPlay;

    private void Awake()
    {
        energies = new List<Energy>();
    }

    public void PowerOn()
    {
        powered = true;
        OnPowered(powered);
    }

    public void PowerOff()
    {
        powered = false;
        OnPowered(powered);
    }

    protected abstract void OnPowered(bool powered);

    public virtual void InteractOnPlay() { }
}

[System.Serializable]
public struct ApplianceInfo
{
    public ApplianceType type;
    public int gridX, gridY;
    public bool facingRight;

    public ApplianceInfo(ApplianceType type, int gridX, int gridY, bool facingRight)
    {
        this.type = type;
        this.gridX = gridX;
        this.gridY = gridY;
        this.facingRight = facingRight;
    }
}