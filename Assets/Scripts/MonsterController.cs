using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterController : MonoBehaviour {

    public float speed = 10;
    public int damage = 20;
    public int value = 25;
    public int startHP = 100;
    public Color damagedColor;
    public float changindColorSpeed;
    public Image heathBar;
    

    private int HP;
    private Transform target;
    private int waypointIndex = 0;
    private float tooClose = 0.2f;
    private Renderer rend;
    private Color startColor;
    private bool changingColor;

    private void Start()
    {
        target = WaypointsController.points[waypointIndex];
        HP = startHP;
        rend=GetComponent<Renderer>();
        startColor = rend.material.color;
        changingColor = false;
    }


    void Update() {
        if (GameManager.gameEnded)
        {
            Destroy(gameObject);
        }

        Color color = rend.material.color;

        if (color == damagedColor)
            changingColor = false;
        if (changingColor)
        {
            rend.material.color = Color.Lerp(color, damagedColor, Time.deltaTime * changindColorSpeed);
        }
        else
        {
            if (color!=startColor)
            {
                rend.material.color = Color.Lerp(color, startColor, Time.deltaTime * changindColorSpeed);
            }
        }

        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime, Space.World);

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

    void HitPlayer()
    {
        PlayerStats.DamagePlayer(damage);
        Destroy(gameObject);
    }

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

    void Die()
    {
        PlayerStats.ChangeCurrency(value);
        Destroy(gameObject);
    }
}
