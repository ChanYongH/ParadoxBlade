using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoy : Item
{
    ///이 밑으로 Han's 코드 
    public GameObject guardManager;         //인스펙터에서 직접 넣거나, 별도의 초기화를 거칠 것.
    IEnumerator DisarmCo()
    {
        guardManager.SetActive(false);
        yield return new WaitForSeconds(activeTime);
        guardManager.SetActive(true);
    }
    ///이 위로 Han's 코드 
    void Awake()
    {
        itemUse = true; //정의 안해주면 false상태임
    }
    public override void Start()
    {
        base.Start();
        guardManager = GameObject.Find("GuardManager");
    }
    public override void ItemEffect()
    {
        if (itemUse)
        {
            StartCoroutine(ItemTimeCo());

            //han's 코드
            StartCoroutine(DisarmCo());
            Player.inventory[1]--;
        }
    }
    public override void ShowInfo(bool state)
    {
        infomation.SetActive(state);
        infoText.text = itemName;
    }

    
}
