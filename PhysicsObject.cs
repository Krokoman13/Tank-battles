using System;
using System.Drawing;
using GXPEngine;

//------------------------------------------------------------------------------------------------------------------------
//														PhysicsObject
//------------------------------------------------------------------------------------------------------------------------
/// <summary>
/// A class disgined to apply physics on
/// </summary>

public abstract class PhysicsObject : EasyDraw
{
	public Vec2 position;
	public Vec2 vecRotation;

	protected MyGame _myGame
	{
		get { return (MyGame)game; }
	}

	public PhysicsObject(int width, int height, Vec2 pPosition) : base(width, height, false)
	{
		position = pPosition;
		vecRotation = Vec2.GetUnitVectorDeg(0);

		SetOrigin(width/2, height/2);
		SetColor(Color.White);
		UpdateScreenPosition();
	}

	protected abstract void Draw();

	public void SetColor(Color fillColor)
	{
		Clear(Color.Transparent);
		Fill(fillColor);
		Stroke(fillColor);
		Draw();
	}

	protected virtual void UpdateScreenPosition()
	{
		x = position.x;
		y = position.y;
		rotation = vecRotation.angleDeg;
	}
}
