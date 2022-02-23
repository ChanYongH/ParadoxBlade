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
    //비활성화 됐을 때 원래 색깔로 돌아감
    void OnDisable()
    {
        mesh.material.color = Color.white;
    }
}
