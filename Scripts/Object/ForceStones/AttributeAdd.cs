using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class AttributeAdd : Stones
{
    float[,] increase = new float[3, 3]; // 속성강화에서만 사용
    public static int[] forceTime = new int[3] { 0, 0, 0 };  // 속성 강화에서만 사용
    void Awake()
    {
        if (StageManager.stageNumber.Between(2, 4)) // (확장 메서드 사용) 2~4스테이지면 강화석의 레벨이 1
            stoneLevel = 1;
        else if (StageManager.stageNumber.Between(5, 7)) // (확장 메서드 사용) 5~7스테이지면 강화석의 레벨이 2
            stoneLevel = 2;
        switch (attrib)
        {
            case 0:
                itemName = "불 속성 강화석";
                itemEffect = "불 속성 공격력 " + ((stoneLevel +1) * 10) + "% 강화";
                break;
            case 1:
                itemName = "생화학 속성 강화석";
                itemEffect = "생화학 속성 공격력 " + ((stoneLevel + 1) * 10) + "% 강화";
                break;
            case 2:
                itemName = "EMP 속성 강화석";
                itemEffect = "EMP 속성 공격력 " + ((stoneLevel + 1) * 10) + "% 강화";
                break;
        }
    }
    //효과 설정
    public override void AddAbility()
    {
        forceTime[attrib]++; // 강화가 누적되게
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
                increase[i, j] = ((i + 1) * 10) / 100f; // % 만큼 강화
        }
        Player.itemForce[stoneLevel, attrib] = increase[stoneLevel, attrib]; // 플레이어의 속성에 따른 강화 값을 넣어줌
                                                                             // %한 값을 더하는건 Enemy.OnHit()에서 처리
        base.AddAbility();
    }
}

