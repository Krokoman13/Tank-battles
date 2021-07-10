using System;
using GXPEngine;
using System.Drawing;
using System.Collections.Generic;

public class MyGame : Game
{
	static void Main() {
		new MyGame().Start();
	}

	public EasyDraw canvas;
	public PlayerTank player;
	public Mouse mouse;
	public Vec2 gravity = new Vec2(0, 0.981f);

	public MyGame () : base(1000, 800, false,false)
	{
		UnitTest();

		canvas = new EasyDraw(width, height);
		canvas.Clear(Color.LightGray);
		AddChild(canvas);

		loadBorder();
		loadCrates();
		loadPilars();

		AddChildAt(player = new PlayerTank(new Vec2(width / 2 - 400, height / 2 + 50)), 100);
		AddChildAt(new EnemyTank(new Vec2(width / 2 + 400, height / 2)), 100);
	}

	void loadBorder(int margin = 5)
	{
		AddChild(new PhysicsLine(new Vec2(0, margin), new Vec2(width, margin)));
		AddChild(new PhysicsLine(new Vec2(width, height - margin), new Vec2(0, height - margin)));
		AddChild(new PhysicsLine(new Vec2(width - margin, 0), new Vec2(width - margin, height)));
		AddChild(new PhysicsLine(new Vec2(margin, height), new Vec2(margin, 0)));
	}

	void loadCrates()
	{
		AddChild(new PhysicsRectangle(51, 98, new Vec2(873, 710), 68));
		AddChild(new PhysicsRectangle(39, 27, new Vec2(127, 566), 1));
		AddChild(new PhysicsRectangle(73, 64, new Vec2(307, 531), 4));
		AddChild(new PhysicsRectangle(59, 57, new Vec2(170, 345), 88));
		AddChild(new PhysicsRectangle(40, 30, new Vec2(473, 216), 4));
		AddChild(new PhysicsRectangle(79, 66, new Vec2(930, 130), 5));
		AddChild(new PhysicsRectangle(69, 100, new Vec2(170, 99), 58));
		AddChild(new PhysicsRectangle(100, 100, new Vec2(75, 75)));
	}

	void loadPilars()
	{
		AddChild(new PhysicsCircle(19, new Vec2(742, 767)));
		AddChild(new PhysicsCircle(50, new Vec2(80, 743)));
		AddChild(new PhysicsCircle(49, new Vec2(481, 700)));
		AddChild(new PhysicsCircle(11, new Vec2(432, 436)));
		AddChild(new PhysicsCircle(26, new Vec2(813, 406)));
		AddChild(new PhysicsCircle(22, new Vec2(588, 126)));
	}


	void DetectCollisions()
	{
		List<DynamicPhysicsObject> _physicsObjects = new List<DynamicPhysicsObject>();
		CollisionInfo earliestCollision = null;

		foreach (GameObject child in GetChildren())
		{
			if (child is DynamicPhysicsObject)
			{
				DynamicPhysicsObject physicsObject = (DynamicPhysicsObject)child;
				physicsObject.DetectAllCollisions();
				CollisionInfo newColision = physicsObject.earliestCollision;

				if (physicsObject.moving)
				{
					if (newColision != null)
					{
						earliestCollision = CollisionInfo.Earliest(earliestCollision, newColision);
					}

					_physicsObjects.Add(physicsObject);
				}
			}
		}

		if (earliestCollision != null)
		{
			if (earliestCollision.timeOfImpact < 1)
			{
				foreach (DynamicPhysicsObject physicsObject in _physicsObjects)
				{
					physicsObject.setTOI(earliestCollision.timeOfImpact);
				}
			}
		}
	}

	public void UnitTest()
	{
		//UNIT TEST
		//============================================================================================
		Console.WriteLine("========================= Start of unit tests ========================= ");
		Vec2 myVec1 = new Vec2(3, 4);
		Vec2 result;

		//Length, Normalized and Normalize unit test
		Console.WriteLine("Length method ok ?: " +
		 (myVec1.length == 5 && myVec1.x == 3 && myVec1.y == 4));

		Console.WriteLine("Normalized method ok ?: " +
		 (myVec1.Normalized().length == 1 && myVec1.x == 3 && myVec1.y == 4));

		result = myVec1;
		result.Normalize();
		Console.WriteLine("Normalize method ok ?: " +
		 (result.length == 1 && myVec1.x == 3 && myVec1.y == 4));

		result = myVec1;
		result.length = 5;
		Console.WriteLine("Lentght setting ok ?: " +
		(result.length == 5 && myVec1.x == 3 && myVec1.y == 4));

		//Multiplication unit test
		result = myVec1 * 3;
		Console.WriteLine("Scalar multiplication right ok ?: " +
		 (result.x == 9 && result.y == 12 && myVec1.x == 3 && myVec1.y == 4));

		result = 4 * myVec1;
		Console.WriteLine("Scalar multiplication left ok ?: " +
		 (result.x == 12 && result.y == 16 && myVec1.x == 3 && myVec1.y == 4));

		//Addition and substraction unit test
		Vec2 myVec2 = new Vec2(2, 3);

		result = myVec1 + myVec2;
		Console.WriteLine("Vector addition ok ?: " +
		 (result.x == 5 && result.y == 7 && myVec1.x == 3 && myVec1.y == 4 && myVec2.x == 2 && myVec2.y == 3));

		result = myVec1 - myVec2;
		Console.WriteLine("Vector subtraction ok ?: " +
		 (result.x == 1 && result.y == 1 && myVec1.x == 3 && myVec1.y == 4 && myVec2.x == 2 && myVec2.y == 3));

		//Angel rotate tests
		myVec1 = Vec2.GetUnitVectorDeg(90f);
		myVec2 = Vec2.GetUnitVectorRad(Mathf.PI / 2);
		result.SetXY(0, 1);
		Console.WriteLine("Vector GetUnitVector Deg/Rad ok?: " +
		 myVec1 + " & " + myVec2 + " equal too " + result);

		myVec1 = Vec2.RandomUnitVector();
		myVec2 = Vec2.RandomUnitVector();
		result = Vec2.RandomUnitVector();
		Console.WriteLine("Vector RandomUnitVector ok?: " +
		 myVec1 + " & " + myVec2 + " & " + result + " all diffrent(random) vectors");

		myVec1.SetAngleDegrees(35f);
		myVec1.length = 5;
		result.angleDeg = myVec1.GetAngleDegrees();
		Console.WriteLine("Vector Get/SetAngleDegrees/Radians ok?: " +
		 35f + " == " + result.GetAngleDegrees());

		myVec1.SetXY(4, 6);
		myVec2.SetXY(2, 1);
		result = myVec1;

		myVec1.RotateDegrees(90f);
		result.RotateAroundDegrees(90f, myVec2);

		Console.WriteLine("Vector RotateDegrees ok?: " +
		 new Vec2(-6, 4) + " == " + myVec1);

		Console.WriteLine("Vector RotateAroundDegrees ok?: " +
		 new Vec2(-3, 3) + " == " + result);

		//Dotproduct units tests
		myVec1 = new Vec2(-4,-9);
		myVec2 = new Vec2(-1,2);
		Console.WriteLine("Vector Dot ok?: " +
		 (myVec1.Dot(myVec2) == -14));

		myVec1 = new Vec2(4, 4);
		result = myVec1.Normal();
		Console.WriteLine("Vector Normal() ok?: " +
			(myVec1.angleDeg + 90f == result.angleDeg ));

		Console.WriteLine("========================= End of unit tests ========================= ");

		//============================================================================================
		//END UNIT TEST
	}

	void Update () {
		DetectCollisions();
		mouse.Update();
	}
}

