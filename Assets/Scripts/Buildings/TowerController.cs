using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TowerController {

    [Header("Seetings")]
    [Range(1, 50)]
    public float range;
    [Range(5,50)]
    public float rotationSpeed;
    [Range(0.01f, 25)]
    public float fireRate = 2;

    [Header("Technical]")]
    public float threshold = 20f;

    private float fireCountdown = 0f;
    //private Transform target;

    private ITower tower;


    // calls from behaviour
    public void SetTowerController(ITower tower)
    {
        this.tower = tower;
    }

    public void TicCountdown()
    {
        fireCountdown -= Time.deltaTime;
    }

    public void Rotate(Vector3 positionSelf, Quaternion rotationSelf, Vector3 positionTarget)
    {
        Quaternion quatRotation= FindDirection(positionSelf,positionTarget);
        if (CheckRotationToTarget(quatRotation,rotationSelf,threshold/2))
        {
            tower.Rotate(quatRotation,rotationSpeed);
        }
    }

    public void CheckToShoot(Vector3 positionSelf, Quaternion rotationSelf,Vector3 positionTarget)
    {
        Quaternion quatRotation = FindDirection(positionSelf,positionTarget);
        if (CheckRotationToTarget(quatRotation,rotationSelf, threshold))
            return;

        if (fireCountdown <= 0)
        {
            tower.Shoot();
            ResetCountdown();
        }    
    }

    //Helpers
    private Quaternion FindDirection(Vector3 positionSelf,Vector3 positionTarget)
    {
        Vector3 dir = positionTarget - positionSelf;
        return Quaternion.LookRotation(dir);
    }

    private bool CheckRotationToTarget(Quaternion quatRotation, Quaternion rotationSelf, float th)
    {
        return (Mathf.Abs(quatRotation.eulerAngles.y - rotationSelf.eulerAngles.y) > th);
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
            tower.UpdateTarget(nearestTarget.transform);
        else
            tower.UpdateTarget(null);

        
    }

    /*
    // Finding with colliders (alternative)
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
