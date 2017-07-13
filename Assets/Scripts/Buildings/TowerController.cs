using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour {


    [Header("Attributes")]
    public float range;
    public float rotationSpeed;
    public float fireRate = 2;
    

    [Header("Setup")]
    public string monsterTag = "Monster";
    public Transform rotator;
    public Transform bulletSpawn;
    public GameObject bullet;
    public string bulletType;

    private float fireCountdown=0f;
    private Transform target;

	void Start () {
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
	}

    void UpdateTarget()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag(monsterTag);

        GameObject nearestTarget = null;
        float shortestDistance = Mathf.Infinity;

        foreach(GameObject monster in monsters)
        {
            float dist = Vector3.Distance(transform.position, monster.transform.position);
            if (dist<shortestDistance)
            {
                nearestTarget = monster;
                shortestDistance = dist;
            }
        }

        if (nearestTarget!=null && shortestDistance<=range)
        {
            target = nearestTarget.transform;
        } else
        {
            target = null;
        }
    }

	
	void Update () {
        if (target == null)
            return;

        Vector3 dir = target.position - transform.position;
        Quaternion quatRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(rotator.rotation, quatRotation,Time.deltaTime*rotationSpeed).eulerAngles;
        rotator.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (fireCountdown<=0f)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -=Time.deltaTime;
	}

    void Shoot()
    {
        GameObject bulletInstance= (GameObject) Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
        if (bulletType == "bullet")
        {
            BulletController bc = bulletInstance.GetComponent<BulletController>();
            if (bc != null)
                bc.Seek(target);
        }
        if (bulletType=="missle")
        {
            MissleController mc = bulletInstance.GetComponent<MissleController>();
            if (mc != null)
                mc.Seek(target);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
