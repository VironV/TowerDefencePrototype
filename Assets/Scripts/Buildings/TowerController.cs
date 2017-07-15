using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerController {

    [Header("Seetings")]
    public float range;
    public float rotationSpeed;
    public float fireRate = 2;

    private float fireCountdown = 0f;

    private ITower tower;

    public void SetTowerController(ITower tower)
    {
        this.tower = tower;
    }

    public void Rotate(Vector3 target, Vector3 positionSelf, Quaternion rotationSelf)
    {
        Vector3 dir = target - positionSelf;
        Quaternion quatRotation = Quaternion.LookRotation(dir);
        if (quatRotation!=rotationSelf)
        {
            tower.Rotate(quatRotation,rotationSpeed);
        }
    }

    public bool isReadyToShoot()
    {
        return (fireCountdown <= 0);
    }

    public void CheckToShoot()
    {
        if (isReadyToShoot())
        {
            tower.Shoot();
            ResetCountdown();
        }    
    }

    public void TicCountdown()
    {
        fireCountdown -= Time.deltaTime;
    }

    private void ResetCountdown()
    {
        fireCountdown = 1f / fireRate;
    }

    // Finding with tags
    public void UpdateTarget(GameObject[] monsters, Vector3 positionSelf)
    {    
        GameObject nearestTarget = null;
        float shortestDistance = Mathf.Infinity;

        foreach (GameObject monster in monsters)
        {
            float dist = Vector3.Distance(positionSelf, monster.transform.position);
            if (dist < shortestDistance)
            {
                nearestTarget = monster;
                shortestDistance = dist;
            }
        }

        if (nearestTarget != null && shortestDistance <= range)
        {
            tower.UpdateTarget(nearestTarget.transform);
        }
        else
        {
            tower.UpdateTarget(null);
        }

        
    }

    /*
    // Finding with colliders
    public void UpdateTarget()
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

        tower.UpdateTarget(target);
    }
    */
}
