using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    private Transform target;

    public GameObject particles;
    public float speed=70f;
    public int damage = 50;

    public void Seek(Transform _target)
    {
        target = _target;
    }

	void Update () {
		if (target==null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
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
        GameObject ptl=Instantiate(particles, transform.position, transform.rotation);
        Destroy(ptl, 1f);

        Destroy(gameObject);

        MonsterController mc = target.GetComponent<MonsterController>();
        mc.GetDamage(damage);
    }
}
