using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;
using UnityEngine.SceneManagement;

public class Player : Character
{
    public static int[] inventory = new int[6] { 0, 0, 0, 0, 0, 0 }; // �κ��丮 �������� ó��
    public static float[,] itemForce = new float[3,3]; // nomal ,��, ȭ��, emp
    
    private bool isLoad = false; // �ҷ����� ����
    public GameObject playerSword; // ��ƼŬ �� ���׸��� ����
    public GameObject attackCol = null; // �ֵѷ��� �� Ȱ��ȭ(Player Attack)
    public List<GameObject> uiPlayerHp; // PlayerHpUIó��
    public int money;
    public Queue<int> setAttribQueue = new Queue<int>(); // �Ӽ� ����

    public int enemyGuardAttack = 0; // ���� ���带 ������ ++
    public List<GameObject> enemyGuardState; // enemyGuardAttack -> UI

    public bool isImmortality = false; // ���� ���� ������ ��� �ϸ�
    public GameObject tazdingo; // ������ ���׸���

    public bool[] soundDelay = new bool[3]; // ���� �� ���� �鸮�� ó��
    public string playerName = "";

    //�ʱ�ȭ
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
    //PlayerUI���� �ǽð� ó��
    //PlayerAttack���� ó��(���ν�Ƽ�� ���)

    public void Update()
    {
        if (isLoad == false) // �ҷ�����
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
        if (enemyGuardAttack > 2) // ���� �ǵ带 3�� ���� �� Hp�� ����
        {
            Hp--;
            enemyGuardAttack = 0;
        }
        for (int i = 0; i < enemyGuardState.Count; i++)
            enemyGuardState[i].SetActive(false);
        for (int i = 0; i < enemyGuardAttack; i++)
            enemyGuardState[i].SetActive(true);
        //PlayerHpó��(UI)
        for (int i = 0; i < uiPlayerHp.Count; i++)
            uiPlayerHp[i].SetActive(false);
        for (int i = 0; i < Hp; i++)
            uiPlayerHp[i].SetActive(true);
        if (setAttrib >= 1)
            playerSword.transform.GetChild(0).gameObject.SetActive(true); // ��ƼŬ
        else
            playerSword.transform.GetChild(0).gameObject.SetActive(false);

        if (OVRInput.GetDown(OVRInput.Button.One))
            SetAttribute();
        //�޴� ���� 3�̻��̰� ���� �����ϰ������� �ߵ�
        bool basicAttack = OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RHand).magnitude >= 3f && 
            ReturnPlace.sword;

        if (basicAttack)
        {
            //�Ӽ��� ���� �ٸ� ���� ���
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
                attackCol.SetActive(true); // ���� �ݶ��̴� Ȱ��ȭ 
                attackCol.transform.GetChild(0).gameObject.SetActive(true); // ��ƼŬ
            }
        }
        else
        {
            for (int i = 0; i < soundDelay.Length; i++)
                soundDelay[i] = true;
            //���� �ݶ��̴� ��Ȱ��ȭ
            if (attackCol != null)
            {
                attackCol.SetActive(false);
                attackCol.transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }
    //����(�� �ѱ�)
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
    //�Ӽ� ����(��ƼŬ, ���׸���, ���� ����)
    public void SetAttribute()
    {
        setAttrib = setAttribQueue.Dequeue();
        //���ҽ��� �ִ� ���׸���� ����(����)�� ����
        playerSword.GetComponent<MeshRenderer>().material = Resources.Load<Material>("Sword7/" + setAttrib);

        setAttribQueue.Enqueue(setAttrib);
        //�ٲ� �� ���� ���带 ����ϰ� ��ƼŬ�� ������ �ٲ���
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
