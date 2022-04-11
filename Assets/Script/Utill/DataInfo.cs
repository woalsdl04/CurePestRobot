using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

[Serializable]
public class Info 
{
    public int PlayerHp = 0;
    public int PlayerDmg = 0;
    public int PlayerSpeed = 0;

    public int[] EnemyHp = new int[4];
    public int[] EnemyDmg = new int[4];

    public int[] BossHp = new int[2];
    public int[] BossDmg = new int[2];

    public float RedRespawnTIme = 0f;
    public float WhiteRespawnTIme = 0f;
}

public class DataInfo : MonoBehaviour
{
    public Info info;

    public void Save()
    {
        string path = Path.Combine(Application.persistentDataPath, "DataInfo.json");

        if (!File.Exists(path))
        {
            string json = JsonUtility.ToJson(info);
            File.WriteAllText(path, json);
        }
    }

    public void Load() 
    {
        Save();

        string path = Path.Combine(Application.persistentDataPath, "DataInfo.json");

        string json = File.ReadAllText(path);

        info = JsonUtility.FromJson<Info>(json);

        V.DataInfo = info;
    }

    void Awake()
    {
        Load();
    }

}
