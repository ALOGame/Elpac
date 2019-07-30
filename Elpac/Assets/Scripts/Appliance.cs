using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Appliance : MonoBehaviour
{
    public enum Type { PowerSupply, PowerConsumer, VerticalWire, HorizontalWire, Heater, PowerGenerator}
    public ApplianceInfo info;

    public bool powered;

    protected bool interactableOnPlay;

    public bool fixedPosition;
    public bool canInteractOnPlay;

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
    public Appliance.Type type;
    public byte gridX, gridY;
    public bool facingRight;

    public ApplianceInfo(Appliance.Type type, byte gridX, byte gridY, bool facingRight)
    {
        this.type = type;
        this.gridX = gridX;
        this.gridY = gridY;
        this.facingRight = facingRight;
    }
}