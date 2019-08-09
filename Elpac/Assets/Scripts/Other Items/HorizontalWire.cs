using System;
using System.Collections.Generic;
using UnityEngine;

public class HorizontalWire : Wire
{
    public Sprite horizontalWireOn;
    public Sprite horizontalWireOff;

    public Sprite horizontalDecorativeWireOn;
    public Sprite horizontalDecorativeWireOff;

    private void Awake()
    {
        horizontal = true;

        bool decorative = false;

        try
        {
            decorative = bool.Parse((string)info.itemData[0]);
        }
        catch { }

        if (decorative)
        {
            wireOn = horizontalDecorativeWireOn;
            wireOff = horizontalDecorativeWireOff;
        }
        else
        {
            wireOn = horizontalWireOn;
            wireOff = horizontalWireOff;
        }
    }
}