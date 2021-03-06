﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicTowerBehaviour : MonoBehaviour, ITower {

    [Header("Techical")]
    public Transform rotator;
    public Transform bulletSpawn;
    public TowerController controller;

    public GameObject bullet;

    public bool fireThrower = false;
    public GameObject fire;
    public float posMultiplier;
    public float posUpper;

    public GameObject gizmo;

    private Transform target;

    private GameObject currentFire;
    private bool throwingFire;
        
    public Transform Target { get { return target; } }

    void Start()
    {
        throwingFire = false;
        target = null;
        controller.SetTowerController(this);
        InvokeRepeating("AskToFindTarget", 0f, 0.5f);
    }

    void Update()
    {
        controller.TicCountdown();

        if (target==null)
        {
            if (fireThrower && throwingFire)
                StopThrowFire();
            return;
        }
            

        controller.Rotate(transform.position,rotator.rotation,target.position);
        controller.CheckToShoot(transform.position,rotator.rotation,target.position);
    }

    // Controller interactions
    void AskToFindTarget()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag(Bestiary.GetMonsterTag);
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
        if (!fireThrower)
            ShootBullet();
        else
            ThrowFire();
    }

    void ThrowFire()
    {
        if (!throwingFire)
        {
            Vector3 offset = target.transform.position - rotator.transform.position;
            offset.Normalize();
            offset = new Vector3(offset.x * posMultiplier, offset.y + posUpper, offset.z * posMultiplier);

            currentFire = (GameObject)Instantiate(fire, rotator.position + offset, Quaternion.identity);
            currentFire.transform.parent = rotator;
            if (currentFire != null)
            {
                IRotatable bc = currentFire.GetComponent<IRotatable>();
                bc.SetRotation(rotator.rotation.eulerAngles.y);
            }
        }
        throwingFire = true;
    }

    void ShootBullet()
    {
        GameObject bulletInstance = (GameObject)Instantiate(bullet, bulletSpawn.position, Quaternion.identity);
        if (bulletInstance!=null)
        {
            IAmmo bc = bulletInstance.GetComponent<IAmmo>();
            bc.SetRotation(rotator.rotation.eulerAngles.y);
            bc.Fire(target);
        }
    }

    //Helpers
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, controller.range);
    }

    void StopThrowFire()
    {
        throwingFire = false;
        Destroy(currentFire);
    }
}
