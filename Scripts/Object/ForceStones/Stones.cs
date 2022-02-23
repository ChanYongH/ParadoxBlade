using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Stones : MonoBehaviour
{
    protected Player player;
    // public GameObject rightHandCamera;// 흔들리는 효과를 넣으려 했으나 멀미 때문에 제거
    public GameObject stayHand;// 강화석을 쥐고 트리거 했을 때 나오는 파티클
    public GameObject destoryStone;// 강화석을 부셨을 때 나오는 파티클
    public string itemName; // 강화석 이름(캔버스에 사용)
    public string itemEffect; // 강화석 효과(캔버스에 사용)
    //UI처리
    public GameObject itemInfoCanvas;
    public GameObject selectCanvas;
    public TextMeshProUGUI infoText;
    public TextMeshProUGUI infoText2;

    public static int stoneLevel = 0; // 스테이지가 올라가면 스톤레벨이 올라가서 올라가는 량이 증가함
    public static bool isStoneDestory = false; // 강화석이 파괴되면 다음 스테이지로 갈 수있게 처리 하기위해 사용
    public static List<string> forceListString = new List<string>(); // 모든 신에서 볼 수 있게 하기 위해서 static처리
    public int attrib = 0; // 속성에 맞게 강화되게 하기위해 넣음(Player.attrib을 가져와서 일치시킴)
    private float _time; // 타이머
    private bool timer = false; // 타이머
    private bool grabOn = false; // 각자의 객체에서 처리 될 수 있도록 처리(스태틱 그랩은 다 같이 작동해서 문제생김) 
                                 //객체 하나하나의 그랩 여부
    private TextMeshProUGUI getForce; // 현재 스테이지에서 얻은 능력
    private TextMeshProUGUI forceListText; // 얻은 능력들
    private GameObject store; // 상점을 나왔을 때 처리하기 위해 사용
    private bool onStore = false; // 상점을 나왔는지 안나왔는지 확인

    //초기화
    public void OnEnable()
    {
        isStoneDestory = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        stayHand = player.transform.GetChild(0).GetChild(0).GetChild(6).GetChild(0).gameObject;
        destoryStone = player.transform.GetChild(0).GetChild(0).GetChild(6).GetChild(1).gameObject;
        itemInfoCanvas = GameObject.FindGameObjectWithTag("MainCanvas");
        infoText = itemInfoCanvas.transform.GetChild(0).GetChild(1).GetComponent<TextMeshProUGUI>();
        infoText2 = itemInfoCanvas.transform.GetChild(0).GetChild(2).GetComponent<TextMeshProUGUI>();
        selectCanvas = GameObject.FindGameObjectWithTag("SelectCanvas");
        getForce = selectCanvas.transform.GetChild(0).GetChild(3).GetComponent<TextMeshProUGUI>();
        forceListText = selectCanvas.transform.GetChild(0).GetChild(4).GetComponent<TextMeshProUGUI>();
        store = GameObject.FindGameObjectWithTag("Store");
    }
    //일정시간동안 트리거 누르면 파괴, 다음 스테이지로 가는 캔버스
    public void Update()
    {
        if (OVRGrabber.grab && grabOn)
            StoneTrigger();
        //그립 상태에서 일정 시간동안 트리거를 누르고 있으면 발생 
        if (!timer)
            _time = Time.time;
        if (Time.time > _time)
        {
            AddAbility();
        }
        if (isStoneDestory)
        {
            gameObject.SetActive(false);
            if(!onStore)
                selectCanvas.transform.GetChild(0).gameObject.SetActive(true);
            else
                selectCanvas.transform.GetChild(0).gameObject.SetActive(false);
        }
    }
    //시간 설정
    public void DestoryTime(float time, bool state)
    {
        timer = state;
        _time = Time.time + time;
        stayHand.SetActive(state);
    }
    //일정시간동안 트리거 누르면 파괴 실질적으로 처리
    public void StoneTrigger()
    {
        if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger)) //핸드 그립 상태//if(m_grabbedObj.tag != "Sword")
        {
            AudioManager.PlaySfx(Resources.Load<AudioClip>("Sound/SFX/StoneStay"));
            Debug.Log("트리거 다운");
            //3초 동안 트리거를 누르고 있으면
            DestoryTime(3, true);
        }
        else if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
        {
            Debug.Log("트리거 업");
            DestoryTime(0, false);
        }
        else
            Debug.Log("트리거 안됨");
    }
    //파괴했을 때 동일하게 적용되는 이벤트들(자식에서 base.AddAbility로 사용)
    public virtual void AddAbility()
    {
        AudioManager.PlaySfx(Resources.Load<AudioClip>("Sound/SFX/Stonedestroy"));
        AudioManager.PlaySfx(null);
        stayHand.SetActive(false);
        destoryStone.SetActive(true);
        //강화목록
        getForce.text = itemEffect;
        forceListString.Add(itemEffect);
        for (int i = 0; i < forceListString.Count; i++)
            forceListText.text += System.Environment.NewLine + forceListString[i];

        FileManager.AutoSave(Player.inventory, player.money,
                        (int)player.MaxHp, (int)player.Hp,
                        (int)player.Att, ++FileManager.loadStageCount);
        gameObject.SetActive(false);
        isStoneDestory = true;
        //랜덤 확률로 상점가기
        //int ranNum;
        //ranNum = Random.Range(0, 1);
        //if (ranNum % 2 == 0)
        //if(ranNum == 0)
            //OnStore();
    }
    //상점에 가면
    public void OnStore()
    {
        onStore = true;
        store.transform.GetChild(0).gameObject.SetActive(true);
    }
    //강화석 정보 보여주기
    public void ShowInfo()
    {
        itemInfoCanvas.transform.GetChild(0).gameObject.SetActive(true);
        infoText.text = itemName;
        infoText2.text = itemEffect; 
    }

    //객체 하나하나의 그랩 불값을 조정
    public void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<OVRGrabber>() != null)
            grabOn = true;
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<OVRGrabber>() != null)
            grabOn = false;
    }

}
