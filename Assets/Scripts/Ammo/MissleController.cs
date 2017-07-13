using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleController : MonoBehaviour {

    private Vector3 target;

    public GameObject particles;
    public float speed = 10f;
    public float explosionRange = 10f;
    public int damage = 90;

    public void Seek(Transform _target)
    {
        target = _target.position;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target - transform.position;
        float distanceToGo = speed * Time.deltaTime;

        if (dir.magnitude <= distanceToGo)
        {
            HitTarget();
            return;
        }

        transform.Translate(dir.normalized * distanceToGo, Space.World);
    }

    void HitTarget()
    {
        GameObject[] monsters = GameObject.FindGameObjectsWithTag(Bestiary.monsterTag);
        
        for (int i=0; i<monsters.Length; i++)
        {
            if (Vector3.Distance(target,monsters[i].transform.position)<explosionRange)
            {
                MonsterController mc = monsters[i].GetComponent<MonsterController>();
                mc.GetDamage(damage);
            }
        }

        Destroy(gameObject);
    }
}
