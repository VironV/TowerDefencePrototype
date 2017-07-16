using UnityEngine;
using System.Collections;

public interface ITower
{
    void Shoot(Transform target);

    void Rotate(Quaternion rotation,float rotationSpeed);
}
