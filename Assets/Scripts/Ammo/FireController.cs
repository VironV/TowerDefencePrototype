using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireController : MonoBehaviour, IRotatable {

    [Header("Settings")]
    public int damage = 10;
    public float howLong = 5;
    public float period = 0.5f;
    public int tickDamage = 10;

    [Header("Techical")]
    public GameObject particles;
    public Vector3 positionOffset;
    public Vector3 rotationOffest;

    private void Start()
    {
        Quaternion rotation = Quaternion.Euler(transform.rotation.eulerAngles.x + rotationOffest.x,
                                            transform.rotation.eulerAngles.y + rotationOffest.y,
                                            transform.rotation.eulerAngles.z + rotationOffest.z);
        GameObject fire = Instantiate(particles, transform.position + positionOffset, rotation);
        fire.transform.parent = transform;
    }

    private void OnTriggerStay(Collider other)
    {
        other.GetComponent<IMonster>().GetDamage(damage);
        other.GetComponent<IBurning>().startBurning(howLong, period, tickDamage);
    }

    private void OnTriggerExit(Collider other)
    {
        other.GetComponent<IMonster>().GetDamage(damage);
        other.GetComponent<IBurning>().startBurning(howLong, period, tickDamage);
    }

    public void SetRotation(float y)
    {
        transform.rotation = Quaternion.Euler(90, 90 + y, 90);
    }
}
