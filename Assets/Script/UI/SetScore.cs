using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SetScore : MonoBehaviour
{
    // Start is called before the first frame update
    public Text SocreText;
    void Start()
    {
        SocreText = GetComponent<Text>();

        if(SocreText != null)
        {
            SocreText.text = "SCORE : " + V.ScoreValue;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
