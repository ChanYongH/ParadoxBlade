using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class UiManager : GameManager<UiManager>
{
    public Canvas tempCanvas = null;
    public static Canvas canvas = null;

    public Text tempVolumeText = null;
    public static Text volumeText = null;

    //임시로 만들어 봄
    //public static  TextMeshProUGUI volumeTextTest;

    public Text tempBlindAngle = null;
    public static Text blindAngleText = null;

    public static int volume;
    public static int blindAngle;
    private bool isLoad = false;

    private void Start()
    {
        //임시
        //volumeTextTest = GameObject.Find("VolumeTextTest").GetComponent<TextMeshProUGUI>();
        //canvas = tempCanvas;
        //volumeText = tempVolumeText;
        //blindAngleText = tempVolumeText;
        //canvas.transform.GetChild(1).gameObject.SetActive(false);
        //volumeText = canvas.transform.GetChild(1).GetChild(0).GetComponent<Text>();
        //blindAngleText = canvas.transform.GetChild(1).GetChild(1).GetComponent<Text>();
    }

    private void Update()
    {
        if (isLoad == false)
        {
            //저장된 값 불러오기
            FileManager.OnLoad();
            volume = FileManager.loadVolumeLv;
            blindAngle = FileManager.loadBlindLv;
            isLoad = true;

            //시각적인 요소
            //olumeText.text = "볼륨: " + volume;
            //blindAngleText.text = "시야각: " + blindAngle;
        }
    }

    //게임 시작
    public static void PushStartButton() 
    {
        //Debug.Log(FileManager.loadPlayer);

        FileManager.AutoSave(FileManager.loadInventory, 0, 3, 3, 10,  0);

        //Debug.Log(FileManager.loadPlayer);
        //StageManager.ChangeStage(); // 여기수정
        //SceneManager.LoadScene("2_Stage");
    }

    //게임 불러오기
    public static void PushLoadButton() 
    {
        //StageManager.ChangeStage(); // 여기수정
    }

    //환경 설정
    public static void PushOptionButton()
    {
        canvas.transform.GetChild(0).gameObject.SetActive(false);
        canvas.transform.GetChild(1).gameObject.SetActive(true);
    }

    //종료 버튼
    public static void PushExitButton()
    {
        Debug.Log("장비를 정지합니다.");
        Application.Quit();
    }

    //환경 설정 -> 볼륨 올리기
    public static void PushVolumeUp()
    {
        if (volume >= 10) { }
        else
            volume++;
        volumeText.text = "볼륨: " + volume;
    }

    //임시로 만들어 봄
    //public static void SetVolume(int value)
    //{
    //    volume = value;
    //    volumeTextTest.text = value.ToString();
    //}

    //환경 설정 -> 볼륨 낮추기
    public static void PushVolumeDown()
    {
        if (volume <= 0) { }
        else
            volume--;
        volumeText.text = "볼륨: " + volume;
    }

    //환경 설정 -> 시야각 증가
    public static void PushBlindAngleUp()
    {
        if (blindAngle >= 10) { }
        else
            blindAngle++;
        blindAngleText.text = "시야각: " + blindAngle;
    }

    //환경 설정 -> 시야각 감소
    public static void PushBlindAngleDown()
    {
        if (blindAngle <= 0) { }
        else
            blindAngle--;
        blindAngleText.text = "시야각: " + blindAngle;
    }

    //환경 설정 -> 메인 메뉴로. 실질적인 저장도 수행됨
    public static void PushToMainMenu()
    {
        canvas.transform.GetChild(0).gameObject.SetActive(true);
        canvas.transform.GetChild(1).gameObject.SetActive(false);
        //조절한 값을 .json 으로 저장
        FileManager.OptionSetting(volume, blindAngle);
        AudioManager.OnVolumControl(volume);
    }
}
