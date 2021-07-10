using System;
using GXPEngine; // Allows using Mathf functions

/// <summary>
/// 2D Vector. Meant for 2D physics calculations.
/// </summary>
public struct Vec2
{
	public float x;
	public float y;

	//------------------------------------------------------------------------------------------------------------------------
	//														Vec2()
	//------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Initializes a new instance of the <see cref="Vec2"/> class.
	/// </summary>
	public Vec2(float pX = 0, float pY = 0)
	{
		x = pX;
		y = pY;
	}

	//------------------------------------------------------------------------------------------------------------------------
	//														Vec2()
	//------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Initializes a new instance of the <see cref="Vec2"/> class.
	/// </summary>
	public Vec2(float pX, float pY, bool normalized)
	{
		x = pX;
		y = pY;

		if (normalized)
		{
			Normalize();
		}
	}

	//------------------------------------------------------------------------------------------------------------------------
	//														Length()
	//------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Returns the length of the current vector
	/// </summary>
	public float Length()
	{
		float _length = (float)Math.Sqrt((x * x) + (y * y));     //Using Pythagorean theorem to calculate the length of the vector
		return _length;
	}

	//------------------------------------------------------------------------------------------------------------------------
	//														length
	//------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Returns the length of the current vector or sets the length by normalizing itself and multiplying itself by the value
	/// </summary>
	public float length
	{
		get
		{
			return Length();     //Using Lenght() method to calculate the length of the vector
		}

		set
		{
			Normalize();        //First normalizing the vector so its length is 1
			this *= value;      //Multiplying the normalized vector times the given value so the length is now equal to the given value
		}
	}

	//------------------------------------------------------------------------------------------------------------------------
	//														Normalize()
	//------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Normalizes the current vector
	/// </summary>
	public void Normalize()
	{
		this = this.Normalized();
	}

	//------------------------------------------------------------------------------------------------------------------------
	//														Normalized()
	//------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Returns a normalized version of the current vector without modifying it
	/// </summary>
	public Vec2 Normalized()
	{
		float pX = x;
		float pY = y;

		if (length != 0f)
		{
			pX /= length;        //Calculate the normalized X value
			pY /= length;        //Calculate the normalized Y value
		}

		Vec2 temp = new Vec2(pX, pY);       //Create a new vector with the normalized X and Y value

		return temp;
	}

	//------------------------------------------------------------------------------------------------------------------------
	//														SetXY()
	//------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Sets the x & y of the current vector to the given two floats
	/// </summary>
	public void SetXY(float pX, float pY)
	{
		x = pX;
		y = pY;
	}

	// TODO: Implement subtract, scale operators

	//------------------------------------------------------------------------------------------------------------------------
	//														+ operator
	//------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Returns the result of adding two vectors(without modifying either of them)
	/// </summary>
	public static Vec2 operator +(Vec2 left, Vec2 right)
	{
		return new Vec2(left.x + right.x, left.y + right.y);
	}

	//------------------------------------------------------------------------------------------------------------------------
	//														- operator
	//------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Returns the result of subtracting two vectors(without modifying either of them)
	/// </summary>
	public static Vec2 operator -(Vec2 left, Vec2 right)
	{
		return new Vec2(left.x - right.x, left.y - right.y);
	}

	//------------------------------------------------------------------------------------------------------------------------
	//														* operator
	//------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Returns the result of multiplying a vector by a float(without modifying either of them)
	/// </summary>
	public static Vec2 operator *(Vec2 left, float right)
	{
		return new Vec2(left.x * right, left.y * right);
	}
	public static Vec2 operator *(float left, Vec2 right)
	{
		return right * left;
	}
	public static bool operator ==(Vec2 left, Vec2 right)
	{
		return left.x == right.x && left.y == right.y;
	}
	public static bool operator !=(Vec2 left, Vec2 right)
	{
		return left.x != right.x && left.y != right.y;
	}

	//------------------------------------------------------------------------------------------------------------------------
	//														LinInt()
	//------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Performs a linear interpolation between two vectors based on the given weighting and returns the result.
	/// </summary>
	/// <param name='left'>
	/// The first vector
	/// </param>
	/// <param name='right'>
	/// The second vector
	/// </param>
	/// <param name='interpolater'>
	/// This number specificifies the percentage of the second(right) vector in the result (If this is 0f: The left vector will be returned. If this is 1f: the right one. If 0.5f it is an average between the two)
	/// </param>
	public static Vec2 LinInt(Vec2 left, Vec2 right, float interpolater)
	{
		return left + interpolater * (right - left);
	}

	//------------------------------------------------------------------------------------------------------------------------
	//														Deg2Rad()
	//------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Converts the given degrees to radians
	/// </summary>
	/// <param name='degree'>
	/// The input degree value
	/// </param>
	static float Deg2Rad(float degree)
	{
		return (Mathf.PI / 180) * degree;
	}

	//------------------------------------------------------------------------------------------------------------------------
	//														Rad2Deg()
	//------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Converts the given radians to degrees
	/// </summary>	
	/// <param name='radians'>
	/// The input radians value
	/// </param>
	public static float Rad2Deg(float radians)
	{
		return (180 / Mathf.PI) * radians;
	}

	//------------------------------------------------------------------------------------------------------------------------
	//														GetUnitVectorDeg ()
	//------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Returns a new vector pointing in the given direction in degrees
	/// </summary>
	public static Vec2 GetUnitVectorDeg(float degrees)
	{
		return GetUnitVectorRad(Vec2.Deg2Rad(degrees));
	}

	//------------------------------------------------------------------------------------------------------------------------
	//														GetUnitVectorRad ()
	//------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Returns a new vector pointing in the given direction in radians
	/// </summary>
	public static Vec2 GetUnitVectorRad(float radians)
	{
		float pX = Mathf.Cos(radians);
		float pY = Mathf.Sin(radians);

		return new Vec2(pX, pY);
	}

	//------------------------------------------------------------------------------------------------------------------------
	//														RandomUnitVector ()
	//------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Returns a new unit vector pointing in a random direction
	/// </summary>
	public static Vec2 RandomUnitVector()
	{
		return Vec2.GetUnitVectorDeg(Utils.Random(0, 360));
	}

	//------------------------------------------------------------------------------------------------------------------------
	//														SetAngleDegrees ()
	//------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Changes the current vector to the given degrees (without changing length)
	/// </summary>
	public void SetAngleDegrees(float degrees)
	{
		SetAngleRadians(Deg2Rad(degrees));
	}

	//------------------------------------------------------------------------------------------------------------------------
	//														SetAngleRadians ()
	//------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Changes the current vector to the given radians (without changing length)
	/// </summary>
	public void SetAngleRadians(float radians)
	{
		Vec2 temp = Vec2.GetUnitVectorRad(radians);
		temp.length = this.length;

		this = temp;
	}

	//------------------------------------------------------------------------------------------------------------------------
	//														GetAngleRadians ()
	//------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Returns a float calculated to be the current angle of the vector in radians
	/// </summary>
	public float GetAngleRadians()
	{
		return Mathf.Atan2(y, x);
	}


	//------------------------------------------------------------------------------------------------------------------------
	//														GetAngleDegrees ()
	//------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Returns a float calculated to be the current angle of the vector in degrees
	/// </summary>
	public float GetAngleDegrees()
	{
		return Rad2Deg(GetAngleRadians());
	}

	public float angleRad
	{
		get { return GetAngleRadians(); }
		set { SetAngleRadians(value); }
	}

	public float angleDeg
	{
		get { return GetAngleDegrees(); }
		set { SetAngleDegrees(value); }
	}

	//------------------------------------------------------------------------------------------------------------------------
	//														RotateRadians ()
	//------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Rotate the vector over the given angle in radians
	/// </summary>
	public void RotateRadians(float radians)
	{
		float pX = x * Mathf.Cos(radians) - y * Mathf.Sin(radians);
		float pY = x * Mathf.Sin(radians) + y * Mathf.Cos(radians);

		this = new Vec2(pX, pY);
	}

	//------------------------------------------------------------------------------------------------------------------------
	//														RotateDegrees ()
	//------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Rotate the vector over the given angle in degrees
	/// </summary>
	public void RotateDegrees(float degrees)
	{
		RotateRadians(Deg2Rad(degrees));
	}

	//------------------------------------------------------------------------------------------------------------------------
	//														RotateAroundDegrees ()
	//------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Rotate the vector around the given point over the given angle in degrees
	/// </summary>
	public void RotateAroundDegrees(float degrees, Vec2 around)
	{
		RotateAroundRadians(Deg2Rad(degrees), around);
	}

	//------------------------------------------------------------------------------------------------------------------------
	//														RotateAroundRadians ()
	//------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Rotate the vector around the given point over the given angle in radians
	/// </summary>
	public void RotateAroundRadians(float radians, Vec2 around)
	{
		this -= around;
		RotateRadians(radians);
		this += around;
	}

	//------------------------------------------------------------------------------------------------------------------------
	//														normalizeDeg ()
	//------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// The entered degree value is normalized, the returend value cannot be outside (-180, 180) degrees
	/// </summary>
	public static float normalizeDeg(float degree)
	{
		if (degree > 180)
		{
			degree -= 360;
		}
		else if (degree < -180)
		{
			degree += 360;
		}

		return degree;
	}

	//------------------------------------------------------------------------------------------------------------------------
	//														RotateTowardsDegrees()
	//------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// The vector will rotate towards the target degrees in given steps
	/// </summary>
	/// <param name='targetDegrees'>
	/// The target degrees for the vector to eventually point too
	/// </param>
	/// <param name='stepDegrees'>
	/// The amount of degrees the vector can turn each time it is called
	/// </param>
	public void RotateTowardsDegrees(float targetDegrees, float stepDegrees)
	{
		//stepDegrees = Mathf.Abs(stepDegrees);
		float diffrence = targetDegrees - angleDeg;

		diffrence = normalizeDeg(diffrence);

		if (Mathf.Abs(diffrence) <= stepDegrees)
		{
			angleDeg = targetDegrees;
		}
		else if (diffrence < 0)
		{
			angleDeg -= stepDegrees;
		}
		else if (diffrence > 0)
		{
			angleDeg += stepDegrees;
		}
	}

	//------------------------------------------------------------------------------------------------------------------------
	//														Dot()
	//------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Will calculate the dot product of a given vector and itself
	/// </summary>
	public float Dot(Vec2 other)
	{
		return x * other.x + y * other.y; ;
	}

	//------------------------------------------------------------------------------------------------------------------------
	//														VectorDotProduct()
	//------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Will calculate the dot product of two given vectors
	/// </summary>
	public static float VectorDotProduct(Vec2 a, Vec2 b)
	{
		return a.Dot(b);
	}

	//------------------------------------------------------------------------------------------------------------------------
	//														Normal()
	//------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Returns a normal to this vector
	/// </summary>
	public Vec2 Normal()
	{
		Vec2 NormalVec = new Vec2(-y, x);
		return NormalVec.Normalized();
	}

	//------------------------------------------------------------------------------------------------------------------------
	//														Reflect()
	//------------------------------------------------------------------------------------------------------------------------
	/// <summary>
	/// Reflects the vector of another vector based on that other vector's normal
	/// </summary>
	public void Reflect(Vec2 pNormal, float pBounciness = 1)
	{
		this -= (1 + pBounciness) * (Dot(pNormal) * pNormal);
	}
}

