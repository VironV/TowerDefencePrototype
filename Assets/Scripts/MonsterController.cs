using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour {

    [Header("Settings")]
    public float speed = 10;
    public int damage = 20;
    public int value = 25;
    public int startHP = 100;
    public Color damagedColor;

    [Header("Technical")]
    public float changindColorSpeed;
    public Image heathBar;
    public float tooClose = 0.2f;

    private int HP;
    private Transform target;
    private int waypointIndex = 0;
    private Renderer rend;
    private Color startColor;
    private bool changingColor;
    private SpawnController spawner;

    public void SetSpawner(SpawnController spawn)
    {
        spawner = spawn;
    }

    private void Start()
    {
        target = WaypointsController.points[waypointIndex];
        HP = startHP;
        rend=GetComponent<Renderer>();
        startColor = rend.material.color;
        changingColor = false;
    }


    void Update() {
        if (GameManager.IsGameEnded)
        {
            Destroy(gameObject);
        }

        ChangeColor();
        Move();
    }

    //
    // Movings, path
    //
    private void Move()
    {
        Vector3 dir = target.position - transform.position;
        Vector3 toMove = dir.normalized * speed * Time.deltaTime;
        if (dir.magnitude < toMove.magnitude)
            transform.position = target.position;
        else
            transform.Translate(toMove, Space.World);

        if (Vector3.Distance(transform.position, target.position) <= tooClose)
        {
            GetNextWaypoint();
        }
    }

    void GetNextWaypoint()
    {
        if (waypointIndex >= WaypointsController.points.Length - 1)
        {
            HitPlayer();
            return;
        }

        waypointIndex++;
        target = WaypointsController.points[waypointIndex];
    }

    //
    // Damage and stuff
    //
    public void GetDamage(int inpDamage)
    {
        if (HP > 0)
        {
            changingColor = true;
            HP -= inpDamage;
            heathBar.fillAmount = (float)HP / (float)startHP;
            if (HP <= 0)
            {
                Die();
            }
        }
      
    }

    private void Die()
    {
        PlayerStats.ChangeCurrency(value);
        DestroySelf();
    }

    private void HitPlayer()
    {
        PlayerStats.DamagePlayer(damage);
        DestroySelf();
    }

    private void DestroySelf()
    {
        spawner.AddToGraveyard();
        Destroy(gameObject);
    }

    //
    // Visual
    //
    private void ChangeColor()
    {
        Color color = rend.material.color;

        if (color == damagedColor)
            changingColor = false;
        if (changingColor)
        {
            rend.material.color = Color.Lerp(color, damagedColor, Time.deltaTime * changindColorSpeed);
        }
        else
        {
            if (color != startColor)
            {
                rend.material.color = Color.Lerp(color, startColor, Time.deltaTime * changindColorSpeed);
            }
        }
    }
}
