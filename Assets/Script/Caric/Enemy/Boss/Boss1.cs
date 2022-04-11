using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : BossBase
{
    IEnumerator AttackCorotine = null;

    public Vector3 BackUpPos;

    // Start is called before the first frame update
    void Start()
    {
        V.Boss = this;

        C_Init(V.DataInfo.BossHp[0], 0, V.DataInfo.BossDmg[0], 6f);

        gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        base.EnemyUpdate();
        
        if (enemyStep != ENEMY_STEP.DIE)
        {
            Body.transform.Rotate(new Vector3(0.5f, -0.5f, 0), 1);
        }
    }

    public override void EnemyS2A()
    {
        base.EnemyS2A();
        V.Cam.ChangeTarget(CAMERA_TARGET.BOSS_AND_PALYER);
        V.Slider_Create(V.Boss.gameObject);

        StartCoroutine(BossAttack());
    }

    public override void EnemyDIe()
    {
        StopAllCoroutines();

        V.Spawn.EnemyList.Remove(gameObject);
        foreach (var item in V.Spawn.EnemyList)
        {
            new JudgeMenetSign(V.Player, item.GetComponent<Caric>(), 100000);
        }

        StartCoroutine(BossDie());
    }

    public override void Die()
    {
        if (enemyStep == ENEMY_STEP.DIE_BEFORE || enemyStep == ENEMY_STEP.DIE) return;
       
        base.Die();

        enemyStep = ENEMY_STEP.DIE_BEFORE;

        CC.enabled = false;
    }

    public IEnumerator BossAttack() 
    {
        while (true) 
        {
            float WaitTime = Random.Range(0.5f, 1f);

            yield return new WaitForSeconds(WaitTime);


            BackUpPos = transform.position;

            int r = Random.Range(0, 3);

            switch (r) 
            {
                case 0:
                    AttackCorotine = TwoAngleChangeAttack();
                    break;
                case 1:
                    AttackCorotine = EnemySpawnPatten();
                    break;
                case 2:
                    AttackCorotine = MoveAndCircleAttack();
                    break;
            }

            yield return StartCoroutine(AttackCorotine);

            yield return StartCoroutine(BackPos());
        }
    }

    public IEnumerator TwoAngleChangeAttack() 
    {
        int Count = 0;

        for (int i = 0; i < 10; i++) 
        {
            for(int j = Count; j < 360; j += 10) 
            {
                var obj = V.pool.Get<BulletBase>(POOLTYPE.BULLET);
                obj.SetUp(BULLETTYPE.BASIC, transform , this, null, j, 10);
                obj.TrailInit(obj.gameObject, TrailTIme, TrailColor);
            }

            yield return new WaitForSeconds(0.5f);

            Count += 5;
        }
    }

    public IEnumerator MoveAndCircleAttack()
    {
        for(int i = 0; i < 5; i++) 
        {
            float x = Random.Range(-V.PLAYER_MINMAX_POS, V.PLAYER_MINMAX_POS);
            float y = Random.Range(-V.PLAYER_MINMAX_POS, V.PLAYER_MINMAX_POS);

            //float MoveSpeed = 0f;

            Vector3 TargetPos = new Vector3(x, y, transform.position.z);

            while (Vector3.Distance(TargetPos, transform.position) > 0.01f) 
            {
                MoveSpeed += Time.deltaTime * MoveSpeed;

                transform.position = Vector3.MoveTowards(transform.position, TargetPos, Time.deltaTime * 10);

                yield return new WaitForSeconds(0.01f);
            }

            Vector3 dir = (V.Player.transform.position - transform.position);

            float rot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            for (int j = -2; j <= 2; j++)
            {
                var obj = V.pool.Get<BulletBase>(POOLTYPE.BULLET);
                obj.SetUp(BULLETTYPE.BASIC, transform, this, null, rot + (j * 10), 15);
                obj.TrailInit(obj.gameObject, 0.3f, TrailColor);
            }

            yield return new WaitForSeconds(0.3f);

            for (int j = 0; j < 360; j += 10)
            {
                var obj = V.pool.Get<BulletBase>(POOLTYPE.BULLET);
                obj.SetUp(BULLETTYPE.BASIC, transform, this, null, j, 10);
                obj.TrailInit(obj.gameObject, TrailTIme, TrailColor);
            }


            yield return new WaitForSeconds(1f);
        }
    }

    public IEnumerator EnemySpawnPatten()
    {
        for(int i = 0; i < 10; i++) 
        {
            V.Spawn.SpawnEnemy(POOLTYPE.E_BACTERIA, new Vector3(Random.Range(-13f, 13f), 25, -3));

            yield return new WaitForSeconds(1f);
        }
    }

    public IEnumerator BackPos()
    {
        while(Vector3.Distance(BackUpPos, transform.position) > 0.01f) 
        {
            transform.position = Vector3.MoveTowards(transform.position, BackUpPos, Time.deltaTime * 10);

            yield return new WaitForSeconds(0.01f);
        }
    }

    public IEnumerator BossDie() 
    {
        int bombCount = 30;
        
        V.Cam.ChangeTarget(CAMERA_TARGET.BOSS);

        while (bombCount > 0) 
        {
            V.Particle_Play(POOLTYPE.PARTICLE_DIE, transform.position + new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(-2f, 2f)));
            V.Cam.Shake();
            V.Sound.SFX_Play(SFX_SOUND.BOMB);

            bombCount--;
            yield return new WaitForSeconds(0.25f);
        }

        V.Sound.SFX_Play(SFX_SOUND.EXPLOSION);
        V.FInd_Child_Name("body", Body).SetActive(false);
        V.Explosion(V.FInd_Child_Component_List<Rigidbody>(Body), Body.transform.position, 1f, 30f);
        Destroy(V.BossSlider);

        yield return new WaitForSeconds(3f);

        V.Cam.ChangeTarget(CAMERA_TARGET.PLAYER);

        V.ScoreValue += 10000 + (V.Player.Hp * 100) - (int)(V.Ingame.PainValue * 100);


        V.Ingame.round_Step = ROUND_STEP.BOSS_CLEAR;
        V.UI.StageClearPopup.gameObject.SetActive(true);
        
        V.Ingame.NextStageTime = 3f + V.WorldTIme;

        Destroy(gameObject, 3f);
    }

}
