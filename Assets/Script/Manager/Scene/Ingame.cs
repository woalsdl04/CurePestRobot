using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public enum ROUND_STEP //라운드 스텝 
{
    PLAYING,
    BOSS_BEFORE,
    BOSS_PLAYING,
    BOSS_CLEAR,
}

[Serializable]
public class EnemyInfo //적 정보
{
    public POOLTYPE[] EnemyType;
}

public class Ingame : SceneBase //인게임
{
    public EnemyInfo[] enemyInfos;

    public ROUND_STEP round_Step = ROUND_STEP.PLAYING;
    public int roundCount = 1;

    public float RedRespawnTIme = 0f;
    public float WhiteRespawnTIme = 0f;
    public float PainValue
    {
        get => pain;

        set 
        {
            pain = value;

            if (pain > 100)
            {
                pain = 100;
            }
            else if (pain < 0) pain = 0;

            V.UI.SetUi(UI_TYPE.PAIN, pain);
        }
    }

    private float pain = 0f;
    
    public float NextStageTime = 0f;
    // Start is called before the first frame update
    void Start()
    {
        V.Ingame = this;
        V.IsGameOver = false;
        V.Player_WeaponeLevel = 1;
        V.Player_Level = 1;
        PainValue = 0;
        roundCount = 0;

        if(V.NowScene.name == "Stage1") 
        {
            PainValue = 10;
        }
        else if(V.NowScene.name == "Stage2") 
        {
            PainValue = 30;
        }
    }

    // Update is called once per frame
    void Update()
    {
        base.SceneUpdate();  
    }

    public override void ScenePlaying()
    {
        base.ScenePlaying();

        CheatKey();

        if (PainValue >= 100)
        {
            V.Fade.FadeIn(new Color(0.7f, 0, 0.7f), 1f, 1f, 1);
            V.IsGameOver = true;
            sceneStep = SCENE_STEP.END_BEFORE;

            return;
        }

        switch (round_Step)
        {
            case ROUND_STEP.PLAYING:

                IngamePlaying();

                break;
            case ROUND_STEP.BOSS_BEFORE:

                if (V.Fade.fadeStep == FADE_STEP.FADE_END_BEFORE) 
                {
                    V.Cam.CancelInvoke();
                    V.UI.WarningText.gameObject.SetActive(false);
                    V.Spawn.SpawnBoss(new Vector3(0, 25, V.Boss.transform.position.z));

                    round_Step = ROUND_STEP.BOSS_PLAYING;
                }

                break;
            case ROUND_STEP.BOSS_PLAYING:
                break;
            case ROUND_STEP.BOSS_CLEAR:

                if(NextStageTime < V.WorldTIme) 
                {
                    V.Fade.FadeIn(Color.black, 1f, 1f, 1);
                    sceneStep = SCENE_STEP.END_BEFORE;
                }

                break;
        }
    }
    public void IngamePlaying() 
    {
        SpawnEnemys();

        SpawnNpcRed();
        
        SpawnNpcWhite();
    }

    public void SpawnEnemys() // 적 소환 명령
    { 
        if(V.Spawn.EnemyList.Count == 0) 
        {
            if(roundCount < enemyInfos.Length)
            {
                for(int i  = 0; i < enemyInfos[roundCount].EnemyType.Length; i++)
                {
                    V.Spawn.SpawnEnemy(enemyInfos[roundCount].EnemyType[i], i); 
                }
                
                V.UI.SetInfo("ROUND " + (roundCount + 1));
                V.UI.SetUi(UI_TYPE.ROUND, roundCount);
                
                roundCount++;
            }
            else if(roundCount == enemyInfos.Length)
            {
                V.Fade.FadeIn(new Color(0.7f, 0, 0), 0.5f, 0.5f, 6);
                V.UI.WarningText.gameObject.SetActive(true);
                V.Cam.ShakeRepeat(2f);

                round_Step = ROUND_STEP.BOSS_BEFORE;
                return;
            }
        }
    }

    public void SpawnNpcRed()
    {
        if(RedRespawnTIme < V.WorldTIme) 
        {
            float r = UnityEngine.Random.Range(-13f, 13f);

            V.Spawn.SpawnEnemy(POOLTYPE.E_RED, new Vector3(r, 25, -3));

            RedRespawnTIme = V.WorldTIme +  V.DataInfo.RedRespawnTIme;
        }
    }

    public void SpawnNpcWhite()
    {
        if(WhiteRespawnTIme < V.WorldTIme)
        {
            float r = UnityEngine.Random.Range(-13f, 13f);

            V.Spawn.SpawnEnemy(POOLTYPE.E_WHITE, new Vector3(r, 25, -3));

            WhiteRespawnTIme = V.WorldTIme + V.DataInfo.WhiteRespawnTIme;
        }
    }

    public void CheatKey()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            ChangeScene("Stage1");
        }
        else if (Input.GetKeyDown(KeyCode.F2))
        {
            ChangeScene("Stage2");
        }
        else if (Input.GetKeyDown(KeyCode.F3))
        {
            if (V.Player_WeaponeLevel < 5)
            {
                V.Player_WeaponeLevel++;
                V.UI.SetInfo("WEAPONE UPGRADE!");
            }
        }
        else if (Input.GetKeyDown(KeyCode.F4))
        {
            V.Player.LevelUp();
        }
        else if (Input.GetKeyDown(KeyCode.F5))
        {
            V.Player.IsMoojuck = (V.Player.IsMoojuck) ? false : true;           
        }
        else if (Input.GetKeyDown(KeyCode.F6))
        {
            foreach (var item in V.Spawn.EnemyList)
            {
                new JudgMentSign(V.Player, item.GetComponent<Caric>(), 100000);
            }        
        }
        else if (Input.GetKeyDown(KeyCode.F7))
        {
            int hp = V.Player.Hp;
            hp += 10;

            if (hp > 100) hp = 10;

            V.Player.Hp = hp;            
        }
        else if (Input.GetKeyDown(KeyCode.F8))
        {
            float pain = PainValue;
            pain += 10f;

            if (pain >= 100f) pain = 10f;

            PainValue = pain;       
        }
        else if (Input.GetKeyDown(KeyCode.F9))
        {
            V.Spawn.SpawnEnemy(POOLTYPE.E_RED, new Vector3(UnityEngine.Random.Range(-13f, 13f), 25, -3));
            
        }
        else if (Input.GetKeyDown(KeyCode.F10))
        {
            V.Spawn.SpawnEnemy(POOLTYPE.E_WHITE, new Vector3(UnityEngine.Random.Range(-13f, 13f), 25, -3));
        }
    }

    public override void SceneEnd()
    {
        base.SceneEnd();

        if ((V.NowScene.name == "Stage1" || V.NowScene.name == "Stage2") && V.IsGameOver) 
        {
            ChangeScene("GameOver");
        }
        else if (V.NowScene.name == "Stage1" && !V.IsGameOver) 
        {
            ChangeScene("Stage2");
        }
        else if (V.NowScene.name == "Stage2" && !V.IsGameOver) 
        {
            ChangeScene("GameClear");
        }
    }

}
