using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class V : MonoBehaviour //공용 스태틱 클래스
{
    public static GameObject MainCanvas = null;

    public static float WorldTIme = 0f;

    public static Player_Main Player;

    public static Caric Boss;

    public static GameObject BossSlider;

    public static Fade Fade = null;
    public static Cam Cam = null;
    public static Item Item = null;
    public static JudgeMent Judge = null;
    public static Spawn Spawn = null;
    public static UI UI = null;
    public static Ingame Ingame = null;
    public static Sound Sound = null;

    public static ObjectPool pool = null;
    public static void Awake()
    {
        MainCanvas = GameObject.Find("MainCanvas"); 
    }

    // Update is called once per frame
    public static void Update()
    {
        WorldTIme = Time.time; // 월드 타임 기록

    }
}
