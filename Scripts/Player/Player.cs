using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;

public class Player : Character
{
    public static int[] inventory = new int[6] { 0, 0, 0, 0, 0, 0 }; // 인벤토리 소지유무 처리
    public static float[,] itemForce = new float[3,3]; // nomal ,불, 화학, emp
    
    private bool isLoad = false; // 불러오기 관련
    public GameObject playerSword; // 파티클 및 매테리얼 변경
    public GameObject attackCol = null; // 휘둘렀을 때 활성화(Player Attack)
    public List<GameObject> uiPlayerHp; // PlayerHpUI처리
    public int money;
    public Queue<int> setAttribQueue = new Queue<int>(); // 속성 변경

    public int enemyGuardAttack = 0; // 적의 가드를 때리면 ++
    public List<GameObject> enemyGuardState; // enemyGuardAttack -> UI

    public bool isImmortality = false; // 죽음 지연 물약을 사용 하면
    public GameObject tazdingo; // 아이템 메테리얼

    public bool[] soundDelay = new bool[3]; // 사운드 한 번만 들리게 처리
    public string playerName = "";

    //초기화
    public virtual void Awake()
    {
        uiPlayerHp = new List<GameObject>();
        for (int i = 0; i < 4; i++)
            setAttribQueue.Enqueue(i);
        for (int i = 0; i < GameObject.Find("PlayerHp").transform.childCount; i++)
            uiPlayerHp.Add(GameObject.Find("PlayerHp").transform.GetChild(i).gameObject);
        for (int i = 0; i < GameObject.Find("EnemyShieldAttack").transform.childCount; i++)
            enemyGuardState.Add(GameObject.Find("EnemyShieldAttack").transform.GetChild(i).gameObject);
    }
    //PlayerUI관련 실시간 처리
    //PlayerAttack관련 처리(벨로시티로 계산)

    public void Update()
    {
        if (isLoad == false) // 불러오기
        {
            FileManager.OnLoad();
            isLoad = true;
            money = FileManager.loadMoney;
            MaxHp = FileManager.loadMaxHp;
            Hp = FileManager.loadHp;
            Att = FileManager.loadAtt;
        }
        if (isImmortality)
            tazdingo.SetActive(true);
        else
            tazdingo.SetActive(false);
        if (enemyGuardAttack > 2) // 몬스터 실드를 3번 쳤을 시 Hp가 감소
        {
            Hp--;
            enemyGuardAttack = 0;
        }
        for (int i = 0; i < enemyGuardState.Count; i++)
            enemyGuardState[i].SetActive(false);
        for (int i = 0; i < enemyGuardAttack; i++)
            enemyGuardState[i].SetActive(true);
        //PlayerHp처리(UI)
        for (int i = 0; i < uiPlayerHp.Count; i++)
            uiPlayerHp[i].SetActive(false);
        for (int i = 0; i < Hp; i++)
            uiPlayerHp[i].SetActive(true);
        if (setAttrib >= 1)
            playerSword.transform.GetChild(0).gameObject.SetActive(true); // 파티클
        else
            playerSword.transform.GetChild(0).gameObject.SetActive(false);

        if (OVRInput.GetDown(OVRInput.Button.One))
            SetAttribute();
        //받는 힘이 3이상이고 검을 장착하고있으면 발동
        bool basicAttack = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RHand).magnitude >= 3f && 
            ReturnPlace.sword;

        if (basicAttack)
        {
            //속성에 따라 다른 사운드 출력
            if (setAttrib == 1 && soundDelay[0])
            {
                AudioManager.PlaySfx(Resources.Load<AudioClip>("Sound/SFX/FireAttack"));
                soundDelay[0] = false;
            }
            else if (setAttrib == 2 && soundDelay[1])
            {
                AudioManager.PlaySfx(Resources.Load<AudioClip>("Sound/SFX/SangAttack"));
                soundDelay[1] = false;
            }
            else if (setAttrib == 3 && soundDelay[2])
            {
                AudioManager.PlaySfx(Resources.Load<AudioClip>("Sound/SFX/EmpAttack"));
                soundDelay[2] = false;
            }
            if (attackCol != null)
            {
                attackCol.SetActive(true); // 어택 콜라이더 활성화 
                attackCol.transform.GetChild(0).gameObject.SetActive(true); // 파티클
            }
        }
        else
        {
            for (int i = 0; i < soundDelay.Length; i++)
                soundDelay[i] = true;
            //어택 콜라이더 비활성화
            if (attackCol != null)
            {
                attackCol.SetActive(false);
                attackCol.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
    //죽음(씬 넘김)
    public override void Dead([CallerMemberName] string caller = "")
    {
        if (isImmortality)
            Hp = 1;
        else
        {
            SceneManager.LoadScene("Player_Lose_Ending");
            AudioManager.ChangeBgm(Resources.Load<AudioClip>("Sound/Music/GameOver"));
        }
    }
    public override void OnCrisis()
    {
        AudioManager.PlaySfx(Resources.Load<AudioClip>("Sound/SFX/Heartbeat"));
    }
    //속성 변경(파티클, 마테리얼, 사운드 변경)
    public void SetAttribute()
    {
        setAttrib = setAttribQueue.Dequeue();
        //리소스에 있는 메테리얼로 외형(색깔)을 변경
        playerSword.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Sword7/" + setAttrib);

        setAttribQueue.Enqueue(setAttrib);
        //바꿀 때 마다 사운드를 재생하고 파티클의 색깔을 바꿔줌
        switch(setAttrib)
        {
            case 1:
                AudioManager.PlaySfx(Resources.Load<AudioClip>("Sound/SFX/SetAttributefire"));
                playerSword.transform.GetChild(0).GetComponent<ParticleSystem>().startColor = Color.red;
                break;
            case 2:
                AudioManager.PlaySfx(Resources.Load<AudioClip>("Sound/SFX/SetAttribSang"));
                playerSword.transform.GetChild(0).GetComponent<ParticleSystem>().startColor = Color.green;
                break;
            case 3:
                AudioManager.PlaySfx(Resources.Load<AudioClip>("Sound/SFX/SetAttribEMP"));
                playerSword.transform.GetChild(0).GetComponent<ParticleSystem>().startColor = Color.blue;
                break;
        }
    }

}
