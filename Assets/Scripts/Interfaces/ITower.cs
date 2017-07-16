using UnityEngine;
using System.Collections;

public interface ITower
{
    void Shoot();

    void UpdateTarget(Transform target);

    void Rotate(Quaternion rotation,float rotationSpeed);
}
