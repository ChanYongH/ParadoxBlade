using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour
{
    bool enter = true;
    public GameObject stopObj;
    public GameObject trackingObj;
    public Quaternion shieldRotate;
    public static bool selectItem = false; // 한 번만 실행 되게
    public bool defence = false;
    Player player;

    public GameObject[] showItem = new GameObject[6] { null, null, null, null, null, null};
    public int setItem; // 아이템 세팅
    public GameObject canvas = null; // 아이템 창
    public GameObject finger = null ; // 아이템 창이 켜졌을 때 실행
    public bool useItem = false; // 휘두르면 사용 하게 함

    ////////////////////////////////////////////////////////////////////////
    ///Han's 코드
    public bool isHelmet = false;
    public GameObject helmetEffect = null;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }
    //아이템 사용, 방어 성공,실패 처리
    private void OnTriggerEnter(Collider other)
    {
        Item item;
        item = other.GetComponent<Item>();

        if (other.CompareTag("UseItem"))
            item.ItemEffect();
        if (other.CompareTag("EnemyAttack") && !defence && isHelmet == false)//isHelmet을 조건에 추가
        {
            player.Hp -= 1;
            AudioManager.PlaySfx(Resources.Load<AudioClip>("Sound/SFX/PlayerHit"));
        }
        if(other.CompareTag("EnemyAttack") && defence) //방어 성공
        {
            AudioManager.PlaySfx(Resources.Load<AudioClip>("Sound/SFX/GuardSuccess"));
        }
        //추가 코드. 적의 공격 타이밍이며, 방어에 실패했지만, 헬멧을 쓰고 있는 경우
        if (other.CompareTag("EnemyAttack") && !defence && isHelmet == true)       //isHelmet을 조건에 추가
            isHelmet = false;
        if(other.CompareTag("ShieldZone"))
            AudioManager.PlaySfx(Resources.Load<AudioClip>("Sound/SFX/PlayerGuradStay"));
    }
    //방어 하고 있을 때
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("ShieldZone"))
        {
            defence = true;
            other.GetComponent<MeshRenderer>().material.color = Color.green;
        }
    }
    //방어를 안할 때
    public virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("ShieldZone"))
        {
            other.GetComponent<MeshRenderer>().material.color = Color.white;
            defence = false;
        }
    }
    //버튼을 눌렀을 때 아이템 인벤토리 보기
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.Three))
        {
            AudioManager.PlaySfx(Resources.Load<AudioClip>("Sound/SFX/ItemOpen"));
            Time.timeScale = 0.1f;
            ShowInventory(true);
            finger.SetActive(true);
            transform.SetParent(stopObj.transform, true);
        }
        else if(OVRInput.GetUp(OVRInput.Button.Three))
        {
            Time.timeScale = 1f;
            selectItem = false;
            ShowInventory(false);
            finger.SetActive(false);
            transform.SetParent(trackingObj.transform, false);
            transform.position = trackingObj.transform.position;
            transform.localRotation = shieldRotate;
        }
        if (OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LHand).magnitude >= 2.5f)
            useItem = true;
        else
            useItem = false;
        if (helmetEffect != null)
        {
            if (isHelmet)
                helmetEffect.SetActive(true); // 아이 앵커에 있음
            else
                helmetEffect.SetActive(false);
        }
    }
    void ShowInventory(bool state)
    {
        canvas.SetActive(state);
        if (state)
        {
            for (int i = 0; i < showItem.Length; i++)
            {
                if (Player.inventory[i] > 0)
                    showItem[i].SetActive(true);
                else
                    showItem[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < showItem.Length; i++)
                showItem[i].SetActive(false);
        }
    }
}
