using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_Virus : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        C_Init(V.DataInfo.EnemyHp[3], 0, V.DataInfo.EnemyDmg[3], 3f);
        DIsableCCAndMovePos(StartPos);
        EndPos = StartPos + (Vector3.down * 10f);

        Dir = Vector3.down;

        AttackCoolTime = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        base.EnemyUpdate();
    }

    public override void EnemyAttack()
    {
        if (MAxAttackTime < V.WorldTIme)
        {
            Vector3 dir = (V.Player.transform.position - transform.position);

            float rot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            for (int i = -1; i <= 1; i++)
            {
                var obj = V.pool.Get<BulletBase>(POOLTYPE.BULLET);
                obj.SetUp(BULLETTYPE.BASIC, transform.position, this, null, rot + (i * 10), 15);
                obj.TrailInit(obj.gameObject, TrailTIme, TrailColor);
            }

            Body.transform.rotation = Quaternion.LookRotation(dir.normalized);

            MAxAttackTime = V.WorldTIme + AttackCoolTime;
        }

        base.EnemyAttack();
    }

    public override void Die()
    {
        if (enemyStep == ENEMY_STEP.DIE_BEFORE || enemyStep == ENEMY_STEP.DIE) return;

        enemyStep = ENEMY_STEP.DIE_BEFORE;

        CC.enabled = false;

        V.ScoreValue += 2500;

        base.Die();
    }
}
