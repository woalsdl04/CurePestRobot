using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class GameEnd : SceneBase
{
    public Text Name;

    public int Count = 0;

    public Ranking Rank;
    // Start is called before the first frame update
    void Start()
    {
        Name.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        SceneUpdate();
    }

    public override void ScenePlaying()
    {
        base.ScenePlaying();

        if(Count < 3) 
        {
            if (Input.anyKeyDown)
            {
                string name = Input.inputString;

                int Ascii = name[0];

                if (Ascii == null) return;
                

                if(Ascii > 64 && Ascii < 123) 
                {
                    V.Sound.SFX_Play(SFX_SOUND.HIT);
                    Name.text += name.ToUpper();
                    Count++;
                    Debug.Log("Count : " + Count);
                }
            }
        }
        else if(Count == 3) 
        {
            if (Input.GetKeyDown(KeyCode.Return)) 
            {
                V.Sound.SFX_Play(SFX_SOUND.HIT);
                Rank.AddRank(new Rank(Name.text, V.ScoreValue));
                V.Fade.FadeIn(Color.black, 1f, 1f, 1);
                sceneStep = SCENE_STEP.END_BEFORE;
            }
        }
        
    }

    public override void SceneEnd()
    {
        base.SceneEnd();

        ChangeScene("Title");
    }
}
