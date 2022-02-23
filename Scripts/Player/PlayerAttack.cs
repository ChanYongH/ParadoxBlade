using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerAttack : MonoBehaviour
{
    //휘둘렀을 때
    //몬스터 공격
    //UI처리

    Player player;
    bool uiStart = true;
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    private void OnTriggerEnter(Collider other)
    {
        //몬스터 관련
        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            if (enemy.Shield > 0)
            {
                enemy.OnHit(player.setAttrib, Player.itemForce, player.Att);
            }
        }
        if (Enemy.comboCol) // 콤보 트리거(뒤쪽)이 활성화 되면 on
        {
            if (other.CompareTag("Combo"))
            {
                AudioManager.PlaySfx(Resources.Load<AudioClip>("Sound/SFX/ComboSuccess"));
                StartCoroutine(SpearTimeCo());
                other.gameObject.SetActive(false);
                other.transform.parent.parent.parent.GetComponent<Enemy>().Hp -= 1;
                Enemy.comboCol = false;
            }
        }
        IEnumerator SpearTimeCo()
        {
            transform.GetChild(1).gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            transform.GetChild(1).gameObject.SetActive(false);
        }
        //UI관련처리
        if (other.CompareTag("UIStart") && uiStart)
        {
            player.Hp = 3;
            FileManager.AutoSave(FileManager.loadInventory, 0, 3, 3, 10, 0);
            StageManager.ChangeStage();
            uiStart = false;
        }
        if (other.CompareTag("GameQuit"))
            Application.Quit();
        if (other.CompareTag("UIVolume"))
            other.transform.GetChild(0).gameObject.SetActive(true);
        if (other.CompareTag("UIVolumeUp"))
            UiManager.PushVolumeUp();
        if (other.CompareTag("UIVolumeDown"))
            UiManager.PushVolumeDown();
        if (other.CompareTag("PlayerDeadGoMenu"))
            SceneManager.LoadScene(0);
    }
}
