using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tazdingo : Item
{
    //////////////////////////////////
    //han's �ڵ�
    //���ӽð�
    IEnumerator TazCo()
    {
        player.transform.GetComponent<Player>().isImmortality = true;
        yield return new WaitForSeconds(activeTime);     // 5�ʰ� ����
        player.transform.GetComponent<Player>().isImmortality = false;
    }

    //////////////////////////////////
    void Awake()
    {
        itemUse = true; //���� �����ָ� false������
    }
    public override void ItemEffect()
    {
        if (itemUse)
        {
            StartCoroutine(ItemTimeCo());
            //han's �ڵ�
            StartCoroutine(TazCo());
            Player.inventory[2]--;
        }
    }
}
