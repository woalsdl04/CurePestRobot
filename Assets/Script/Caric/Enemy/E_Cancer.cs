using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Cancer : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        C_Init(V.DataInfo.EnemyHp[2], 0, V.DataInfo.EnemyDmg[2], 0.5f);
        DIsableCCAndMovePos(StartPos);
        EndPos = StartPos + (Vector3.down * 10f);

        Dir = Vector3.down;

        AttackCoolTime = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        base.EnemyUpdate();

        if (enemyStep != ENEMY_STEP.DIE)
        {
            Body.transform.Rotate(new Vector3(0.3f, 0.5f, 0), 0.3f);
        }
    }

    public override void EnemyAttack()
    {
        if (MAxAttackTime < V.WorldTIme)
        {
            for (int i = 0; i < 360; i += Random.Range(5, 15))
            {
                var obj = V.pool.Get<BulletBase>(POOLTYPE.BULLET);
                obj.SetUp(BULLETTYPE.BASIC, transform.position, this, null, i, 15);
                obj.TrailInit(obj.gameObject, TrailTIme, TrailColor);
            }

            MAxAttackTime = V.WorldTIme + AttackCoolTime;
        }

        base.EnemyAttack();
    }

    public override void Die()
    {
        if (enemyStep == ENEMY_STEP.DIE_BEFORE || enemyStep == ENEMY_STEP.DIE) return;

        enemyStep = ENEMY_STEP.DIE_BEFORE;

        CC.enabled = false;

        V.ScoreValue += 3000;

        base.Die();
    }
}
