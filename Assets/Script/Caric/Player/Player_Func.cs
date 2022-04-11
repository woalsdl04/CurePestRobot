using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class Player_Main : Caric
{
    
    public void LevelUp()
    {
        if (V.Player_Level > 4) return;

        V.Player_Level++;

        GetComponent<Player_Fire>().AttackCoolTime -= 0.02f;
        Hp += 20;
        Mp = 100;
        Exp = 0;
        Dmg += 3;
        MoveSpeed += 1.5f;

        V.UI.SetUi(UI_TYPE.LEVEL, V.Player_Level);
        V.UI.SetUi(UI_TYPE.EXP, Exp);

        V.UI.SetInfo("LEVEL UP!");
    }

    public Vector3 ClampPlayerPos() 
    {
        return new Vector3
            (
                Mathf.Clamp(transform.position.x, -V.PLAYER_MINMAX_POS, V.PLAYER_MINMAX_POS),
                Mathf.Clamp(transform.position.y, -V.PLAYER_MINMAX_POS, V.PLAYER_MINMAX_POS),
                transform.position.z
            );
    }

    public void PlayerMove() 
    {
        DIsableCCAndMovePos(ClampPlayerPos());

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        Vector3 dir = new Vector3(h, v, 0);

        CC.Move(MoveSpeed * dir * Time.deltaTime);
    }

    public IEnumerator PlayerRotate() 
    {
        while (true) 
        {
            float x = -Input.GetAxis("Horizontal");

            Body.transform.rotation = Quaternion.Lerp(Body.transform.rotation, Quaternion.Euler(Vector3.up * x * 20), 0.1f);

            yield return new WaitForSeconds(0.01f);
        }
    }
}
