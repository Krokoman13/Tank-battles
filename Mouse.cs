using System;
using GXPEngine;

public struct Mouse
{
    public Vec2 position;
    public Vec2 oldPosition;

    public Vec2 difference
    {
        get { return oldPosition - position; }
    }

    public void Update()
    {
        oldPosition = position;
        position.SetXY(Input.mouseX, Input.mouseY);
    }
}

