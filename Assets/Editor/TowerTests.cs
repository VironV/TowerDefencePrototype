using UnityEngine;
using UnityEditor;
using UnityEngine.TestTools;
using NUnit.Framework;
using System.Collections;
using NSubstitute;

public class TowerTests {

    [Test]
    public void ItShoots()
    {
        var tower = GetTowerMock();
        var controller = GetControllerMock(tower);

        controller.CheckToShoot();
        tower.Received().Shoot();
    }

    [UnityTest]
    public IEnumerable ItShootsNotPermanently()
    {
        var tower = GetTowerMock();
        var controller = GetControllerMock(tower);

        controller.CheckToShoot();
        tower.Received().Shoot();

        yield return null;

        controller.CheckToShoot();
        tower.DidNotReceive().Shoot();
    }

    [Test]
    public void ItRotates()
    {
        var tower = GetTowerMock();
        var controller = GetControllerMock(tower);

        Quaternion tempQ = new Quaternion();
        Vector3 tempV = new Vector3();
        controller.Rotate(tempV,tempV,tempQ);
        tower.ReceivedWithAnyArgs().Rotate(tempQ,1f);
    }

    [Test]
    public void ItUpdatesTarget()
    {
        var tower = GetTowerMock();
        var controller = GetControllerMock(tower);

        controller.UpdateTarget(new GameObject[0],new Vector3());
        tower.ReceivedWithAnyArgs().UpdateTarget(Arg.Any<Transform>());
    }

    [UnityTest]
    public IEnumerable ItUpdatesTargetPeriodically()
    {
        var tower = GetTowerMock();
        var controller = GetControllerMock(tower);

        controller.UpdateTarget(new GameObject[2], new Vector3());
        tower.ReceivedWithAnyArgs().UpdateTarget(Arg.Any<Transform>());

        yield return new WaitForSeconds(2);

        controller.UpdateTarget(new GameObject[2], new Vector3());
        tower.ReceivedWithAnyArgs().UpdateTarget(Arg.Any<Transform>());
    }

    [Test]
    public void ItNotFindsTargetFromNowhere()
    {
        var tower = GetTowerMock();
        var controller = GetControllerMock(tower);

        controller.UpdateTarget(new GameObject[0], new Vector3());
        tower.Received().UpdateTarget(null);
    }

    private TowerController GetControllerMock(ITower tower)
    {
        var controller = Substitute.For<TowerController>();
        controller.SetTowerController(tower);
        
        return controller;
    }

    private ITower GetTowerMock()
    {
        return Substitute.For<ITower>();
    }
}
