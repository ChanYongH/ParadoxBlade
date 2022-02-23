using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnPlace : MonoBehaviour
{
    //�׷��� false�� ��(�������� ���� ��) ���� �ڸ��� ���ư��� �ϴ� Ŭ����
    public GameObject place; // ���� �ڸ�
    public Light lighting = null; // ���� ���� �� ����Ʈ ó��
    public bool lightSwitch = false; 
    public Quaternion nowRotate; // ���� ȸ�� ��
    public static bool sword = false; // ���� ������� �ȵ������ ���⼭ �Ǻ�
                                      //���� ���ν�Ƽ�� ������ Ȱ��ȭ(�������� ���̱� ������ ���)

    void Start()
    {
        nowRotate = transform.rotation;
        if (lighting !=null)
            lighting = GetComponent<Light>();
    }

    void Update()
    {
        if (lighting != null)
        {
            if (lightSwitch)
                lighting.intensity = 3;
            else if (!lightSwitch)
                lighting.intensity = 0;
            lightSwitch = false;
        }
        if (!OVRGrabber.grab)
        {
            transform.position = place.transform.position;
            if (lighting != null)
            {
                transform.rotation = Quaternion.identity;
                sword = false;
            }
            else
            {
                transform.rotation = nowRotate;
                sword = true;
            }
        }
    }
}
