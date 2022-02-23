using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelect : Shield // 플레이어 실드에서 상속 받음
{
    public GameObject[] setItemObj = new GameObject[6]; // 방패에서 고른 아이템을 보여줌
    public GameObject[] itemUseTrigger = new GameObject[6]; // 아이템을 선택하면 트리거가 활성화 됨

    // 인벤토리가 활성화 되면 Active되는 새로운 손가락(콜라이더) -> Player하이락키에서 Finger에 있음
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Item>() != null)
            AudioManager.PlaySfx(Resources.Load<AudioClip>("Sound/SFX/ItemSelect")); // 사운드처리
    }
    // 아이템 선택 부분(Item.kind와 플레이어 인벤토리 활용)
    private void OnTriggerStay(Collider other)
    {
        Item item;
        item = other.GetComponent<Item>();
        if (item != null)
        {
            item.GetComponent<MeshRenderer>().material.color = Color.red;
            // 아이템 세팅
            setItem = item.kind; // 아이템의 종류를 불러옴
            for (int i = 0; i < itemUseTrigger.Length; i++)
            {
                itemUseTrigger[i].SetActive(false);
                setItemObj[i].SetActive(false);
            }
            itemUseTrigger[setItem].SetActive(true); // 해당 아이템 트리거 활성화
            setItemObj[setItem].SetActive(true); // 현재 장착한 아이템을 보여 줌

        }
    }
    // 메테리얼 색을 원래 색으로 바꿈
    public override void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Item>() != null)
            other.GetComponent<MeshRenderer>().material.color = Color.white;
    }
}
