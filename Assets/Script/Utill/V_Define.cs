using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public partial class V : MonoBehaviour
{
    public static bool IsGameOver = false;

    public static int Player_Level = 1;

    public static int Player_WeaponeLevel = 1;

    public static int PLAYER_MINMAX_POS = 13;

    public static int ScoreValue = 0;

    public static Scene NowScene;

    public static Info DataInfo = null;

    public static List<Rank> Rank = new List<Rank>();
}
