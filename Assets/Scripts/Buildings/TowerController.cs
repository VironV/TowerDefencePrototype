using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour {


    [Header("Seetings")]
    public float range;
    public float rotationSpeed;
    public float fireRate = 2;

    [Header("Techical")]
    public Transform rotator;
    public Transform bulletSpawn;
    public GameObject bullet;

    private float fireCountdown=0f;
    private Transform target;
    private string monsterTag = "Monster";

    void Start () {
        monsterTag = Bestiary.GetMonsterTag;
        InvokeRepeating("UpdateTarget", 0f, 0.5f);
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
        GameObject bulletInstance = (GameObject)Instantiate(bullet, bulletSpawn.position, bulletSpawn.rotation);
        if (bulletInstance!=null)
        {
            BulletController bc = bulletInstance.GetComponent<BulletController>();
            if (bc!=null)
                bc.Seek(target);
        }
    }

    // Finding with tags
    void UpdateTarget()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag(monsterTag);

        GameObject nearestTarget = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject monster in monsters)
        {
            float dist = Vector3.Distance(transform.position, monster.transform.position);
            if (dist < shortestDistance)
            {
                nearestTarget = monster;
                shortestDistance = dist;
            }
        }

        if (nearestTarget != null && shortestDistance <= range)
        {
            target = nearestTarget.transform;
        }
        else
        {
            target = null;
        }
    }

    /*
    // Finding with colliders
    void UpdateTarget()
    {
        Transform nearestTarget = null;
        float shortestDistance = Mathf.Infinity;

        Collider[] monsters = Physics.OverlapSphere(transform.position, range);
        for (int i=0; i<monsters.Length;i++)
        {
            MonsterController mc = monsters[i].GetComponent<MonsterController>();
            if (mc != null)
            {
                Transform mcT = mc.gameObject.transform;
                float dist = Vector3.Distance(transform.position, mcT.position);
                if (dist < shortestDistance)
                {
                    nearestTarget = mcT;
                    shortestDistance = dist;
                }
            }
        }

        if (nearestTarget!=null)
        {
            target = nearestTarget;
        }
        else
        {
            target = null;
        }
    }
    */


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
