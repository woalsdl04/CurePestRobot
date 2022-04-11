using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSlider : MonoBehaviour
{
    GameObject Target;
    RectTransform rect;
    Vector3 distance = Vector3.up * 150;

    Slider slider;
    // Start is called before the first frame update
    public void SetUp(GameObject target) 
    {
        Target = target;
        rect = GetComponent<RectTransform>();
        slider = GetComponent<Slider>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Target == false) 
        {
            Destroy(gameObject);
        }

        slider.value = ((float)V.Boss.Hp / (float)V.Boss.MaxHp);

        var pos = Camera.main.WorldToScreenPoint(Target.transform.position);

        rect.position = pos + distance;
    }
}
