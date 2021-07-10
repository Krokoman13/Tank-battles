using System;
using System.Drawing;
using System.Collections.Generic;
using GXPEngine;

//------------------------------------------------------------------------------------------------------------------------
//														PhysicsRectangle
//------------------------------------------------------------------------------------------------------------------------
/// <summary>
/// This class is a rectangle, can be inherinted to create other shapes. 
/// It uses PhysicPoints(small circles) for corners and PhysicLines to collide with other objects and let other obects collide with it.
/// </summary>

public class PhysicsRectangle : DynamicPhysicsObject
{
    public Vec2 topLeft
    {
        get
        {
            Vec2 point = new Vec2(position.x - width / 2, position.y - height / 2);
            point.RotateAroundDegrees(rotation, position);
            return point;
        }
    }
    public Vec2 topRight
    {
        get
        {
            Vec2 point = new Vec2(position.x + width / 2, position.y - height / 2);
            point.RotateAroundDegrees(rotation, position);
            return point;
        }
    }
    public Vec2 bottemLeft
    {
        get
        {
            Vec2 point = new Vec2(position.x - width / 2, position.y + height / 2);
            point.RotateAroundDegrees(rotation, position);
            return point;
        }
    }
    public Vec2 bottemRight
    {
        get
        {
            Vec2 point = new Vec2(position.x + width / 2, position.y + height / 2);
            point.RotateAroundDegrees(rotation, position);
            return point;
        }
    }

    private PhysicsLine _top;
    private PhysicsLine _right;
    private PhysicsLine _bottem;
    private PhysicsLine _left;

    public PhysicsLine top
    {
        get
        {
            _top.start = topRight;
            _top.end = topLeft;
            return _top;
        }
    }
    public PhysicsLine right
    {
        get
        {
            _right.start = bottemRight;
            _right.end = topRight;
            return _right;
        }
    }
    public PhysicsLine bottem
    {
        get
        {
            _bottem.start = bottemLeft;
            _bottem.end = bottemRight;
            return _bottem;
        }
    }
    public PhysicsLine left
    {
        get 
        {
            _left.start = topLeft;
            _left.end = bottemLeft;
            return _left; 
        }
    }

    private PhysicsPoint _topLeft;
    private PhysicsPoint _topRight;
    private PhysicsPoint _bottemLeft;
    private PhysicsPoint _bottemRight;

    public List<GameObject> sides
    {
        get { return new List<GameObject>() { top, right, left, bottem }; }
    }
    public List<GameObject> points
    {
        get
        {
            List<GameObject> circles = new List<GameObject>();

            _topLeft.position = topLeft;
            circles.Add(_topLeft);
            _topRight.position = topRight;
            circles.Add(_topRight);
            _bottemLeft.position = bottemLeft;
            circles.Add(_bottemLeft);
            _bottemRight.position = bottemRight;
            circles.Add(_bottemRight);

            return circles;
        }
    }
    public List<GameObject> ends
    {
        get { return new List<GameObject>() { left, right }; }
    }

    public PhysicsRectangle(int pWidth, int pHeight, Vec2 pPosition, float pRotation = 0) : base(pWidth, pHeight, pPosition)
    {
        SetColor(Color.Gray);
        rotation = pRotation;

        _topLeft = new PhysicsPoint(topLeft, this);
        _topRight = new PhysicsPoint(topRight, this);
        _bottemLeft = new PhysicsPoint(bottemLeft, this);
        _bottemRight = new PhysicsPoint(bottemRight, this);

        _top = new PhysicsLine(topRight, topLeft);
        _right = new PhysicsLine(bottemRight, topRight);
        _bottem = new PhysicsLine(bottemLeft, bottemRight);
        _left = new PhysicsLine(topLeft, bottemLeft);
    }

    protected override void Draw()
    {
        Rect(width / 2, height / 2, width, height);
    }

    public override CollisionInfo DetectCollision(PhysicsCircle circle)
    {
        if (!circle.moving)
        {
            foreach (PhysicsLine end in sides)
            {
                Vec2 _lineVector = end.end - end.start;
                Vec2 _lineToBall = circle.position - end.start;
                float ballDistance = _lineToBall.Dot(_lineVector.Normal());

                if (ballDistance < circle.radius)
                {
                    Vec2 middle = (end.start + end.end) * 0.5f;
                    Vec2 middleToBall = circle.position - middle;

                    if (middleToBall.length < _lineVector.length / 2)
                    {
                        position = position - (-ballDistance + circle.radius) * _lineVector.Normal();
                    }
                }
            }

            foreach (PhysicsPoint point in points)
            {
                Vec2 Difference = point.position - circle.position;
                float distance = Difference.length;
                float minDistance = circle.radius;

                if (minDistance > distance)
                {
                    Vec2 normal = Difference.Normalized();
                    float overlap = circle.radius - distance;

                    position = position + normal * overlap;
                }
            }
        }

        return null;
    }

    public override CollisionInfo DetectCollision(PhysicsLine _lineSegment)
    {
        foreach (PhysicsPoint point in points)
        {
            Vec2 _lineVector = _lineSegment.end - _lineSegment.start;
            Vec2 _lineToBall = point.position - _lineSegment.start;
            float ballDistance = _lineToBall.Dot(_lineVector.Normal());

            if (ballDistance < 0)
            {
                Vec2 middle = (_lineSegment.start + _lineSegment.end) * 0.5f;
                Vec2 middleToBall = point.position - middle;

                if (middleToBall.length < _lineVector.length / 2 + point.radius)
                {
                    position = position + (-ballDistance) * _lineVector.Normal();
                }
            }
        }

        return null;
    }

    public override CollisionInfo DetectCollision(PhysicsRectangle other)
    {
        foreach (PhysicsLine line in other.sides)
        {
            DetectCollision(line);
        }

        foreach (PhysicsPoint point in other.points)
        {
            DetectCollision(point);
        }

        return null;
    }
}

