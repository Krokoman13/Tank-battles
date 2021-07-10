using System;
using System.Drawing;
using GXPEngine;

//------------------------------------------------------------------------------------------------------------------------
//														PhysicsLine
//------------------------------------------------------------------------------------------------------------------------
/// <summary>
/// This class is a straight line, and let other physics object collide with it as a line
/// </summary>
public class PhysicsLine : PhysicsObject
{
	public Vec2 start;
	public Vec2 end;
	public Vec2 middle
	{
		get { return CalculateMiddle(start, end); }
	}

	public uint lineWidth = 1;

	public PhysicsLine(Vec2 pStart, Vec2 pEnd) : base((int)(pEnd - pStart).length, 1, CalculateMiddle(pStart, pEnd))
	{
		start = pStart;
		end = pEnd;

		SetColor(Color.Green);

		rotation = (start - end).angleDeg;
	}

	private static Vec2 CalculateMiddle(Vec2 _start, Vec2 _end)
	{
		Vec2 middle = _end + _start;
		middle *= 0.5f;
		return middle;
	}

    protected override void Draw()
    {
		Clear(Color.Green);
	}
}
