using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class E_White : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        C_Init(1, 0, 0, Random.Range(3f, 6f));
        DIsableCCAndMovePos(StartPos);

        enemyStep = ENEMY_STEP.ATTACK;

        Dir = Vector3.down;
    }

    // Update is called once per frame
    void Update()
    {
        base.EnemyUpdate();
    }

    public override void IsOverLine()
    {
    }

    public override void EnemyAttack()
    {
        base.EnemyAttack();
    }

    public override void EnemyDIe()
    {
        var obj = V.pool.Get<Transform>(POOLTYPE.ITEM);
        obj.transform.position = transform.position;

        base.EnemyDIe();
    }

    public override void Attack()
    {
        base.Attack();
        Die();
    }

    public override void Die()
    {
        if (enemyStep == ENEMY_STEP.DIE_BEFORE || enemyStep == ENEMY_STEP.DIE) return;

        enemyStep = ENEMY_STEP.DIE_BEFORE;

        CC.enabled = false;

        base.Die();
    }
}
