using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoForceStone : MonoBehaviour
{
    public void ShowStoneScene()
    {
        transform.parent.gameObject.SetActive(false);
    }
}
