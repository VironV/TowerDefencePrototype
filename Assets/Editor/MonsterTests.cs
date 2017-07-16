using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using NSubstitute;

public class MonsterTests {

    [Test]
    public void ItMoves()
    {
        IMonster monster = GetMonsterMock();
        MonsterController controller = GetControllerMock(monster);

        controller = SetWithSingleWaypoint(controller, monster);

        controller.Move(new Vector3(0, 0, 1));
        monster.ReceivedWithAnyArgs().Move(new Vector3(0, 0, 1));
    }

    [Test]
    public void ItStopsAtPoint()
    {
        IMonster monster = GetMonsterMock();
        MonsterController controller = GetControllerMock(monster);

        Vector3 vect1 = new Vector3(0, 0, 1.1f);
        controller = SetWithSingleWaypointAndVector(controller, monster, vect1);

        controller.Move(vect1);
        monster.DidNotReceiveWithAnyArgs().Move(vect1);
    }

    [Test]
    public void ItHitsPlayer()
    {
        IMonster monster = GetMonsterMock();
        MonsterController controller = GetControllerMock(monster);

        Vector3 vect1 = new Vector3(0, 0, 1.1f);
        controller = SetWithSingleWaypointAndVector(controller, monster, vect1);

        controller.Move(vect1);
        monster.ReceivedWithAnyArgs().HitPlayer(10);
    }

    [UnityTest]
    public IEnumerable itChangesColorOnHit()
    {
        IMonster monster = GetMonsterMock();
        MonsterController controller = GetControllerMock(monster);

        controller = SetWithSingleWaypoint(controller, monster);

        controller.GetDamage(10);

        yield return null;
        monster.ReceivedWithAnyArgs().ChangeColor(Color.black);
    }

    [UnityTest]
    public IEnumerable ItNotChangesColorWithZeroChangeSpeed()
    {
        IMonster monster = GetMonsterMock();
        MonsterController controller = GetControllerMock(monster);

        controller.changindColorSpeed = 0;
        controller = SetWithSingleWaypoint(controller, monster);

        controller.GetDamage(10);

        yield return null;
        monster.DidNotReceiveWithAnyArgs().ChangeColor(Color.black);
    }

    // May be bad test - do not panic on failure
    [UnityTest]
    public IEnumerable itDies()
    {
        IMonster monster = GetMonsterMock();
        MonsterController controller = GetControllerMock(monster);

        controller = SetWithSingleWaypoint(controller, monster);

        controller.GetDamage(10000000);

        yield return null;
        monster.ReceivedWithAnyArgs().Die(10);
    }

    [Test]
    public void ItChangesHealthBar()
    {
        IMonster monster = GetMonsterMock();
        MonsterController controller = GetControllerMock(monster);

        controller = SetWithSingleWaypoint(controller, monster);

        controller.GetDamage(10);

        monster.ReceivedWithAnyArgs().ChangeHealthBar(0.1f);
    }

    [UnityTest]
    public IEnumerable ItIgnoresZeroAndNegativeDamage()
    {
        IMonster monster = GetMonsterMock();
        MonsterController controller = GetControllerMock(monster);

        controller = SetWithSingleWaypoint(controller, monster);

        //zero
        controller.GetDamage(0);

        monster.DidNotReceiveWithAnyArgs().ChangeHealthBar(0.1f);
        monster.DidNotReceiveWithAnyArgs().Die(10);

        yield return null;

        monster.DidNotReceiveWithAnyArgs().ChangeColor(Color.black);

        //negative
        controller.GetDamage(-100);

        monster.DidNotReceiveWithAnyArgs().ChangeHealthBar(0.1f);
        monster.DidNotReceiveWithAnyArgs().Die(10);

        yield return null;

        monster.DidNotReceiveWithAnyArgs().ChangeColor(Color.black);
    }


    private IMonster GetMonsterMock()
    {
        return Substitute.For<IMonster>();
    }

    private MonsterController GetControllerMock(IMonster monster)
    {
        var controller = Substitute.For<MonsterController>();
        return controller;
    }

    private MonsterController SetWithSingleWaypoint(MonsterController controller, IMonster monster)
    {
        Color black = Color.black;
        Vector3 vect1 = new Vector3(0, 0, 1.1f);
        Vector3[] waypoints = new Vector3[1];
        waypoints[0] = vect1;
        controller.SetMonsterController(monster, black, waypoints);
        return controller;
    }

    private MonsterController SetWithSingleWaypointAndVector(MonsterController controller, IMonster monster,Vector3 vect1)
    {
        Color black = Color.black;
        Vector3[] waypoints = new Vector3[1];
        waypoints[0] = vect1;
        controller.SetMonsterController(monster, black, waypoints);
        return controller;
    }
}
