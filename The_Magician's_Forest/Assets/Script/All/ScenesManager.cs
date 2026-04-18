using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager Instance;

    // Player
    public float ShotInterval = 0.5f;
    public int PlayerLifeMax = 3;

    // Bullet
    public float BulletDamage = 1.0f;

    //Type
    public enum BulletType
    {
        Ice,
        Fire,
        Thunder,
        Wind
    }

    public BulletType BulletTypes = BulletType.Ice;

    public float UpDamage_Ice = 1.0f;
    public float UpDamage_Fire = 0.3f;
    public float UpDamage_Thunder = 0.3f;
    public float UpDamage_Wind = 1.0f;


    public Slider EffSlider;
    public Slider BGMSlider;
    public float EffVolume = 0.5f;
    public float BGMVolume = 0.5f;

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.LoadScene("Title");
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "Title")
        {
            var canvas = GameObject.Find("Canvas").transform;

            EffSlider = canvas
                .Find("SettingUI/In/EFFECT/EffSlider")
                .GetComponent<Slider>();
            BGMSlider = canvas
                .Find("SettingUI/In/BGM/BGMSlider")
                .GetComponent<Slider>();

            // 1. 값 복원
            float eff = PlayerPrefs.GetFloat("EffVolume", 0.5f);
            EffSlider.value = eff;
            float bgm = PlayerPrefs.GetFloat("BGMVolume", 0.5f);
            BGMSlider.value = bgm;

            // 2. 이벤트 연결
            EffSlider.onValueChanged.AddListener(OnEffVolumeChanged);
            BGMSlider.onValueChanged.AddListener(OnBGMVolumeChanged);

            EffSlider.onValueChanged.AddListener(v => Debug.Log(v));
        }
    }

    // 슬라이더 값이 변경될 때마다 호출되는 메서드
    public void OnEffVolumeChanged(float value)
    {
        EffVolume = value;
        PlayerPrefs.SetFloat("EffVolume", EffVolume);
    }

    public void OnBGMVolumeChanged(float value)
    {
        BGMVolume = value;
        PlayerPrefs.SetFloat("BGMVolume", BGMVolume);
    }

    // 슬라이더 값 저장
    public void SaveVolumeSettings()
    {
        PlayerPrefs.Save();
    }
}
