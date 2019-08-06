using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum WireDirection { None = 0, Right = 1, Left = 2, Up = 4, Down = 8 }

public class Slot : MonoBehaviour
{
    private Appliance appliance;
    public WireDirection wireDirection { get; private set; }

    private List<EnergyTrail> energyTrails;

    public bool isOccupied { get { return appliance != null; } }

    private void Awake()
    {
        energyTrails = new List<EnergyTrail>();
    }

    public void SetAppliance(Appliance appliance)
    {
        this.appliance = appliance;
    }

    public void AddWireDirection(WireDirection direction)
    {
        wireDirection |= direction;
    }

    public void AddEnergyTrail(EnergyTrail trail, Energy caller)
    {
        energyTrails.Add(trail);
        UpdateItems(caller);
    }

    public void RemoveEnergyTrail(EnergyTrail trail)
    {
        energyTrails.Remove(trail);
        UpdateItems(null);
    }

    private void UpdateItems(Energy caller)
    {
        List<Energy> updatedEnergies = new List<Energy>();
        if (caller != null)
            updatedEnergies.Add(caller);

        foreach (EnergyTrail trail in energyTrails)
        {
            if (!updatedEnergies.Contains(trail.energy))
            {
                trail.energy.UpdateTrail();
                updatedEnergies.Add(trail.energy);
            }
        }

        appliance?.EnergiesChanges(energyTrails, caller);
    }
}
