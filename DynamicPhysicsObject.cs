using System;
using System.Drawing;
using GXPEngine;
using System.Collections.Generic;

//------------------------------------------------------------------------------------------------------------------------
//														PhysicsObject
//------------------------------------------------------------------------------------------------------------------------
/// <summary>
/// A class disgined to apply physics on, inherinted from PhysicsObject, this class lets itself move, not only be collidable
/// </summary>
public abstract class DynamicPhysicsObject : PhysicsObject
{
	public Vec2 acceleration;
	public Vec2 velocity;

	public virtual Vec2 getVelocity()
	{
		return velocity;
	}
	public virtual void setVelocity(Vec2 pVelocity)
	{
		velocity = pVelocity;
	}

	public bool moving
	{
		get { return velocity.length > 0; }
	}
	public Vec2 oldPosition
	{
		get { return position - velocity; }
	}

	public CollisionInfo earliestCollision;
	public float bouncyness;

	public DynamicPhysicsObject(int pWidth, int pHeight, Vec2 pPosition) : base(pWidth, pHeight, pPosition)
	{
		position = pPosition;
		SetOrigin(width / 2, height / 2);

		SetColor(Color.Blue);
		UpdateScreenPosition();
	}

	public void DetectAllCollisions()
	{
		if (moving)
		{
			earliestCollision = null;
			earliestCollision = DetectCollisionsIn(game.GetChildren());
		}
	}

	protected CollisionInfo DetectCollisionsIn(List<GameObject> gameObjects)
	{
		CollisionInfo newCollision = null;
		CollisionInfo outCollision = null;

		foreach (GameObject gameObject in gameObjects)
		{
			newCollision = DetectCollision(gameObject);
			outCollision = CollisionInfo.Earliest(newCollision, outCollision);
		}

		return outCollision;
	}

	private CollisionInfo DetectCollision(GameObject gameObject)
	{
		CollisionInfo _newCollision;

		if (gameObject == this)
		{
			_newCollision = null;
		}
		else if (gameObject is PhysicsLine)
		{
			PhysicsLine lineSegment = (PhysicsLine)gameObject;
			_newCollision = DetectCollision(lineSegment);
		}
		else if (gameObject is PhysicsCircle)
		{
			PhysicsCircle circle = (PhysicsCircle)gameObject;
			_newCollision = DetectCollision(circle);
		}
		else if (gameObject is PhysicsRectangle)
		{
			PhysicsRectangle rectangle = (PhysicsRectangle)gameObject;
			_newCollision = DetectCollision(rectangle);
		}
		else
		{
			_newCollision = null;
		}
		
		return _newCollision;
	}

	public abstract CollisionInfo DetectCollision(PhysicsLine lineSegment);
	public abstract CollisionInfo DetectCollision(PhysicsCircle circle);
	public abstract CollisionInfo DetectCollision(PhysicsRectangle rectangle);

	public virtual void setTOI(float TOI)
	{
		if (moving)
		{
			position = oldPosition + velocity * TOI;

			if (earliestCollision != null)
			{
				if (earliestCollision.timeOfImpact == TOI)
				{
					Collide(earliestCollision.other);
				}
			}
		}
	}

	public virtual void Collide(PhysicsObject other)
	{
	}

	public void Step()
	{
		UpdateScreenPosition();
		setAccelleration();
		applyAcceleration();
		applyVelocity();
	}

	protected virtual void applyVelocity()
	{
		position += velocity;
	}

	protected virtual void applyAcceleration()
	{
		velocity += acceleration;
	}

	protected virtual void setAccelleration()
	{
		acceleration.SetXY(0, 0);
	}
}
