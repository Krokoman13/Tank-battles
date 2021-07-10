using System;
using System.Drawing;
using System.Collections.Generic;
using GXPEngine;

public abstract class Tank : PhysicsRectangle
{
	public int health;

	protected Vec2 _speed;
	protected readonly Vec2 _power = new Vec2(0.05f, 0);
	protected Vec2 _barrelRotation;

	readonly float _friction = 0.02f;
	int _shootCooldown = 120;
	EasyDraw _barrel = new EasyDraw(90, 20, false);

	int _currentbullet;
	Bullet[] _magazine;

	protected abstract Color tankColor 
	{
		get;
	}

	public Tank(Vec2 pPosition) : base(100, 75, pPosition)
	{
		_barrelRotation = Vec2.GetUnitVectorDeg(0);
		load();

		SetColor(tankColor);
		_barrel.Clear(Color.FromArgb(tankColor.R + 50, tankColor.G + 50, tankColor.B + 50));
		_barrel.SetOrigin(_barrel.width / 5, _barrel.height / 2);
		AddChild(_barrel);

		health = 10;
	}

    private void load()
	{
		_magazine = new Bullet[15];

		for (int i = 0; i < _magazine.Length - 1; i++)
		{
			_magazine[i] = new Bullet();
		}

		_currentbullet = 0;
	}

	private Bullet getBullet(Vec2 bulletPosition, Vec2 bulletVelocity)
	{
		Bullet outPut = _magazine[_currentbullet];
		_currentbullet++;

		if (_currentbullet >= _magazine.Length - 1)
		{
			_currentbullet = 0;
		}

		outPut.position = bulletPosition;
		outPut.velocity = bulletVelocity;

		return outPut;
	}

	public void Update()
	{
		_shootCooldown++;

		_barrel.rotation = _barrelRotation.angleDeg - rotation;

		if (health <= 0)
		{
			SetColor(Color.Gray);
			_barrel.Clear(Color.DarkGray);
		}
        else 
		{
			Controls();
		}
		
		_speed += _speed * -_friction;

		velocity = _speed;
		velocity.angleDeg += rotation;

		Step();
	}

	protected void Shoot()
	{
		if (_shootCooldown > 120)
		{
			Vec2 bulletPosition = new Vec2(_barrel.width * (4f / 5f), 0);
			Vec2 bulletVelocity = new Vec2(7f, 0);

			bulletPosition += position;
			bulletPosition.RotateAroundDegrees(_barrel.rotation + rotation, position);

			bulletVelocity.angleDeg = _barrel.rotation + rotation;

			game.AddChildAt(getBullet(bulletPosition, bulletVelocity), 1);

			_shootCooldown = 0;
		}
	}

	protected abstract void Controls();
}

