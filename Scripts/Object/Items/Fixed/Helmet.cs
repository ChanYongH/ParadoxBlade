using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helmet : Item
{
    //////////////////////////////////////
    ///Han이 추가 
    public GameObject shield;       //인스펙터 창에 직접 넣기. 별도의 셋팅 방법이 있다면 그쪽도 괜찮음
    void Awake()
    {
        itemUse = true; //정의 안해주면 false상태임
    }
    //base.Start(플레이어, 에너미 불러옴)
    public override void Start()
    {
        base.Start();
    }
    //아이템 사용 처리(인벤토리[kind]--, 효과)
    public override void ItemEffect()
    {
        StartCoroutine(ItemTimeCo()); // 아이템 재사용 대기시간
        shield.transform.GetComponent<Shield>().isHelmet = true; // shield에서 아이템 사용 처리
        Player.inventory[0]--;
    }
    //아이템 정보(켜기 끄기)
    public override void ShowInfo(bool state)
    {
        infomation.SetActive(state);
        infoText.text = itemName;
    }
}
