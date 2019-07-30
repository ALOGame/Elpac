using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSupply : Appliance
{
    private bool generatingPower;

    private void Start()
    {
        canInteractOnPlay = true;
    }

    private void Update()
    {
        if (!GameManager.gameRunning)
            return;
    }

    public override void InteractOnPlay()
    {
        if (generatingPower)
            SlotGrid.instance.PowerSlotOff(info.gridX, info.gridY);
        else
            SlotGrid.instance.PowerSlotOn(info.gridX, info.gridY);
    }

    protected override void OnPowered(bool powered)
    {}
}
