using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss2 : BossBase //보스2 클래스
{
    // Start is called before the first frame update
    IEnumerator AttackCorotine = null;

    public Vector3 BackUpPos;

    public GameObject Laser_Pt;
    public GameObject Laser;
    public GameObject Muzzle;
    // Start is called before the first frame update
    void Start()
    {
        V.Boss = this;

        C_Init(V.DataInfo.BossHp[1], 0, V.DataInfo.BossDmg[1], 6f);

        gameObject.SetActive(false);
        Laser_Pt.SetActive(false);
        Laser.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        base.EnemyUpdate();
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
            new JudgMentSign(V.Player, item.GetComponent<Caric>(), 100000);
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

    public IEnumerator BossAttack() //보스 공격
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
                    AttackCorotine = LaserAttack(); //patten 1
                    break;
                case 1:
                    AttackCorotine = EnemySpawnPatten(); //patten 2
                    break;
                case 2:
                    AttackCorotine = MoveAndCircleAttack(); //patten 3
                    break;
            }

            yield return StartCoroutine(AttackCorotine);

            yield return StartCoroutine(BackPos());
        }
    }

    public IEnumerator LaserAttack() //레이저 공격 패턴
    {
        int stepCount = 0;

        float AttackCoolTime = 0;
        float Timer = 0f;

        while (stepCount != 3) 
        {
            Vector3 dir = (V.Player.transform.position - transform.position);

            float rot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,0,rot + 90), Time.deltaTime * 3f);

            switch (stepCount)
                {
                    case 0:

                    Laser_Pt.gameObject.SetActive(true);
                    Timer = V.WorldTIme + 2f;
                    stepCount = 1;

                        break;
                    case 1:

                    if (Timer < V.WorldTIme)
                    {
                        Laser.SetActive(true);
                        V.Sound.SFX_Play(SFX_SOUND.LASER);
                        Laser_Pt.gameObject.SetActive(false);
                        Timer = V.WorldTIme + 5f;
                        stepCount = 2;
                    }

                        break;

                    case 2:
                    
                    if(Timer < V.WorldTIme) 
                    {
                        Laser.SetActive(false);
                        stepCount = 3;
                    }

                    if(AttackCoolTime < V.WorldTIme) 
                    {
                        if (Physics.Raycast(Muzzle.transform.position, Vector3.down, out RaycastHit hit))
                        {
                            if (hit.transform != null && hit.transform.gameObject.layer == LayerMask.NameToLayer("Player"))
                            {
                                new JudgMentSign(this, hit.transform.GetComponent<Caric>());
                            }
                        }

                        AttackCoolTime = V.WorldTIme + 0.25f;
                    }
            

                        break;
                }


            yield return new WaitForSeconds(0.01f);
        }
    }

    public IEnumerator MoveAndCircleAttack() //움직인 후 원형탄 공격 패턴
    {
        for (int i = 0; i < 5; i++)
        {
            float x = Random.Range(-V.PLAYER_MINMAX_POS, V.PLAYER_MINMAX_POS);
            float y = Random.Range(-V.PLAYER_MINMAX_POS, V.PLAYER_MINMAX_POS);

            Vector3 TargetPos = new Vector3(x, y, transform.position.z);

            float playerRot = Mathf.Atan2(TargetPos.y, TargetPos.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, 90 + playerRot);

            while (Vector3.Distance(TargetPos, transform.position) > 0.01f)
            {
                MoveSpeed += Time.deltaTime * MoveSpeed;

                transform.position = Vector3.MoveTowards(transform.position, TargetPos, Time.deltaTime * 5);

                yield return new WaitForSeconds(0.01f);
            }

            Vector3 dir = (V.Player.transform.position - transform.position);

            float rot = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;

            for (int j = -2; j <= 2; j++)
            {
                var obj = V.pool.Get<BulletBase>(POOLTYPE.BULLET);
                obj.SetUp(BULLETTYPE.BASIC, transform.position, this, null, rot + (j * 5), 15);
                obj.TrailInit(obj.gameObject, 0.15f, TrailColor);
            }

            yield return new WaitForSeconds(0.3f);

            for (int j = 0; j < 360; j += 10)
            {
                var obj = V.pool.Get<BulletBase>(POOLTYPE.BULLET);
                obj.SetUp(BULLETTYPE.BASIC, transform.position, this, null, j, 10);
                obj.TrailInit(obj.gameObject, TrailTIme, TrailColor);
            }


            yield return new WaitForSeconds(1f);
        }
    }

    public IEnumerator EnemySpawnPatten() //적 생성 패턴
    {
        for (int i = 0; i < 5; i++)
        {
            V.Spawn.SpawnEnemy(POOLTYPE.E_GERM, new Vector3(Random.Range(-12f, 12f), 25, -3));

            yield return new WaitForSeconds(2f);
        }
    }

    public IEnumerator BackPos() //위치 되돌아가기
    {
        while (Vector3.Distance(BackUpPos, transform.position) > 0.01f)
        {
            transform.position = Vector3.MoveTowards(transform.position, BackUpPos, Time.deltaTime * 10);

            yield return new WaitForSeconds(0.01f);
        }
    }

    public IEnumerator BossDie() //보스 사망
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
        V.Explosion(V.Find_Child_Component_List<Rigidbody>(Body), Body.transform.position, 1f, 30f);
        Destroy(V.BossSlider);

        yield return new WaitForSeconds(3f);

        V.Cam.ChangeTarget(CAMERA_TARGET.PLAYER);
        
        V.ScoreValue += 50000 + (V.Player.Hp * 100) - ((int)V.Ingame.PainValue * 100);

        V.Ingame.round_Step = ROUND_STEP.BOSS_CLEAR;
        V.UI.StageClearPopup.gameObject.SetActive(true);

        V.Ingame.NextStageTime = 3f + V.WorldTIme;

        Destroy(gameObject, 3f);
    }
}
