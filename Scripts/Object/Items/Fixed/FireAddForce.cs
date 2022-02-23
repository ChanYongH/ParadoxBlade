using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireAddForce : Item
{
    //////////////////////////////////
    //han's 코드. AF만 상정하고 기술
    //다른 코드에서는 isAddAF를 필요에 맞게 수정할 것
    //지속시간
    IEnumerator AddForceCo()
    {
        enemy.GetComponent<Enemy>().isAddAF = true;
        yield return new WaitForSeconds(activeTime);
        enemy.GetComponent<Enemy>().isAddAF = false;
    }
    //////////////////////////////////
    void Awake()
    {
        itemUse = true;         //정의 안해주면 false상태임
    }
    public override void ItemEffect()
    {
        if (itemUse)
        {
            StartCoroutine(ItemTimeCo());
            ////////////////////////////////////////////////
            //han's 코드. 인벤토리 감소는 분화 하면서 수정할 것
            StartCoroutine(AddForceCo());
            Player.inventory[3]--;
            ////////////////////////////////////////////////
        }
    }
}
