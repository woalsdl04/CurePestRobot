using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum UI_TYPE //ui 종류
{
    HP,
    MP,
    EXP,
    SOCRE,
    PAIN,
    ROUND,
    LEVEL,
}

public class UI : MonoBehaviour // ui 매니저
{
    public Slider HpSlider;
    public Slider MpSlider;
    public Slider ExpSlider;

    public Text HpText;
    public Text MpText;
    public Text ExpText;
    public Text RoundText;
    public Text ScoreText;
    public Text PainText;
    public Text LevelText;
    public Text InfoText;
    public Text WarningText;
    public GameObject StageClearPopup;

    public float score;

    public Text ClassText;
    public Image ClassImg;
    public Sprite[] ClassSprite;

    // Start is called before the first frame update
    private void Awake()
    {
        V.UI = this;
    }
    // Update is called once per frame
    void Update()
    {
        SetUi(UI_TYPE.SOCRE, V.ScoreValue);
    }

    public void SetUi(UI_TYPE type, float value) //ui 설정
    {
        switch (type) 
        {
            case UI_TYPE.HP:

                HpText.text = "HP : " + value.ToString();
                HpSlider.value = value;

                break;
            case UI_TYPE.MP:

                MpText.text = "MP : " + ((int)value).ToString();
                MpSlider.value = value;

                break;
            case UI_TYPE.EXP:

                ExpText.text = "EXP : " + value.ToString();
                ExpSlider.value = value;

                break;
            case UI_TYPE.SOCRE:

                ScoreText.text = "SCORE : " + string.Format("{0:#,###}", value.ToString());

                break;
            case UI_TYPE.PAIN:

                PainText.text = "PAIN : " + value + "%";

                break;
            case UI_TYPE.ROUND:

                RoundText.text = "ROUND : " + value;

                break;
            case UI_TYPE.LEVEL:

                LevelText.text = "LV : " + value;

                break;
        }
    }

    public void ChangeClassImage(int index)  
    {
        ClassImg.sprite = ClassSprite[index % 3];
        ClassText.text = ClassSprite[index % 3].name;
    }
 
    public void SetInfo(string info) //정보 ui 애니메이션 실행
    {
        InfoText.GetComponent<Animation>().Stop();

        InfoText.text = info.ToUpper();
        InfoText.GetComponent<Animation>().Play("Info");
    }

}
