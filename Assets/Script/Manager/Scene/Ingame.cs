using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ROUND_STEP 
{
    PLAYING,
    BOSS_BEFORE,
    BOSS_PLAYING,
    BOSS_CLEAR,
}

public class Ingame : SceneBase
{
    public ROUND_STEP round_Step = ROUND_STEP.PLAYING;

    public int rountCount = 1;

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
        rountCount = 1;

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

        FunctionKey();

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
        if(V.Spawn.EnemyList.Count == 0) 
        {
            if (V.NowScene.name == "Stage1")
            {
                switch (rountCount) 
                {
                    case 1:
                        V.Spawn.SpawnEnemy(POOLTYPE.E_BACTERIA, 5);
                        break;
                    case 2:
                        V.Spawn.SpawnEnemy(POOLTYPE.E_BACTERIA, 2);
                        V.Spawn.SpawnEnemy(POOLTYPE.E_CANCER, 1);
                        V.Spawn.SpawnEnemy(POOLTYPE.E_BACTERIA, 2);
                        break;
                    case 3:
                        V.Spawn.SpawnEnemy(POOLTYPE.E_CANCER, 1);
                        V.Spawn.SpawnEnemy(POOLTYPE.E_BACTERIA, 1);
                        V.Spawn.SpawnEnemy(POOLTYPE.E_CANCER, 1);
                        V.Spawn.SpawnEnemy(POOLTYPE.E_BACTERIA, 1);
                        V.Spawn.SpawnEnemy(POOLTYPE.E_CANCER, 1);
                        break;
                    case 4:
                        V.Spawn.SpawnEnemy(POOLTYPE.E_CANCER, 2);
                        V.Spawn.SpawnEnemy(POOLTYPE.E_VIRUS, 1);
                        V.Spawn.SpawnEnemy(POOLTYPE.E_CANCER, 2);
                        break;
                    case 5:
                        V.Spawn.SpawnEnemy(POOLTYPE.E_CANCER, 5);
                        break;
                    case 6:
                        V.Fade.FadeIn(new Color(0.7f, 0, 0), 0.5f, 0.5f, 6);
                        V.UI.WarningText.gameObject.SetActive(true);
                        V.Cam.ShakeRepeat(2f);


                        round_Step = ROUND_STEP.BOSS_BEFORE;
                        return;
                }

                V.UI.SetInfo("ROUND " + rountCount);
            }
            else if (V.NowScene.name == "Stage2")
            {
                switch (rountCount)
                {
                    case 1:
                        V.Spawn.SpawnEnemy(POOLTYPE.E_BACTERIA, 5);
                        break;
                    case 2:
                        V.Spawn.SpawnEnemy(POOLTYPE.E_BACTERIA, 2);
                        V.Spawn.SpawnEnemy(POOLTYPE.E_GERM, 1);
                        V.Spawn.SpawnEnemy(POOLTYPE.E_BACTERIA, 2);
                        break;
                    case 3:
                        V.Spawn.SpawnEnemy(POOLTYPE.E_CANCER, 1);
                        V.Spawn.SpawnEnemy(POOLTYPE.E_BACTERIA, 1);
                        V.Spawn.SpawnEnemy(POOLTYPE.E_GERM, 1);
                        V.Spawn.SpawnEnemy(POOLTYPE.E_BACTERIA, 1);
                        V.Spawn.SpawnEnemy(POOLTYPE.E_CANCER, 1);
                        break;
                    case 4:
                        V.Spawn.SpawnEnemy(POOLTYPE.E_CANCER, 2);
                        V.Spawn.SpawnEnemy(POOLTYPE.E_GERM, 1);
                        V.Spawn.SpawnEnemy(POOLTYPE.E_CANCER, 2);
                        break;
                    case 5:
                        V.Spawn.SpawnEnemy(POOLTYPE.E_VIRUS, 2);
                        V.Spawn.SpawnEnemy(POOLTYPE.E_BACTERIA, 1);
                        V.Spawn.SpawnEnemy(POOLTYPE.E_VIRUS, 2);
                        break;
                    case 6:
                        V.Spawn.SpawnEnemy(POOLTYPE.E_CANCER, 5);
                        break;
                    case 7:
                        V.Spawn.SpawnEnemy(POOLTYPE.E_GERM, 1);
                        V.Spawn.SpawnEnemy(POOLTYPE.E_BACTERIA, 3);
                        V.Spawn.SpawnEnemy(POOLTYPE.E_GERM, 1);
                        break;
                    case 8:
                        V.Spawn.SpawnEnemy(POOLTYPE.E_VIRUS, 5);
                        break;
                    case 9:
                        V.Spawn.SpawnEnemy(POOLTYPE.E_GERM, 2);
                        V.Spawn.SpawnEnemy(POOLTYPE.E_CANCER, 1);
                        V.Spawn.SpawnEnemy(POOLTYPE.E_GERM, 2);
                        break;
                    case 10:
                        V.Spawn.SpawnEnemy(POOLTYPE.E_GERM, 5);
                        break;
                    case 11:
                        V.Fade.FadeIn(new Color(0.7f, 0, 0), 0.5f, 0.5f, 6);
                        V.UI.WarningText.gameObject.SetActive(true);
                        V.Cam.ShakeRepeat(2f);

                        round_Step = ROUND_STEP.BOSS_BEFORE;
                        return;
                }

                V.UI.SetInfo("ROUND " + rountCount);
            }

            V.UI.SetUi(UI_TYPE.ROUND, rountCount);

            rountCount++;
        }

        if(RedRespawnTIme < V.WorldTIme) 
        {
            float r = Random.Range(-13f, 13f);

            V.Spawn.SpawnEnemy(POOLTYPE.E_RED, new Vector3(r, 25, -3));

            RedRespawnTIme = V.WorldTIme +  V.DataInfo.RedRespawnTIme;
        }

        if(WhiteRespawnTIme < V.WorldTIme)
        {
            float r = Random.Range(-13f, 13f);

            V.Spawn.SpawnEnemy(POOLTYPE.E_WHITE, new Vector3(r, 25, -3));

            WhiteRespawnTIme = V.WorldTIme + V.DataInfo.WhiteRespawnTIme;
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

    public void FunctionKey()
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
                new JudgeMenetSign(V.Player, item.GetComponent<Caric>(), 100000);
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
            V.Spawn.SpawnEnemy(POOLTYPE.E_RED, new Vector3(Random.Range(-13f, 13f), 25, -3));
            
        }
        else if (Input.GetKeyDown(KeyCode.F10))
        {
            V.Spawn.SpawnEnemy(POOLTYPE.E_WHITE, new Vector3(Random.Range(-13f, 13f), 25, -3));
        }
    }


}
