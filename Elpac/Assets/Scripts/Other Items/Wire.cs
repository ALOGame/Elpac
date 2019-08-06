using System;
using System.Collections.Generic;
using UnityEngine;

[Flags]
public enum WireDirection { None = 0, Right = 1, Left = 2, Up = 4, Down = 8 }

public class Wire : MonoBehaviour
{
    public ItemInfo info;

    public bool fixedPosition;
    public bool horizontal;
}