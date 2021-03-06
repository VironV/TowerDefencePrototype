﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour, IAmmo{

    [Header("Settings")]
    public float speed=70f;
    public int damage = 50;
    public float explosionRange = 0f;
    public bool targetFloor = false;

    [Header("Techical")]
    public GameObject particles;

    private Transform targetGO;
    private Vector3 targetPlace;

    void Update()
    {
        if (targetGO != null)
            targetPlace = targetGO.position;
        Vector3 dir = targetPlace - transform.position;
        float distanceToGo = speed * Time.deltaTime;

        if (dir.magnitude <= distanceToGo)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceToGo, Space.World);
    } 

    //
    // Seek target
    //
    public void Fire(Transform go)
    {
        if (targetFloor)
            SeekPlace(go.position);
        else
            SeekGO(go);
    }

    private void SeekGO(Transform targetGO)
    {
        this.targetGO = targetGO;
        targetPlace = targetGO.position;
    }

    private void SeekPlace(Vector3 targetPlace)
    {
        this.targetPlace = targetPlace;
    }

    //
    // Hitting target
    //
    void HitTarget()
    {
        if (explosionRange <= 0 && targetFloor)
            return;

        if (explosionRange>0)
            Explode();
        else
            JustHit();
    }

    void JustHit()
    {
        if (targetGO!=null)
        {
            MonsterBehaviour mc = targetGO.GetComponent<MonsterBehaviour>();
            mc.GetDamage(damage);
        }

        if (particles!=null)
        {
            GameObject ptl = Instantiate(particles, transform.position, transform.rotation);
            Destroy(ptl, 1f);
        }
        

        Destroy(gameObject);
    }

    // Finding exploding targets with colliders
    void Explode()
    {
        Collider[] monsters = Physics.OverlapSphere(transform.position, explosionRange);
        for (int i = 0; i < monsters.Length; i++)
        {
            IMonster mc = monsters[i].GetComponent<IMonster>();
            if (mc != null)
            {
                mc.GetDamage(damage);
            }
        }
        GameObject ptl = Instantiate(particles, transform.position, transform.rotation);
        Destroy(ptl, 2f);
        Destroy(gameObject);
    }

    /*
    // Finding exploding targets with tags (old bad version)
    void Explode()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag(Bestiary.monsterTag);
        
        for (int i=0; i<monsters.Length; i++)
        {
            if (Vector3.Distance(target,monsters[i].transform.position)<explosionRange)
            {
                MonsterController mc = monsters[i].GetComponent<MonsterController>();
                mc.GetDamage(damage);

                GameObject ptl = Instantiate(particles, transform.position, transform.rotation);
                Destroy(ptl, 2f);
            }
        }

        Destroy(gameObject);
    }
    */

    // Helpers
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRange);
    }

    public void SetRotation(float y)
    {
        transform.rotation = Quaternion.Euler(90, 90 + y, 90);
    }
}
