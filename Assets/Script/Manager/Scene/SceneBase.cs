using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum SCENE_STEP 
{ 
    FIRST_FRAME,
    AWAKE,
    START,
    PLAYING,
    END_BEFORE,
    END
}

public class SceneBase : MonoBehaviour
{
    public SCENE_STEP sceneStep = SCENE_STEP.FIRST_FRAME;
    // Start is called before the first frame update
    public void Awake()
    {
        V.Awake();
        V.NowScene = SceneManager.GetActiveScene();
    }
    // Update is called once per frame
    public void SceneUpdate()
    {
        V.Update();

        switch (sceneStep) 
        {
            case SCENE_STEP.FIRST_FRAME:

                V.Fade.FadeOut(Color.black, 1f, 1f, 1);

                sceneStep = SCENE_STEP.AWAKE;

                break;
            case SCENE_STEP.AWAKE:

                if(V.Fade.fadeStep == FADE_STEP.FADE_END_BEFORE)
                {
                    sceneStep = SCENE_STEP.START;
                }

                break;
            case SCENE_STEP.START:

                SceneStart();

                sceneStep = SCENE_STEP.PLAYING;

                break;
            case SCENE_STEP.PLAYING:

                ScenePlaying();

                break;
            case SCENE_STEP.END_BEFORE:

                if (V.Fade.fadeStep == FADE_STEP.FADE_END_BEFORE)
                {
                    sceneStep = SCENE_STEP.END;
                }

                break;
            case SCENE_STEP.END:

                SceneEnd();

                break;
        }
    }

    public virtual void SceneStart() { }
    public virtual void ScenePlaying() { }
    public virtual void SceneEnd() { }

    public void ChangeScene(string sceneName) 
    {
        SceneManager.LoadScene(sceneName);
    }
}
