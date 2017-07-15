using UnityEngine;
using System.Collections;

public interface ITower
{
    void Shoot();

    void Rotate(Quaternion rotation,float rotationSpeed);

    void UpdateTarget(Transform target);
}
