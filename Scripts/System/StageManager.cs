using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

using System.Runtime.CompilerServices;

public class StageManager : GameManager<StageManager>
{
    //Vector3 startPosition;
    public GameObject player;       //저장된 플레이어 정보가 없을 경우에 사용. 합칠 때, 이곳에 플레이어 프리팹을 넣어줄것
    public static GameObject playerClone;
    public static int stageNumber;
    public Text tempText1 = null;
    public static Text stageCheck = null;
    public Text tempText2 = null;
    public static Text fieldCheck = null;
    //public static int fieldMax = 6;

    public static bool isCreate = false;
    public static int maxStage = 4;

    // Start is called before the first frame update
    void Start()
    {
        FileManager.OnLoad();
        //player = FileManager.loadPlayer;
        //Debug.Log(FileManager.loadPlayer);

        //딜레이를 넣을 필요가 있다면 이곳에
        //playerClone = player;

        //스테이지가 몇인지, 실제 사용되고 있는 필드(씬)는 무엇인지 보여주기
        //stageCheck = tempText1;
        //fieldCheck = tempText2;
    }

    void OnEnable()
    {
        // 씬 매니저의 sceneLoaded에 체인을 건다.
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        FileManager.OnLoad();

        if(FileManager.loadStageCount <= maxStage)
            Instantiate(player);
        //Instantiate(FileManager.loadPlayer);
    }

    public static void ChangeStage()
    {
        FileManager.OnLoad();

        stageNumber = FileManager.loadStageCount;
        //Debug.Log(stageNumber + "_스테이지");
        //stageCheck.text = "stageCount: " + stageNumber;

        //Debug.Log(FileManager.loadStageCount);
        if (FileManager.loadStageCount > maxStage)
        {
            SceneManager.LoadScene("Player_Win_Ending");
            AudioManager.ChangeBgm(Resources.Load<AudioClip>("Sound/Music/GameClear"));
        }
        else if (stageNumber > 6 || stageNumber <= 6)
        {
            stageNumber %= 7;
            SceneManager.LoadScene(stageNumber + "_Stage");
        }

        //fieldCheck.text = "fieldcheck: " + stageNumber;
        AudioManager.ChangeBgm(Resources.Load<AudioClip>("Sound/Music/Battle"));

        //씬 이름은 "stageCount_이름"으로 구성
        //SceneManager.LoadScene(stageNumber + "_Stage");
        //Debug.Log(stageNumber + "_스테이지");
    }
}
