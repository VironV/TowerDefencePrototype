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

        controller.Rotate(Arg.Any<Vector3>(),Arg.Any<Quaternion>(), Arg.Any<Vector3>());
        controller.CheckToShoot(Arg.Any<Vector3>(), Arg.Any<Quaternion>(), Arg.Any<Vector3>());
        tower.Received().Shoot();
    }

    [UnityTest]
    public IEnumerable ItShootsNotPermanently()
    {
        var tower = GetTowerMock();
        var controller = GetControllerMock(tower);

        controller.CheckToShoot(Arg.Any<Vector3>(), Arg.Any<Quaternion>(), Arg.Any<Vector3>());
        tower.Received().Shoot();

        yield return null;

        controller.CheckToShoot(Arg.Any<Vector3>(), Arg.Any<Quaternion>(), Arg.Any<Vector3>());
        tower.DidNotReceive().Shoot();
    }
    

    [Test]
    public void ItRotates()
    {
        var tower = GetTowerMock();
        var controller = GetControllerMock(tower);

        controller.threshold = 1;
        Quaternion quat = new Quaternion();
        quat.eulerAngles = new Vector3(0f, 0f, 10000f);
        Vector3 self = new Vector3(0f, 0f, 0f);
        Vector3 target = GetTargetToRotate();

        //Vector3 dir = target - self;
        //Debug.Log(Mathf.Abs(Quaternion.LookRotation(dir).eulerAngles.y-quat.eulerAngles.y));

        controller.Rotate(self,quat,target);
        tower.ReceivedWithAnyArgs().Rotate( Arg.Any<Quaternion>(), Arg.Any<float>());
    }

    [Test]
    public void ItNotRotatesInPosition()
    {
        var tower = GetTowerMock();
        var controller = GetControllerMock(tower);

        controller.threshold = 1;
        Quaternion quat = new Quaternion();
        quat.eulerAngles = new Vector3(0f, 0f, 10000f);
        Vector3 self = new Vector3(0f, 0f, 0f);
        Vector3 target = new Vector3(0f, 0f, 0f);

        controller.Rotate(self, quat, target);
        tower.DidNotReceiveWithAnyArgs().Rotate(Arg.Any<Quaternion>(), Arg.Any<float>());
    }

    [Test]
    public void ItNotShootsNotInPosition()
    {
        var tower = GetTowerMock();
        var controller = GetControllerMock(tower);

        controller.threshold = 1;
        controller.rotationSpeed = 0;
        Quaternion quat = new Quaternion();
        quat.eulerAngles = new Vector3(0f, 0f, 10000f);
        Vector3 self = new Vector3(0f, 0f, 0f);
        Vector3 target = GetTargetToRotate();

        controller.CheckToShoot(self, quat, target);
        tower.DidNotReceive().Shoot();
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

    //Helpers

    private Vector3 GetTargetToRotate()
    {
        return new Vector3(500f, 10f, 100f);
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
