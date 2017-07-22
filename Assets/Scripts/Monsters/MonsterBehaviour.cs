using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterBehaviour : MonoBehaviour, IMonster, IBurning {

    public Image heathBar;
    public MonsterController controller;

    //private Renderer rend;
    private ISpawn spawner;
    private Coroutine burning;

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

    
    public void SetSpawner(ISpawn spawn)
    {
        spawner = spawn;
    }

    // Controller intercations
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

    public void startBurning(float howLong, float period, int damagePerTick)
    {
        if (burning != null)
            StopCoroutine(burning);
        burning = StartCoroutine(toBurn(howLong,period,damagePerTick));
    }

    IEnumerator toBurn(float howLong, float period, int damagePerTick)
    {
        float currentTime = 0f;
        while (currentTime<howLong)
        {
            GetDamage(damagePerTick);
            currentTime += period;
            yield return new WaitForSeconds(period);
        }
    }
}
