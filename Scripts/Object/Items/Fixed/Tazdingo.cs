using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tazdingo : Item
{
    //////////////////////////////////
    //han's 코드
    //지속시간
    IEnumerator TazCo()
    {
        player.transform.GetComponent<Player>().isImmortality = true;
        yield return new WaitForSeconds(activeTime);     // 5초간 무적
        player.transform.GetComponent<Player>().isImmortality = false;
    }

    //////////////////////////////////
    void Awake()
    {
        itemUse = true; //정의 안해주면 false상태임
    }
    public override void ItemEffect()
    {
        if (itemUse)
        {
            StartCoroutine(ItemTimeCo());
            //han's 코드
            StartCoroutine(TazCo());
            Player.inventory[2]--;
        }
    }
}
