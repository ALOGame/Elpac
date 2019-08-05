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

        if (generatingPower)
            Debug.Log("generating power " + energies.Count);
    }

    public override void InteractOnPlay()
    {
        if (generatingPower)
            TurnPowerOff();
        else
            TurnPowerOn();

        generatingPower = !generatingPower;
    }

    private void TurnPowerOff()
    {
        energies.Clear();
    }

    private void TurnPowerOn()
    {
        energies.Add(new Electricity(info.gridX, info.gridY));
    }
}
