using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IAmmo: IRotatable {
    void Fire(Transform go);
}

public interface IRotatable
{
    void SetRotation(float y);
}