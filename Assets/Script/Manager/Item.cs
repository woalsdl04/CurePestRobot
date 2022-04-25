using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ITEM 
{
    WEAPONEUPGRADE,
    BARRIER,
    HPUP,
    MPUP,
    PAINDOWN,
    SPEEDUP,

    END,
}

public class Item : MonoBehaviour
{

    public void GetItem() //아이템 적용
    {
        ITEM r = (ITEM)Random.Range(0, (int)ITEM.END);

        string info = "";

        switch (r) 
        {
            case ITEM.WEAPONEUPGRADE:

                if(V.Player_WeaponeLevel < 5) 
                {
                    V.Player_WeaponeLevel++;
                    info = "WEAPONE UPGRADE!";
                }

                break;
            case ITEM.BARRIER:

                V.Player.SetBarrier(3f);
                info = "BARRIER!";

                break;
            case ITEM.HPUP:

                V.Player.Hp += 20;
                info = "HP 20UP!";

                break;
            case ITEM.MPUP:

                V.Player.Mp += 20;
                info = "MP 20UP!";

                break;
            case ITEM.PAINDOWN:

                V.Ingame.PainValue -= 20;
                info = "PAIN DOWN!";

                break;
            case ITEM.SPEEDUP:

                StartCoroutine(SpeedUpItem(3f));
                info = "SPEED UP!";

                break;
        }

        V.UI.SetInfo(info);
    }

    public IEnumerator SpeedUpItem(float time) 
    {
        V.Player.MoveSpeed += 2f;

        yield return new WaitForSeconds(time);

        V.Player.MoveSpeed -= 2f;
    }

    // Start is called before the first frame update
    private void Awake()
    {
        V.Item = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
