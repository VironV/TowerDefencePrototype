using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class MonsterController {

    [Header("Settings")]
    public float speed = 10;
    public int damage = 20;
    public int value = 25;
    public int startHP = 100;
    public Color damagedColor;

    [Header("Technical")]
    public float changindColorSpeed;
    public float tooClose = 0.2f;

    private Vector3[] waypoints;
    private Vector3 target;
    private int HP;
    private int waypointIndex = 0;
    private Color startColor;
    private bool changingColor;

    private IMonster monsterController;

    public void SetMonsterController(IMonster monster,Color startColor,Vector3[] waypoints)
    {
        monsterController = monster;
        HP = startHP;
        this.startColor = startColor;
        changingColor = false;
        this.waypoints = waypoints;
        target = waypoints[waypointIndex];
    }

    public void Move(Vector3 positionSelf)
    {
        Vector3 dir = target - positionSelf;
        Vector3 toMove = dir.normalized * speed * Time.deltaTime;
        float len = dir.magnitude;
        if (len!=0)
        {
            if (len < toMove.magnitude)
                monsterController.Move(dir);
            else
                monsterController.Move(toMove);
        }

        if (Vector3.Distance(positionSelf, target) <= tooClose)
            GetNextWaypoint();
    }

    void GetNextWaypoint()
    {
        if (waypointIndex >= waypoints.Length - 1)
        {
            monsterController.HitPlayer(damage);
            return;
        }

        waypointIndex++;
        target = waypoints[waypointIndex];
    }

    public void ChangeColor(Color currentColor)
    { 
        if (currentColor == damagedColor)
        {
            changingColor = false;
        }
            
        if (changingColor)
        {
            monsterController.ChangeColor(Color.Lerp(currentColor, damagedColor, Time.deltaTime * changindColorSpeed));
        }
        else
        {
            if (currentColor != startColor)
            {
                monsterController.ChangeColor(Color.Lerp(currentColor, startColor, Time.deltaTime * changindColorSpeed));
            }
        }
    }

    public void GetDamage(int inpDamage)
    {
        if (inpDamage <= 0)
            return;
        if (HP > 0)
        {
            changingColor = true;
            HP -= inpDamage;
           
            if (HP <= 0)
            {
                monsterController.Die(value);
            }

            monsterController.ChangeHealthBar((float)HP / (float)startHP);
        }
    }
   
}
