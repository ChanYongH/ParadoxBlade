using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaxHpAdd : Stones
{
    private void Awake()
    {
        itemName = "ü�� ��ȭ��";
        itemEffect = "�ִ�ü�� ����";
    }
    public override void AddAbility()
    {
        player.MaxHp += stoneLevel+1;
        base.AddAbility();
    }
}
