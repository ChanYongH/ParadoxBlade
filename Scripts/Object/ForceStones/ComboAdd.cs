using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAdd : Stones
{
    private void Awake()
    {
        itemName = "�޺� ��ȭ��";
        itemEffect = "�޺� ī��Ʈ ����";
    }
    public override void AddAbility()
    {
        if(Enemy.comboCount <= 4)
            Enemy.comboCount++;
        base.AddAbility();
    }
}
