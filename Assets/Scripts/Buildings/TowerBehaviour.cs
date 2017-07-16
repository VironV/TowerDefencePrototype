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

    void Start()
    {
        controller.SetTowerController(this);
        monsterTag = Bestiary.GetMonsterTag;
        InvokeRepeating("AskToFindTarget", 0f, 0.5f);
    }

    void Update()
    {
        controller.TicCountdown();

        if (controller.isTargetNull())
            return;

        controller.Rotate(transform.position, transform.rotation);
        controller.CheckToShoot(rotator.rotation,transform.position);
    }

    void AskToFindTarget()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag(monsterTag);
        controller.UpdateTarget(monsters,transform.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, controller.range);
    }

    #region ITower implementation

    public void Rotate(Quaternion quatRotation, float rotationSpeed)
    {
        Vector3 rotation = Quaternion.Lerp(rotator.rotation, quatRotation, Time.deltaTime * rotationSpeed).eulerAngles;
        rotator.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    public void Shoot(Transform target)
    {
        GameObject bulletInstance = (GameObject)Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
        if (bulletInstance!=null)
        {
            IAmmo bc = bulletInstance.GetComponent<IAmmo>();
            bc.SetRotation(rotator.rotation.eulerAngles.y);
            bc.Seek(target);
        }
    }

#endregion

    
}
