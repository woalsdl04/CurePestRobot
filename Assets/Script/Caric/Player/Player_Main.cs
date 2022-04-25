using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PLAYER_CLASS 
{
    BASIC,
    SMART,
    LASER,
}

public partial class Player_Main : Caric
{
    public GameObject Muzzle = null;
    public GameObject Core = null;

    public float Exp = 0f;

    public BULLETTYPE Player_class = BULLETTYPE.BASIC;

    public GameObject Barrier = null;
    public float BarrierTime = 0;

    public bool IsMoojuck = false;

         
    // Start is called before the first frame update

    private void Awake()
    {
        V.Player = this;
    }
    void Start()
    {
        C_Init(V.DataInfo.PlayerHp, 100, V.DataInfo.PlayerDmg, V.DataInfo.PlayerSpeed);
        StartCoroutine(PlayerRotate());

        if (Muzzle == null) Muzzle = V.FInd_Child_Name("muzzle", gameObject);
        if (Core == null) Core = V.FInd_Child_Name("Core", gameObject);
        if (Barrier == null) Barrier = V.FInd_Child_Name("Barrier", gameObject);

        V.UI.SetUi(UI_TYPE.EXP, Exp);
        V.UI.SetUi(UI_TYPE.HP, Hp);
        V.UI.SetUi(UI_TYPE.MP, Mp);
    }

    // Update is called once per frame
    void Update()
    {
        if (!V.IsGameOver) 
        {
            PlayerMove();

            if (BarrierTime < V.WorldTIme && Barrier.activeSelf)
            {
                Barrier.SetActive(false);
                IsMoojuck = false;
            }

            if (Mp < 100)
            {
                Mp += (Time.deltaTime);
            }

        }
    }

    public void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.transform.gameObject.layer == LayerMask.NameToLayer("Enemy") && !Barrier.activeSelf && !IsMoojuck) 
        {
            new JudgeMenetSign(hit.transform.GetComponent<Caric>(), this, 1);
            IsMoojuck = true;


            return;
        }
    }

    public override void Kill()
    {
        base.Kill();

        Exp += 5f;

        if(Exp > 100) 
        {
            LevelUp();
        }

        V.UI.SetUi(UI_TYPE.EXP, Exp);
    }

    public override void Hit()
    {
        base.Hit();

        SetBarrier(1.5f);

        V.Cam.Shake();

        V.Fade.FadeIn(new Color(0.5f, 0, 0), 0.5f, 1.5f, 2);
    }

    public override void Die()
    {
        if (V.IsGameOver) return;

        base.Die();

        CC.enabled = false;

        V.Particle_Play(POOLTYPE.PARTICLE_DIE, transform.position);
        V.FInd_Child_Name("Core", Body).SetActive(false);
        V.Explosion(V.Find_Child_Component_List<Rigidbody>(Body), Body.transform.position, 1f, 16f);

        V.Fade.FadeIn(Color.black, 1f, 1f, 1);
        V.IsGameOver = true;

        V.Ingame.sceneStep = SCENE_STEP.END_BEFORE;
    }

    public void SetBarrier(float time)
    {
        Barrier.SetActive(true);
        BarrierTime = V.WorldTIme + time;
    }

}
