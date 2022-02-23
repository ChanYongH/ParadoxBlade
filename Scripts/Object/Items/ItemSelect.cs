using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSelect : Shield // �÷��̾� �ǵ忡�� ��� ����
{
    public GameObject[] setItemObj = new GameObject[6]; // ���п��� �� �������� ������
    public GameObject[] itemUseTrigger = new GameObject[6]; // �������� �����ϸ� Ʈ���Ű� Ȱ��ȭ ��

    // �κ��丮�� Ȱ��ȭ �Ǹ� Active�Ǵ� ���ο� �հ���(�ݶ��̴�) -> Player���̶�Ű���� Finger�� ����
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<Item>() != null)
            AudioManager.PlaySfx(Resources.Load<AudioClip>("Sound/SFX/ItemSelect")); // ����ó��
    }
    // ������ ���� �κ�(Item.kind�� �÷��̾� �κ��丮 Ȱ��)
    private void OnTriggerStay(Collider other)
    {
        Item item;
        item = other.GetComponent<Item>();
        if (item != null)
        {
            item.GetComponent<MeshRenderer>().material.color = Color.red;
            // ������ ����
            setItem = item.kind; // �������� ������ �ҷ���
            for (int i = 0; i < itemUseTrigger.Length; i++)
            {
                itemUseTrigger[i].SetActive(false);
                setItemObj[i].SetActive(false);
            }
            itemUseTrigger[setItem].SetActive(true); // �ش� ������ Ʈ���� Ȱ��ȭ
            setItemObj[setItem].SetActive(true); // ���� ������ �������� ���� ��

        }
    }
    // ���׸��� ���� ���� ������ �ٲ�
    public override void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Item>() != null)
            other.GetComponent<MeshRenderer>().material.color = Color.white;
    }
}
