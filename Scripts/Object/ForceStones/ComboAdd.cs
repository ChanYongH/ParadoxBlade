using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAdd : Stones
{
    private void Awake()
    {
        itemName = "콤보 강화석";
        itemEffect = "콤보 카운트 증가";
    }
    public override void AddAbility()
    {
        if(Enemy.comboCount <= 4)
            Enemy.comboCount++;
        base.AddAbility();
    }
}
