using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAddForce : Item
{
    //////////////////////////////////
    //han's �ڵ�. AF�� �����ϰ� ���
    //�ٸ� �ڵ忡���� isAddAF�� �ʿ信 �°� ������ ��
    //���ӽð�
    IEnumerator AddForceCo()
    {
        enemy.GetComponent<Enemy>().isAddAF = true;
        yield return new WaitForSeconds(activeTime);
        enemy.GetComponent<Enemy>().isAddAF = false;
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
            Player.inventory[3]--;
            ////////////////////////////////////////////////
        }
    }
}
