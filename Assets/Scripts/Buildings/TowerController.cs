using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerController {

    [Header("Seetings")]
    public float range;
    public float rotationSpeed;
    public float fireRate = 2;

    [Header("Technical]")]
    public float threshold = 20f;

    private float fireCountdown = 0f;
    private Transform target;

    private ITower tower;

    public void SetTowerController(ITower tower)
    {
        this.tower = tower;
        target = null;
    }

    private Quaternion FindDirection(Vector3 positionSelf)
    {
        Vector3 dir = target.position - positionSelf;
        return Quaternion.LookRotation(dir);
    }

    public void Rotate(Vector3 positionSelf, Quaternion rotationSelf)
    {
        Quaternion quatRotation= FindDirection(positionSelf);
        if (quatRotation!=rotationSelf)
        {
            tower.Rotate(quatRotation,rotationSpeed);
        }
    }

    public bool isTargetNull()
    {
        return target == null;
    }

    public void CheckToShoot(Quaternion rotation,Vector3 positionSelf)
    {
        Quaternion quatRotation = FindDirection(positionSelf);

        if (Mathf.Abs(quatRotation.eulerAngles.y - rotation.eulerAngles.y) > threshold)
            return;

        if (fireCountdown <= 0)
        {
            tower.Shoot(target);
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
            target = nearestTarget.transform;
        }
        else
        {
            target = null;
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
