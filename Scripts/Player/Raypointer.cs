using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Raypointer : MonoBehaviour
{
    private LineRenderer laser;        // 레이저
    private RaycastHit Collided_object; // 충돌된 객체
    private GameObject currentObject;   // 가장 최근에 충돌한 객체를 저장하기 위한 객체
    public GameObject mainCanvas = null;
    public TextMeshProUGUI text = null;
    public GameObject grabSwitch = null;
    public GameObject magnetPart = null;

    public float raycastDistance = 100f; // 레이저 포인터 감지 거리
    void Start()
    {
        laser = gameObject.AddComponent<LineRenderer>(); // 스크립트가 포함된 객체에 라인 렌더러라는 컴포넌트를 넣고있다.
        // 라인이 가지개될 색상 표현
        Material material = new Material(Shader.Find("Standard"));
        material.color = new Color(0, 195, 255, 0.5f);
        laser.material = material;
        // 레이저의 꼭지점은 2개가 필요 더 많이 넣으면 곡선도 표현 할 수 있다.
        laser.positionCount = 2;
        // 레이저 굵기 표현
        laser.startWidth = 0.01f;
        laser.endWidth = 0.01f;
    }
    //레이 당했을 때, 버튼을 누르면 정해진 액션을 취함
    void Update()
    {
        laser.SetPosition(0, transform.position); // 첫번째 시작점 위치
                                                  // 업데이트에 넣어 줌으로써, 플레이어가 이동하면 이동을 따라가게 된다.
                                                  //  선 만들기(충돌 감지를 위한)
        //Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.green, 0.5f);
        if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
            magnetPart.SetActive(false);
        // 충돌 감지 시
        if (Physics.Raycast(transform.position, transform.forward, out Collided_object, raycastDistance))
        {
            laser.SetPosition(1, Collided_object.point);
            if(Collided_object.collider.gameObject.CompareTag("NextStage"))
            {
                Collided_object.collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.gray;
                if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
                    StageManager.ChangeStage();
            }
            if (Collided_object.collider.gameObject.CompareTag("Stone")) // 강화석을 레이 하면
            {
                text.text = Collided_object.transform.name;
                Collided_object.transform.GetComponent<ReturnPlace>().lightSwitch = true;
                Collided_object.transform.GetComponent<Stones>().ShowInfo();
                if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger)) // 눌렀을 때 사운드 재생
                    AudioManager.PlaySfx(Resources.Load<AudioClip>("Sound/SFX/Magnet"));
                else if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger)) // 땠을 때 사운드 
                    AudioManager.sfxController.Stop();
                if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
                {
                    Collided_object.transform.LookAt(this.transform.position); // 강화석이 플레이어 방향으로 보고
                    Collided_object.transform.Translate(Vector3.forward * 0.1f); // 끌려옴
                    OVRGrabber.grab = true;
                    magnetPart.SetActive(true);
                }
                else
                    magnetPart.SetActive(false);
            }
            else
                GameObject.FindGameObjectWithTag("MainCanvas").transform.GetChild(0).gameObject.SetActive(false);
            if (Collided_object.collider.CompareTag("NextStone"))
            {
                //AudioManager.PlaySfx(Resources.Load<AudioClip>("Sound/SFX/UIRay"));
                if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
                {
                    AudioManager.PlaySfx(Resources.Load<AudioClip>("Sound/SFX/UISelect"));
                    Collided_object.collider.GetComponent<GoForceStone>().ShowStoneScene();
                }
            }
            //상점 부분
            if (Collided_object.collider.GetComponent<Store>() != null)
            {
                //AudioManager.PlaySfx(Resources.Load<AudioClip>("Sound/SFX/ItemRay"));
                if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
                {
                    AudioManager.PlaySfx(Resources.Load<AudioClip>("Sound/SFX/BuyItem"));
                    Collided_object.collider.GetComponent<Store>().SaleItem();
                }
            }
        }
        else
        {
            // 레이저에 감지된 것이 없기 때문에 레이저 초기 설정 길이만큼 길게 만든다.
                laser.SetPosition(1, transform.position + (transform.forward * raycastDistance));
            // 최근 감지된 오브젝트가 Button인 경우
            // 버튼은 현재 눌려있는 상태이므로 이것을 풀어준다.
            if (currentObject != null)
                currentObject = null;
        }
    }

    //private void LateUpdate()
    //{
    //    // 버튼을 누를 경우        
    //    if (OVRInput.GetDown(OVRInput.Button.One))
    //    {
    //        //layser.material.color = new Color(255, 255, 255, 0.5f);
    //    }

    //    // 버튼을 뗄 경우          
    //    else if (OVRInput.GetUp(OVRInput.Button.One))
    //    {
    //        //layser.material.color = new Color(0, 195, 255, 0.5f);
    //    }
    //}
}