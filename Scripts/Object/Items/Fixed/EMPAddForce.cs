using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EMPAddForce : Item
{
    //////////////////////////////////
    //han's �ڵ�. AF�� �����ϰ� ���
    //�ٸ� �ڵ忡���� isAddAF�� �ʿ信 �°� ������ ��

    IEnumerator AddForceCo()
    {
        enemy.GetComponent<Enemy>().isAddCY = true;
        yield return new WaitForSeconds(activeTime);
        enemy.GetComponent<Enemy>().isAddCY = false;
    }
    //////////////////////////////////
    
    void Awake()
    {
        itemUse = true;         //���� �����ָ� false������
    }
    public override void ItemEffect()
    {
        if (itemUse)
        {
            StartCoroutine(ItemTimeCo());
            ////////////////////////////////////////////////
            //han's �ڵ�. �κ��丮 ���Ҵ� ��ȭ �ϸ鼭 ������ ��
            StartCoroutine(AddForceCo());
            Player.inventory[5]--;
            ////////////////////////////////////////////////
        }
    }
}
