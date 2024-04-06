using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PuaseAudioController : MonoBehaviour
{
    [SerializeField] VolumeSettings volume;
    [SerializeField] GameObject settingMenu;
    [SerializeField] Slider bgm;
    [SerializeField] Slider sfx;

    private void Start()
    {
        settingMenu.SetActive(false);
        bgm.value = volume.GetVolumeBGM();
        sfx.value = volume.GetVolumeSFX();
    }

    public void OpenSettingMenu()
    {
        settingMenu.SetActive(true);
    }

    public void Back()
    {
        settingMenu.SetActive(false);
    }

    public void ChangeBGM()
    {
        volume.ChangeVolumeBGM(bgm.value);
    }
    public void ChangeSFX()
    {
        volume.ChangeVolumeSFX(sfx.value);
    }

}
