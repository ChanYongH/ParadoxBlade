using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Runtime.CompilerServices;


public class FileManager : GameManager<FileManager>
{
    public GameObject player;
    //public GameObject[] playerClone;

    //public static GameObject loadPlayer;
    public static int[] loadInventory;
    public static int loadMoney;
    public static int loadMaxHp;
    public static int loadHp;
    public static int loadAtt;
    public static int loadStageCount;

    public static int loadVolumeLv;        //범위는 1~10으로 상정
    public static int loadBlindLv;         //범위는 1~10으로 상정

    private void Start()
    {
        string filePath = Application.persistentDataPath + "/AutoSave.json";
        //loadPlayer = new GameObject();

        //플레이를 시작할 때, 세이브 파일이 없다면 하나 만든다.
        if (File.Exists(filePath))
        {
            //Debug.Log("있음");
        }
        else
        {

            //Debug.Log("없음");
            //디폴트. 초기 셋팅
            DataField firstData = new DataField();
            //저장할 데이터 초기화. 플레이어도 여기서 값을 가져온다.
            firstData.inventory = new int[6];
            firstData.money = 0;
            firstData.maxHp = 3;
            firstData.hp = firstData.maxHp;
            firstData.att = 10;
            firstData.stageCount = 1;


            firstData.volumeLv = 5;
            firstData.blindLv = 5;
            //데이터를 json타입으로 변환
            string tempData = JsonUtility.ToJson(firstData);

            //덮어쓰기(경로, 데이터)
            File.WriteAllText(filePath, tempData);
        }

        //코드 테스트
        //AutoSave(player, 12);
        //OnLoad();
        //Setting(10, 10);
        //OnLoad();               //플레이어, 12, 10, 10
    }


    /// <summary>
    /// 이 밑으로 환경 설정, 자동 저장, 불러오기에 대한 명세
    /// </summary>

    //환경 설정에서 볼륨, 시야각 조절할 때. 플레이어 정보 및 스테이지 정보를 침범하지 않기 위해 분리
    public static void OptionSetting(int volume, int blind)
    {
        //파일 경로
        string filePath = Application.persistentDataPath + "/AutoSave.json";

        //일시적으로 불러온 저장 파일
        string fromJson = File.ReadAllText(filePath);
        DataField loadData = JsonUtility.FromJson<DataField>(fromJson);

        //세이브 시작
        DataField data = new DataField();
        data.inventory = loadData.inventory;
        data.money = loadData.money;
        data.maxHp = loadData.maxHp;
        data.hp = loadData.hp;
        data.att = loadData.att;
        data.stageCount = loadData.stageCount;

        data.volumeLv = volume;
        data.blindLv = blind;
        string tempData = JsonUtility.ToJson(data);

        File.WriteAllText(filePath, tempData);
    }

    //플레이어 정보와 스테이지 정보 갱신할 때. 볼륨, 시야각 정보를 침범하지 않기 위해 분리
    public static void AutoSave(int[] invetory, int money, int maxHp, int hp, int att, int stageCount, [CallerMemberName] string caller = "")
    {
        //파일 경로
        string filePath = Application.persistentDataPath + "/AutoSave.json";

        //일시적으로 불러온 저장 파일
        string fromJson = File.ReadAllText(filePath);
        DataField loadData = JsonUtility.FromJson<DataField>(fromJson);

        //세이브 시작
        DataField data = new DataField();
        //data.player = player;
        data.inventory = new int[6];
        data.money = money;
        data.maxHp = maxHp;
        data.hp = hp;
        data.att = att;
        data.stageCount = stageCount;

        data.volumeLv = loadData.volumeLv;
        data.blindLv = loadData.blindLv;
        string tempData = JsonUtility.ToJson(data);

        File.WriteAllText(filePath, tempData);
        Debug.Log("스테이지 테스트 : " + caller);
    }

    public static void OnLoad()
    {
        //데이터 불러오기
        string filePath = Application.persistentDataPath + "/AutoSave.json";
        string fromJson = File.ReadAllText(filePath);
        DataField loadData = JsonUtility.FromJson<DataField>(fromJson);
        //Debug.Log("불러온 값: '" + loadData.player + "' / '" + loadData.stageCount + "' / '" + loadData.volumeLv + "' / '" + loadData.blindLv + "'");



        //외부에서 참조할 스태틱 변수에 값 저장
        //loadPlayer = loadData.player;
        loadInventory = loadData.inventory;
        loadMoney = loadData.money;
        loadMaxHp = loadData.maxHp;
        loadHp = loadData.hp;
        loadAtt = loadData.att;
        
        loadStageCount = loadData.stageCount;
        loadVolumeLv = loadData.volumeLv;
        loadBlindLv = loadData.blindLv;

    }
}


[System.Serializable]
public class DataField
{
    //public GameObject player;
    //플레이어 정보
    public int[] inventory;
    public int money;
    public int maxHp;
    public int hp;
    public int att;

    //스테이지 정보
    public int stageCount;

    //볼륨과 시야각
    public int volumeLv;        //범위는 1~10으로 상정
    public int blindLv;         //범위는 1~10으로 상정
}