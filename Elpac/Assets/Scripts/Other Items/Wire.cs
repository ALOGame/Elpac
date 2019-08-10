using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class Wire : MonoBehaviour
{
    protected Sprite wireOn;
    protected Sprite wireOff;

    private SpriteRenderer spriteRenderer;

    [HideInInspector]
    public ItemData info;
    [HideInInspector]
    public bool fixedPosition;
    [HideInInspector]
    public bool horizontal;

    private void Start()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        PowerOff();
    }

    public void EnergiesChanged(List<EnergyTrail> energies)
    {
        if (energies.Count(trail => trail.type == EnType.Electric) == 0)
            PowerOff();
        else
            PowerOn();
    }

    private void PowerOff()
    {
        spriteRenderer.sprite = wireOff;
    }

    private void PowerOn()
    {
        spriteRenderer.sprite = wireOn;
    }
}