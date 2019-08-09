using System;
using System.Collections.Generic;
using UnityEngine;

public class VerticalWire : Wire
{
    public Sprite verticalWireOn;
    public Sprite verticalWireOff;

    public Sprite verticalDecorativeWireOn;
    public Sprite verticalDecorativeWireOff;

    private void Awake()
    {
        bool decorative = false;
        try
        {
            decorative = bool.Parse((string)info.itemData[0]);
        } catch { }

        if (decorative)
        {
            wireOn = verticalDecorativeWireOn;
            wireOff = verticalDecorativeWireOff;
        }
        else
        {
            wireOn = verticalWireOn;
            wireOff = verticalWireOff;
        }
    }
}