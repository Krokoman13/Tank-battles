using System;
using System.Drawing;
using System.Collections.Generic;
using GXPEngine;

class EnemyTank : Tank
{
	Vec2 _target;
	int _idleTimer;
	int direction;

	protected override Color tankColor
	{
		get { return Color.DarkRed; }
	}

	public EnemyTank(Vec2 pPosition) : base(pPosition)
	{
		_target = pPosition;
	}

	protected override void Controls()
	{
		_idleTimer++;

		if (_idleTimer > 150)
		{
			pickTarget();
			direction = Utils.Random(1, 4);
			_idleTimer = 0;
		}

		Vec2 toTarget = _target - position;

		switch (direction)
		{
			case 1:
				vecRotation.RotateTowardsDegrees(toTarget.angleDeg, 1f * _speed.length);
				_speed = _power * 50;
				break;

			case 2:
				vecRotation.RotateTowardsDegrees(toTarget.angleDeg - 180f, 1f * _speed.length);
				_speed = _power * -50;
				break;

			default:
				Shoot();
				break;	
		}

		Vec2 lead = _myGame.player.position + _myGame.player.velocity * 10f;
		Vec2 toPlayer = lead - position;
		_barrelRotation.RotateTowardsDegrees(toPlayer.angleDeg, 1f);
	}

	private void pickTarget()
	{
		_target.SetXY(Utils.Random(75, game.width - 75), Utils.Random(75, game.height - 75));
		//_target = _myGame.mouse.position;
	}

}

