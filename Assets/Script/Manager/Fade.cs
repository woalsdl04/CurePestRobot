using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum FADE_STEP 
{
    FADE_IN,
    FADE_OUT,
    FADE_END_BEFORE,
    FADE_END,
}

public class Fade : MonoBehaviour //페이드 클래스
{
    public FADE_STEP fadeStep = FADE_STEP.FADE_END;

    public Image fadeImage = null;

    public Color fadeColor;

    public float fadeSpeed = 0f;

    public float alphaValue = 0f;

    public int LoopCount = 0;

    public delegate void CALLBACK();

    public event CALLBACK callback = null;
    
    public void FadeIn(Color _color, float _alphaValue, float _speed, int _count) 
    {
        fadeImage.gameObject.SetActive(true);

        fadeColor = _color;
        fadeColor.a = 0f;
        alphaValue = _alphaValue;
        fadeSpeed = _speed;
        LoopCount = _count;

        fadeStep = FADE_STEP.FADE_IN;
    }

    public void FadeOut(Color _color, float _alphaValue, float _speed, int _count)
    {
        fadeImage.gameObject.SetActive(true);

        fadeColor = _color;
        fadeColor.a = _alphaValue;
        alphaValue = _alphaValue;
        fadeSpeed = _speed;
        LoopCount = _count;

        fadeStep = FADE_STEP.FADE_OUT;
    }

    public void StopFade() 
    {
        if (callback != null) callback();

        LoopCount = 0;
        fadeStep = FADE_STEP.FADE_END_BEFORE;
    }
    // Start is called before the first frame update
    private void Awake()
    {
        V.Fade = this;
    }
    void Start()
    {
        if (fadeImage == null) fadeImage = V.MainCanvas.transform.Find("Fade").GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        if (LoopCount > 0)
        {
            switch (fadeStep)
            {
                case FADE_STEP.FADE_IN:

                    fadeColor.a += Time.deltaTime * fadeSpeed;

                    if (fadeColor.a > alphaValue)
                    {
                        fadeColor.a = alphaValue;
                        LoopCount--;
                        fadeStep = FADE_STEP.FADE_OUT;
                    }

                    break;
                case FADE_STEP.FADE_OUT:

                    fadeColor.a -= Time.deltaTime * fadeSpeed;

                    if (fadeColor.a < 0)
                    {
                        fadeColor.a = 0;
                        LoopCount--;
                        fadeStep = FADE_STEP.FADE_IN;
                    }

                    break;
                case FADE_STEP.FADE_END_BEFORE:

                    fadeStep = FADE_STEP.FADE_END;

                    break;
                case FADE_STEP.FADE_END:
                    break;
            }
        }
        else StopFade();

        fadeImage.color = fadeColor;
    }
}
