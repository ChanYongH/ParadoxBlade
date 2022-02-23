using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardManager : MonoBehaviour
{
    Enemy enemy;
    int randomGuard;
    float randomTime;
    //han�� ����
    //bool isDisarm = false;      //�ʱ� ��ȹ ſ�� ��ũ��Ʈ ���� �̳�(decoy)����, ���� ������ ���������̹Ƿ� �̷� ������ ����
    void OnEnable()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        StartCoroutine(GuardCo());
    }
    //n�� �ڿ� ������ �ٲ�
    IEnumerator GuardCo()
    {
        while (true)
        {
            randomGuard = Random.Range(0, transform.childCount);
            randomTime = Random.Range(1f, 3.5f);
            yield return new WaitForSeconds(randomTime);
            for(int i = 0; i < transform.childCount; i++)
                transform.GetChild(i).gameObject.SetActive(false);
            transform.GetChild(randomGuard).gameObject.SetActive(true);       //�ش� �ڵ�� �Ʒ��� else������ ��� ����

            //////////////////////////////////////////////
            //han's �ڵ�
            //if (isDisarm == true)
            //    transform.GetChild(randomGuard).gameObject.SetActive(false);
            //else if(isDisarm == false)
            //    transform.GetChild(randomGuard).gameObject.SetActive(true); 
            //////////////////////////////////////////////
            if (enemy.Shield <= 0)
                break;
        }

    }
    //���� ���� 0�̸� setfalse
    void Update()
    {
        if (enemy.Shield <= 0) // ������Ƽ�� ���� �ؾ� ��
        {
            for (int i = 0; i < transform.childCount; i++)
                transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}