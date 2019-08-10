using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class LaserMirror : Appliance
{
    private bool facingUp;

    private Laser laserUpDown;
    private Laser laserLeftRight;

    protected override void Start()
    {
        try
        {
            facingUp = bool.Parse((string)data.itemData[0]);
        } catch
        {
            StringBuilder builder = new StringBuilder();
            foreach (object param in data.itemData)
                builder.Append(" ").Append(param.ToString());

            Debug.Log("Erroneous LaserMirror data - " + builder.ToString());
        }

        consumerableEnergyType = EnType.Laser;

        laserUpDown = new Laser(data.gridPos, facingUp ? Direction.Up : Direction.Down);
        laserLeftRight = new Laser(data.gridPos, data.facingRight ? Direction.Right : Direction.Left);

        producedEnergies.Add(laserUpDown);
        producedEnergies.Add(laserLeftRight);
    }

    protected override void OnPowerOn()
    {
        if (consumedEnergyDir == Direction.Up || consumedEnergyDir == Direction.Down)
            ReflectLeftRight();
        else
            ReflectUpDown();
    }

    private void ReflectUpDown()
    {
        laserUpDown.Spread();
    }

    private void ReflectLeftRight()
    {
        laserLeftRight.Spread();
    }

    protected override void OnPowerOff()
    {
        laserUpDown.StopSpreading();
        laserLeftRight.StopSpreading();
    }
}
