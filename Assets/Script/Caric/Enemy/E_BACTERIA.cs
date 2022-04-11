using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_BACTERIA : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        C_Init(V.DataInfo.EnemyHp[0], 0, V.DataInfo.EnemyDmg[0], 1.5f);
        DIsableCCAndMovePos(StartPos);
        EndPos = StartPos + (Vector3.down * 10f);

        Dir = Vector3.down;
    }

    // Update is called once per frame
    void Update()
    {
        base.EnemyUpdate();

        if (enemyStep != ENEMY_STEP.DIE) 
        {
            Body.transform.Rotate(new Vector3(-1, -1, 0), 1);
        }
    }


    public override void Die()
    {
        if (enemyStep == ENEMY_STEP.DIE_BEFORE || enemyStep == ENEMY_STEP.DIE) return;

        enemyStep = ENEMY_STEP.DIE_BEFORE;

        V.ScoreValue += 1000;

        CC.enabled = false;

        base.Die();
    }
}
