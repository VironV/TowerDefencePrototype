using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    [Header("Settings")]
    public int startMoney = 500;
    public int startHealth = 100;

    private static int _Money;
    private static int _Health;

    private static int _startMoney;
    private static int _startHealth;

    public static int Health { get { return _Health; } }
    public static int Money { get { return _Money; } }

    private void Start()
    {
        _startHealth = startHealth;
        _startMoney = startMoney;
        ResetStats();
    }

    public static void ResetStats()
    {
        _Money = _startMoney;
        _Health = _startHealth;
    }

    public static bool EnoughMoney(int cost)
    {
        return cost <= _Money;
    }

    public static bool ChangeCurrency(int change)
    {
        if (_Money + change < 0)
        {
            Debug.Log("Not enough money!");
            return false;
        }

        _Money += change;
        return true;
    }

    public static void DamagePlayer(int damage)
    {
        _Health -= damage;
    }
}
