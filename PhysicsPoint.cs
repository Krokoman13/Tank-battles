using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//------------------------------------------------------------------------------------------------------------------------
//														PhysicsPoint
//------------------------------------------------------------------------------------------------------------------------
/// <summary>
/// This class is meant to be a single piont, but for collision reasons still has a radius of 1, it mostly acts as a circle
/// </summary>
public class PhysicsPoint : PhysicsCircle
{
	DynamicPhysicsObject _physicsParent;

	public override Vec2 getVelocity()
	{
		return _physicsParent.velocity;
	}
	public override void setVelocity(Vec2 pVelocity)
	{
		_physicsParent.velocity = pVelocity;
	}

	public PhysicsPoint(Vec2 pPosition, DynamicPhysicsObject pPhysicsParent) : base(1, pPosition)
    {
		_physicsParent = pPhysicsParent;
    }

    public override CollisionInfo DetectCollision(PhysicsLine other)
    {
		return base.DetectCollision(other);
    }
}

