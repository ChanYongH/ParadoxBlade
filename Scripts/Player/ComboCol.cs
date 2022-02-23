using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboCol : MonoBehaviour
{
    //�÷��̾� �ڿ� �����Ǵ� Ʈ����(��� ����� �ޱ� ���� Ʈ������)
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Sword"))
        {
            other.transform.GetChild(1).gameObject.SetActive(true);
            Enemy.comboCol = true;
            AudioManager.PlaySfx(Resources.Load<AudioClip>("Sound/SFX/ComboCol"));
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Sword"))
            StartCoroutine(SwordPartCo(other)); // �޺� �ݶ��̴�(����)�� Ȱ��ȭ �ϰ� ����
    }
    //��ƼŬ ó��
    IEnumerator SwordPartCo(Collider swordPart)
    {
        yield return new WaitForSeconds(0.4f);
        swordPart.transform.GetChild(1).gameObject.SetActive(false);
    }
}
