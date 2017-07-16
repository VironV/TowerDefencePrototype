using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBehaviour : MonoBehaviour, ITower {

    [Header("Techical")]
    public Transform rotator;
    public Transform bulletSpawn;
    public GameObject bullet;
    public TowerController controller;

    private string monsterTag = "Monster";
    private Transform target;

    void Start()
    {
        target = null;
        controller.SetTowerController(this);
        monsterTag = Bestiary.GetMonsterTag;
        InvokeRepeating("AskToFindTarget", 0f, 0.5f);
    }

    void Update()
    {
        controller.TicCountdown();

        if (target==null)
            return;

        controller.Rotate(transform.position,rotator.rotation,target.position);
        controller.CheckToShoot(transform.position,rotator.rotation,target.position);
    }

    // Controller interactions
    void AskToFindTarget()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag(monsterTag);
        controller.UpdateTarget(monsters,transform.position);
    }

    public void UpdateTarget(Transform target)
    {
        this.target = target;
    }

    // ITower functions
    public void Rotate(Quaternion quatRotation, float rotationSpeed)
    {
        Vector3 rotation = Quaternion.Lerp(rotator.rotation, quatRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        rotator.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    public void Shoot()
    {
        GameObject bulletInstance = (GameObject)Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
        if (bulletInstance!=null)
        {
            IAmmo bc = bulletInstance.GetComponent<IAmmo>();
            bc.SetRotation(rotator.rotation.eulerAngles.y);
            bc.Seek(target);
        }
    }

    //Helpers
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, controller.range);
    }

}
