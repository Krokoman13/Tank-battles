using GXPEngine; // For GameObject

//------------------------------------------------------------------------------------------------------------------------
//														CollisionInfo
//------------------------------------------------------------------------------------------------------------------------
/// <summary>
/// This class is meant to store the information of any collisions.
/// It is used to compare Time of impact data, to make sure collisions are handeled in the right order.
/// </summary>
public class CollisionInfo
{
	public readonly float timeOfImpact;
	public readonly float COR;
	public readonly Vec2 normal;
	public PhysicsObject other;

	public CollisionInfo(float pTimeOfImpact = 1, float pCOR = 0, Vec2 pNormal = new Vec2 (), PhysicsObject pOther = null)
	{
		timeOfImpact = pTimeOfImpact;
		COR = pCOR;
		normal = pNormal;
		other = pOther;
	}

	public static CollisionInfo Earliest(CollisionInfo a, CollisionInfo b) 
	{
		if (a == null)
		{
			return b;
		}
		if (b == null)
		{
			return a;
		}

		if (b.timeOfImpact < a.timeOfImpact)
		{
			return b;
		}
		else
		{
			return a;
		}
	}
}
