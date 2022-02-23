using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnPlace : MonoBehaviour
{
    //그랩이 false일 때(아이템을 놨을 때) 원래 자리로 돌아가게 하는 클래스
    public GameObject place; // 원래 자리
    public Light lighting = null; // 레이 했을 때 라이트 처리
    public bool lightSwitch = false; 
    public Quaternion nowRotate; // 원래 회전 값
    public static bool sword = false; // 검을 들었는지 안들었는지 여기서 판별
                                      //일정 벨로시티를 받으면 활성화(전역에서 쓰이기 때문에 사용)

    void Start()
    {
        nowRotate = transform.rotation;
        if (lighting !=null)
            lighting = GetComponent<Light>();
    }

    void Update()
    {
        if (lighting != null)
        {
            if (lightSwitch)
                lighting.intensity = 3;
            else if (!lightSwitch)
                lighting.intensity = 0;
            lightSwitch = false;
        }
        if (!OVRGrabber.grab)
        {
            transform.position = place.transform.position;
            if (lighting != null)
            {
                transform.rotation = Quaternion.identity;
                sword = false;
            }
            else
            {
                transform.rotation = nowRotate;
                sword = true;
            }
        }
    }
}
