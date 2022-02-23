using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuardManager : MonoBehaviour
{
    Enemy enemy;
    int randomGuard;
    float randomTime;
    //han의 변수
    //bool isDisarm = false;      //초기 계획 탓에 스크립트 명은 미끼(decoy)지만, 실제 동작은 무장해제이므로 이런 변수명 지정
    void OnEnable()
    {
        enemy = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Enemy>();
        StartCoroutine(GuardCo());
    }
    //n초 뒤에 패턴이 바뀜
    IEnumerator GuardCo()
    {
        while (true)
        {
            randomGuard = Random.Range(0, transform.childCount);
            randomTime = Random.Range(1f, 3.5f);
            yield return new WaitForSeconds(randomTime);
            for(int i = 0; i < transform.childCount; i++)
                transform.GetChild(i).gameObject.SetActive(false);
            transform.GetChild(randomGuard).gameObject.SetActive(true);       //해당 코드는 아래의 else문에서 대신 수행

            //////////////////////////////////////////////
            //han's 코드
            //if (isDisarm == true)
            //    transform.GetChild(randomGuard).gameObject.SetActive(false);
            //else if(isDisarm == false)
            //    transform.GetChild(randomGuard).gameObject.SetActive(true); 
            //////////////////////////////////////////////
            if (enemy.Shield <= 0)
                break;
        }

    }
    //적의 방어막이 0이면 setfalse
    void Update()
    {
        if (enemy.Shield <= 0) // 프로퍼티로 수정 해야 함
        {
            for (int i = 0; i < transform.childCount; i++)
                transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
