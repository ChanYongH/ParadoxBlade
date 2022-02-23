using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Item : MonoBehaviour
{
    public string itemName; // 아이템 이름(자식에서 정해 줘야 함)
    public int kind = 0; // 아이템의 종류(아이템을 구매하거나 사용 할 때 사용, 인벤토리도 처리)
    // 아이템 UI처리
    public GameObject infomation = null;  
    public TextMeshProUGUI infoText = null; 
    //public List<GameObject> Effects;
    public bool itemUse = true; // 아이템 사용 대기시간
    public float activeTime; // 아이템 지속시간
    public GameObject itemEffect = null; // 아이템 파티클, 오브젝트

    /// /////////////////////////////////
    /// 여기서 부터 Han's 코드
    public GameObject player;
    public GameObject enemy;

    //플레이어와 에너미를 불러옴
    public virtual void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        enemy = GameObject.FindGameObjectWithTag("Enemy");
    }

    /// 여기까지 Han's 코드
    //아이템 사용
    public virtual void ItemEffect()
    {
    }
    //아이템 정보 보기
    public virtual void ShowInfo(bool state)
    { 
    }
    //아이템 재사용 대기시간 코루틴
    public IEnumerator ItemTimeCo()
    {
        itemUse = false;
        yield return new WaitForSeconds(5);
        itemUse = true;
    }
}
