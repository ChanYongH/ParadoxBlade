using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Decoy : Item
{
    ///�� ������ Han's �ڵ� 
    public GameObject guardManager;         //�ν����Ϳ��� ���� �ְų�, ������ �ʱ�ȭ�� ��ĥ ��.
    IEnumerator DisarmCo()
    {
        guardManager.SetActive(false);
        yield return new WaitForSeconds(activeTime);
        guardManager.SetActive(true);
    }
    ///�� ���� Han's �ڵ� 
    void Awake()
    {
        itemUse = true; //���� �����ָ� false������
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

            //han's �ڵ�
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
