using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBehaviour : MonoBehaviour, IMonster {

    public Image heathBar;
    public MonsterController controller;

    //private Renderer rend;
    private ISpawn spawner;

    private void Start()
    {
        //rend = GetComponent<Renderer>();
        Color changeThisVariableLater = Color.black;

        controller.SetMonsterController(this,changeThisVariableLater,WaypointsController.points);
    }

    void Update() {
        if (GameManager.IsGameEnded)
        {
            Destroy(gameObject);
            return;
        }

        //controller.ChangeColor(rend.material.color);
        controller.Move(transform.position);
    }

    // Controller intercations
    public void SetSpawner(ISpawn spawn)
    {
        spawner = spawn;
    }

    public void GetDamage(int inpDamage)
    {
        controller.GetDamage(inpDamage);
    }

    public void Move(Vector3 toMove)
    {
        transform.Translate(toMove, Space.World);
    } 

    public void ChangeColor(Color color)
    {
        //rend.material.color = color;
    }

    public void DieAndTellToSpawn()
    {
        spawner.AddToGraveyard();
    }

    public void ChangeHealthBar(float p)
    {
        heathBar.fillAmount = p;
    } 

    public void HitPlayer(int damage)
    {
        PlayerStats.DamagePlayer(damage);
        DestroySelf();
    }

    public void Die(int value)
    {
        PlayerStats.ChangeCurrency(value);
        DestroySelf();
    }

    // Destroing and calling spawner to count death
    private void DestroySelf()
    {
        spawner.AddToGraveyard();
        Destroy(gameObject);
    }
}
