using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSupply : Appliance
{
    private bool generatingPower;
    private Energy producedElectricity;

    private void Start()
    {
        canInteractOnPlay = true;
        producedElectricity = new Electricity(info.gridX, info.gridY);
        producedEnergies.Add(producedElectricity);
    }

    private void Update()
    {
        if (!GameManager.gameRunning)
            return;

        if (generatingPower)
        {
            Debug.Log("generating power " + producedEnergies.Count);
        }
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
        producedElectricity.StopSpreading();
    }

    private void TurnPowerOn()
    {
        producedElectricity.Spread();
    }
}
