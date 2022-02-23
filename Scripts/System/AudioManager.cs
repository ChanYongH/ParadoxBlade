using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : GameManager<AudioManager>
{
    public static AudioMixer mixer;
    public AudioMixer TempMixer;
    public static int soundLv;
    public AudioClip[] aClip = new AudioClip[6];
    public static AudioSource bgmController;
    public static AudioSource sfxController;
    //public dynamic isTrue;

    private void Start()
    {
        mixer = TempMixer;
        bgmController = transform.GetChild(0).GetComponent<AudioSource>();
        sfxController = transform.GetChild(1).GetComponent<AudioSource>();
    }

    private void Update()
    {
        //FileManager.OnLoad();
        //soundLv = FileManager.loadVolumeLv;
        //OnVolumControl(soundLv);
    }

    public static void ChangeBgm(AudioClip aClip)
    {
        bgmController.clip = aClip;
        bgmController.Play();
    }
    public static void PlaySfx(AudioClip aClip)
    {
        sfxController.loop = false;
        sfxController.clip = aClip;
        sfxController.PlayOneShot(aClip);
    }

    public static void PlaySfxNonOnShot(AudioClip aClip)
    {
        sfxController.clip = aClip;
        sfxController.Play();
    }

    public static void OnVolumControl(float db)
    {
        //val의 범주는 -80db ~ 20db
        //<10 -> 20>,
        //<9 -> 10>,
        //<8 -> 0>,
        //<7 -> -10>,
        //<6 -> -20>
        //<5 -> -30>,
        //<4 -> -40>,
        //<3 -> -50>,
        //<2 -> -60>,
        //<1 -> -70>,
        //<0 -> -80>
        switch (db)
        {
            case 10:
                db = 20;
                break;
            case 9:
                db = 10;
                break;
            case 8:
                db = 0;
                break;
            case 7:
                db = -10;
                break;
            case 6:
                db = -20;
                break;
            case 5:
                db = -30;
                break;
            case 4:
                db = -40;
                break;
            case 3:
                db = -50;
                break;
            case 2:
                db = -60;
                break;
            case 1:
                db = -70;
                break;
            case 0:
                db = -80;
                break;
        }
        mixer.SetFloat("MasterSoundLv", db);
        soundLv = FileManager.loadVolumeLv;
    }


}
