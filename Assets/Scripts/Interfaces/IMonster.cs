using UnityEngine;
using System.Collections;

public interface IMonster
{
    void Move(Vector3 toMove);

    void GetDamage(int damage);

    void SetSpawner(ISpawn spawn);

    void ChangeColor(Color color);

    void Die(int value);

    void HitPlayer(int damage);

    void ChangeHealthBar(float p);
}
