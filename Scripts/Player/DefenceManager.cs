using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DefenceManager : MonoBehaviour
{
    MeshRenderer mesh;
    void Awake()
    {
        mesh = gameObject.GetComponent<MeshRenderer>();
    }
    //��Ȱ��ȭ ���� �� ���� ����� ���ư�
    void OnDisable()
    {
        mesh.material.color = Color.white;
    }
}
