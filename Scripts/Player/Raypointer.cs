using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Raypointer : MonoBehaviour
{
    private LineRenderer laser;        // ������
    private RaycastHit Collided_object; // �浹�� ��ü
    private GameObject currentObject;   // ���� �ֱٿ� �浹�� ��ü�� �����ϱ� ���� ��ü
    public GameObject mainCanvas = null;
    public TextMeshProUGUI text = null;
    public GameObject grabSwitch = null;
    public GameObject magnetPart = null;

    public float raycastDistance = 100f; // ������ ������ ���� �Ÿ�
    void Start()
    {
        laser = gameObject.AddComponent<LineRenderer>(); // ��ũ��Ʈ�� ���Ե� ��ü�� ���� ��������� ������Ʈ�� �ְ��ִ�.
        // ������ �������� ���� ǥ��
        Material material = new Material(Shader.Find("Standard"));
        material.color = new Color(0, 195, 255, 0.5f);
        laser.material = material;
        // �������� �������� 2���� �ʿ� �� ���� ������ ��� ǥ�� �� �� �ִ�.
        laser.positionCount = 2;
        // ������ ���� ǥ��
        laser.startWidth = 0.01f;
        laser.endWidth = 0.01f;
    }
    //���� ������ ��, ��ư�� ������ ������ �׼��� ����
    void Update()
    {
        laser.SetPosition(0, transform.position); // ù��° ������ ��ġ
                                                  // ������Ʈ�� �־� �����ν�, �÷��̾ �̵��ϸ� �̵��� ���󰡰� �ȴ�.
                                                  //  �� �����(�浹 ������ ����)
        //Debug.DrawRay(transform.position, transform.forward * raycastDistance, Color.green, 0.5f);
        if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger))
            magnetPart.SetActive(false);
        // �浹 ���� ��
        if (Physics.Raycast(transform.position, transform.forward, out Collided_object, raycastDistance))
        {
            laser.SetPosition(1, Collided_object.point);
            if(Collided_object.collider.gameObject.CompareTag("NextStage"))
            {
                Collided_object.collider.gameObject.GetComponent<MeshRenderer>().material.color = Color.gray;
                if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger))
                    StageManager.ChangeStage();
            }
            if (Collided_object.collider.gameObject.CompareTag("Stone")) // ��ȭ���� ���� �ϸ�
            {
                text.text = Collided_object.transform.name;
                Collided_object.transform.GetComponent<ReturnPlace>().lightSwitch = true;
                Collided_object.transform.GetComponent<Stones>().ShowInfo();
                if (OVRInput.GetDown(OVRInput.Button.SecondaryIndexTrigger)) // ������ �� ���� ���
                    AudioManager.PlaySfx(Resources.Load<AudioClip>("Sound/SFX/Magnet"));
                else if (OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger)) // ���� �� ���� 
                    AudioManager.sfxController.Stop();
                if (OVRInput.Get(OVRInput.Button.SecondaryIndexTrigger))
                {
                    Collided_object.transform.LookAt(this.transform.position); // ��ȭ���� �÷��̾� �������� ����
                    Collided_object.transform.Translate(Vector3.forward * 0.1f); // ������
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
            //���� �κ�
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
            // �������� ������ ���� ���� ������ ������ �ʱ� ���� ���̸�ŭ ��� �����.
                laser.SetPosition(1, transform.position + (transform.forward * raycastDistance));
            // �ֱ� ������ ������Ʈ�� Button�� ���
            // ��ư�� ���� �����ִ� �����̹Ƿ� �̰��� Ǯ���ش�.
            if (currentObject != null)
                currentObject = null;
        }
    }

    //private void LateUpdate()
    //{
    //    // ��ư�� ���� ���        
    //    if (OVRInput.GetDown(OVRInput.Button.One))
    //    {
    //        //layser.material.color = new Color(255, 255, 255, 0.5f);
    //    }

    //    // ��ư�� �� ���          
    //    else if (OVRInput.GetUp(OVRInput.Button.One))
    //    {
    //        //layser.material.color = new Color(0, 195, 255, 0.5f);
    //    }
    //}
}