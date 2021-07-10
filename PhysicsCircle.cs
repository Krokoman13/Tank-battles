using System;
using System.Drawing;
using GXPEngine;
using System.Collections.Generic;

//------------------------------------------------------------------------------------------------------------------------
//														PhysicsCircle
//------------------------------------------------------------------------------------------------------------------------
/// <summary>
/// This object is a circle and will collide with other objects as a circle to do so
/// </summary>
public class PhysicsCircle : DynamicPhysicsObject
{
	public int radius {
		get {
			return _radius;
		}
	}

	int _radius;

	public PhysicsCircle(int pRadius, Vec2 pPosition) : base (pRadius*2 + 1, pRadius*2 + 1, pPosition)
	{
		_radius = pRadius;

		SetColor(Color.Gray);
	}

    protected override void Draw()
    {
		Ellipse(radius, radius, radius * 2, radius * 2);
    }

	public override CollisionInfo DetectCollision(PhysicsRectangle rectangle)
	{
		CollisionInfo sides = DetectCollisionsIn(rectangle.sides);
		CollisionInfo points = DetectCollisionsIn(rectangle.points);
		
		CollisionInfo output = CollisionInfo.Earliest(sides, points);
		if (output != null)
		{
			output.other = rectangle;
		}

		return output;
	}

	public override CollisionInfo DetectCollision(PhysicsCircle other)
    {
		Vec2 Difference = position - other.position;
		float distance = Difference.length;
		float minDistance = other.radius + radius;

		if (minDistance > distance)
		{
			Vec2 normal = Difference.Normalized();
			float overlap = other.radius + radius - distance;

			Vec2 POI = position + normal * overlap;

			Vec2 a = POI - oldPosition;
			float TOI = a.length / velocity.length;

			return new CollisionInfo(TOI, bouncyness, normal, other);
		}

		return null;
	}

	public override CollisionInfo DetectCollision(PhysicsLine _lineSegment)
	{
		Vec2 _lineVector = _lineSegment.end - _lineSegment.start;
		Vec2 _lineToBall = position - _lineSegment.start;
		float ballDistance = _lineToBall.Dot(_lineVector.Normal());

		if (ballDistance < radius)
		{
			Vec2 middle = (_lineSegment.start + _lineSegment.end) * 0.5f;
			Vec2 middleToBall = position - middle; 

			if (middleToBall.length < _lineVector.length/2)
			{
				Vec2 POI = position + (-ballDistance + radius) * _lineVector.Normal();

				Vec2 a = POI - oldPosition;
				float TOI = a.length / getVelocity().length;


				return new CollisionInfo(TOI, bouncyness, _lineVector.Normal(), _lineSegment);
			}
		}

		return null;
	}
}

