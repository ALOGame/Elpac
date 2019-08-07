using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSupply : Appliance
{
    public Sprite spritePowerOn, spritePowerOff;

    private SpriteRenderer spriteRenderer;

    private bool generatingPower;
    private Energy producedElectricity;

    private void Start()
    {
        canInteractOnPlay = true;
        producedElectricity = new Electricity(new Vector2Int(info.gridPos.x, info.gridPos.y));
        producedEnergies.Add(producedElectricity);

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
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
        spriteRenderer.sprite = spritePowerOff;
    }

    private void TurnPowerOn()
    {
        producedElectricity.Spread();
        spriteRenderer.sprite = spritePowerOn;
    }
}
