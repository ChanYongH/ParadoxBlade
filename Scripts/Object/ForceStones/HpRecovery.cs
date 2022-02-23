using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpRecovery : Stones
{
    private void Awake()
    {
        itemName = "체력 회복석";
        itemEffect = "Hp 모두 회복";
    }
    public override void AddAbility()
    {
        player.Hp = player.MaxHp;
        base.AddAbility();
    }
}
