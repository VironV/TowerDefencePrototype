using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class CastleInfoPanel : MonoBehaviour {

    [Header("Technical")]
    public Text moneyText;
    public Text HPText;
    public int changingMoneySpeed=10;
    public int changingHPSpeed = 5;

    private float money;
    private int HP;

    private void Start()
    {
        money = PlayerStats.Money;
        HP = PlayerStats.Health;
    }

    void Update () {
        money =  Mathf.Lerp(money, PlayerStats.Money, Time.deltaTime * changingMoneySpeed);
        if (Mathf.Abs(money - PlayerStats.Money) < 1)
            money = PlayerStats.Money;
        moneyText.text = "$" + (int)money;

        HP = (int)Mathf.Lerp(HP, PlayerStats.Health, Time.deltaTime * changingHPSpeed);
        HPText.text = HP + " HP";
        if (HP<=0)
        {
            HPText.color = Color.red;
        }
	}
}
