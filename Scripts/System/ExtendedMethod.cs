using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ExtendedMethod
{

    //기본적인 타이머를 쓸려면 매개변수에 넣으면 안됨
    // 공격 대기시간, 파티클을 일정 시간동안만 실행시키고 싶으면 매개변수를 넣음
    //미완성임
    public static bool Timer(this float repeatTime, GameObject setActive)
    {
        float nowTime = 0;
        nowTime += Time.time;
        if (nowTime > repeatTime)
        {
            if (setActive != null)
                setActive.SetActive(false);
            nowTime = 0;
            return true;
        }
        else
        {
            if (setActive != null)
                setActive.SetActive(true);
            return false;
        }
    }

    public static bool Between(this float bet, float min, float max)
    {
        if (min <= bet && bet <= max)
            return true;
        else
            return false;
    }

    public static bool Between(this int bet, int min, int max)
    {
        if (min <= bet && bet <= max)
            return true;
        else
            return false;
    }
}
