using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : SceneBase
{
    // Start is called before the first frame update
    public int StepCount = 0;

    public GameObject RankPopup = null;

    public RectTransform PlayerImage = null;

    public GameObject[] buttons = null;

    void Start()
    {
        StepCount = 9999;

        if(RankPopup == null) 
        {
            RankPopup = GameObject.Find("RankPopUp");
            RankPopup.SetActive(false);
        }

        V.ScoreValue = 0;
    }

    // Update is called once per frame
    void Update()
    {
        SceneUpdate();
    }

    public override void ScenePlaying()
    {
        base.ScenePlaying();

        Select();
    }

    public void Select() 
    {
        int count = StepCount % 3;

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            StepCount--;
            PlayerImage.transform.SetParent(buttons[StepCount % 3].transform);
            PlayerImage.anchoredPosition = new Vector3(-350, 0);
            V.Sound.SFX_Play(SFX_SOUND.HIT);
            Debug.Log(StepCount % 3);
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            StepCount++;
            PlayerImage.transform.SetParent(buttons[StepCount % 3].transform);
            PlayerImage.anchoredPosition = new Vector3(-350, 0);
            V.Sound.SFX_Play(SFX_SOUND.HIT);
            Debug.Log(StepCount % 3);
        }

        if (Input.GetKeyDown(KeyCode.Z)) 
        {
            switch (count) 
            {
                case 0:
                    
                    V.Fade.FadeIn(Color.black, 1f, 1f, 1);
                    Camera.main.GetComponent<Animation>().Play("TitleCameraAnim");
                    V.Sound.SFX_Play(SFX_SOUND.HIT);
                    sceneStep = SCENE_STEP.END_BEFORE;

                    break;
                case 1:
                    V.Sound.SFX_Play(SFX_SOUND.HIT);
                    if (RankPopup.activeSelf) RankPopup.SetActive(false);
                    else RankPopup.SetActive(true);

                    break;
                case 2:
                    V.Sound.SFX_Play(SFX_SOUND.HIT);
                    Application.Quit();

                    break;
            }
        }
    }

    public override void SceneEnd()
    {
        base.SceneEnd();

        if(V.NowScene.name == "Title") 
        {
            ChangeScene("Stage1");
        }
    }
}
