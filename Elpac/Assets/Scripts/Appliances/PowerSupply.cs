using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerSupply : Appliance
{
    public Sprite spritePowerOn, spritePowerOff;

    private SpriteRenderer spriteRenderer;

    private bool generatingPower;
    private Energy producedElectricity;

    protected override void Start()
    {
        canInteractOnPlay = true;
        producedElectricity = new Electricity(new Vector2Int(data.gridPos.x, data.gridPos.y));
        producedEnergies.Add(producedElectricity);

        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    private void Update()
    {
        if (!GameManager.gameRunning)
            return;
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

    protected override void OnPowerOn() { } // Unused in this class

    protected override void OnPowerOff() { } // Unused in this class
}
