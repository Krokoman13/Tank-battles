using System;
using System.Drawing;
using GXPEngine;
using System.Collections.Generic;

public class Bullet : PhysicsCircle
{

    float _friction = 0.001f;

    public Bullet() : base(9, new Vec2(0, 0))
    {
        SetColor(Color.DarkGray);
        bouncyness = 0.5f;
    }

    void Update()
    {
        rotation = velocity.angleDeg;
        Step();
    }

    protected override void applyAcceleration()
    {
        base.applyAcceleration();
        velocity += velocity * -_friction;
        if (velocity.length < 2)
        {
            Remove();
        }
    }

    public override void Collide(PhysicsObject other)
    {
        base.Collide(other);
        if (other is Tank)
        {
            Tank tank = (Tank)other;
            tank.health -= 1;
            Remove();
        }
        else if (other is PhysicsLine) velocity.Reflect(earliestCollision.normal, bouncyness);
        else if (other is PhysicsCircle) velocity.Reflect(earliestCollision.normal, bouncyness);
        else if (other is PhysicsRectangle) velocity.Reflect(earliestCollision.normal, bouncyness);
    }
}

