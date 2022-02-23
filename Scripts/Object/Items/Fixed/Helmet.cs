using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helmet : Item
{
    //////////////////////////////////////
    ///Han�� �߰� 
    public GameObject shield;       //�ν����� â�� ���� �ֱ�. ������ ���� ����� �ִٸ� ���ʵ� ������
    void Awake()
    {
        itemUse = true; //���� �����ָ� false������
    }
    //base.Start(�÷��̾�, ���ʹ� �ҷ���)
    public override void Start()
    {
        base.Start();
    }
    //������ ��� ó��(�κ��丮[kind]--, ȿ��)
    public override void ItemEffect()
    {
        StartCoroutine(ItemTimeCo()); // ������ ���� ���ð�
        shield.transform.GetComponent<Shield>().isHelmet = true; // shield���� ������ ��� ó��
        Player.inventory[0]--;
    }
    //������ ����(�ѱ� ����)
    public override void ShowInfo(bool state)
    {
        infomation.SetActive(state);
        infoText.text = itemName;
    }
}
