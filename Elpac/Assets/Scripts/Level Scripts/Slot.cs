using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slot : MonoBehaviour
{
    private Appliance appliance;
    private Wire[] wires;

    public Direction wireDirection { get; private set; }

    public  List<EnergyTrail> energyTrails { get; private set; }

    public bool isOccupied { get { return appliance != null; } }

    private void Awake()
    {
        energyTrails = new List<EnergyTrail>();
        wires = new Wire[2];
    }

    public void SetAppliance(Appliance appliance)
    {
        this.appliance = appliance;
    }

    public void AddWire(Wire wire)
    {
        if (wires[0] == null)
            wires[0] = wire;
        else
            wires[1] = wire;
    }

    public void AddWireDirection(Direction direction)
    {
        wireDirection |= direction;
    }

    public void AddEnergyTrail(EnergyTrail trail)
    {
        energyTrails.Add(trail);
    }

    public void RemoveEnergyTrail(EnergyTrail trail)
    {
        energyTrails.Remove(trail);
    }

    public void UpdateItems(Energy caller)
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

        foreach (Wire wire in wires)
        {
            wire?.EnergiesChanged(energyTrails);
        }
    }
}
