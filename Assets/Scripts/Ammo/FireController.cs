using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour, IRotatable {

    [Header("Settings")]
    public int damage = 1;
    public float howLong = 5;
    public float tickPeriod = 0.5f;
    public float damagePeriod = 0.5f;
    public int tickDamage = 10;

    [Header("Techical")]
    public GameObject particles;
    public float positionOffset;
    public Vector3 rotationOffest;

    private float damageCountdown;

    private void Start()
    {
        damageCountdown = 0;


        Quaternion rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + rotationOffest.x,
                                            transform.rotation.eulerAngles.y + rotationOffest.y,
                                            transform.rotation.eulerAngles.z + rotationOffest.z);
        GameObject fire = Instantiate(particles, transform.position, rotation);
        fire.transform.Translate(new Vector3(0,0,positionOffset), Space.Self);
        fire.transform.parent = transform;
    }


    private void OnTriggerStay(Collider other)
    {    
        if (other.tag==Bestiary.GetMonsterTag )
        {
            damageCountdown += Time.deltaTime;
            if (damageCountdown>=damagePeriod)
            {
                damageCountdown = 0;
                other.GetComponent<IMonster>().GetDamage(damage);
            }
        }  
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == Bestiary.GetMonsterTag)
        {
            damageCountdown = 0;
            other.GetComponent<IBurning>().startBurning(howLong, tickPeriod, tickDamage);
        }
    }

    public void SetRotation(float y)
    {
        transform.rotation = Quaternion.Euler(90, 90 + y, 90);
    }
}
