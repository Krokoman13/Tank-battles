using System;
using System.Drawing;
using System.Collections.Generic;
using GXPEngine;

public class PlayerTank : Tank
{
    protected override Color tankColor
	{
		get { return Color.DarkGreen; }
	}

    public PlayerTank(Vec2 pPosition) : base(pPosition)
	{
	}

	protected override void Controls()
	{
		if (Input.GetKey(Key.A))
		{
			vecRotation.RotateDegrees(-1f * _speed.length);
		}
		if (Input.GetKey(Key.D))
		{
			vecRotation.RotateDegrees(1f * _speed.length);
		}
		if (Input.GetKey(Key.W))
		{
			_speed += _power;
		}
		if (Input.GetKey(Key.S))
		{
			_speed -= _power;
		}

		Vec2 toMouse = _myGame.mouse.position - position;
		_barrelRotation.RotateTowardsDegrees(toMouse.angleDeg, 2f);

		if (Input.GetMouseButtonDown(0))
		{
			Shoot();
		}
	}
}

