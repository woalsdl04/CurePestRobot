using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caric : CBase
{
    public int Hp 
    {
        get => hp;
        set
        {
            hp = value;

            if (hp > MaxHp) hp = 100;
            else if (hp < 0) hp = 0;

            V.UI.SetUi(UI_TYPE.HP, V.Player.Hp);
        }
    }

    public float Mp
    {
        get => mp;
        set
        {
            mp = value;

            if (mp > 100) mp = 100;
            else if (mp < 0) mp = 0;

            V.UI.SetUi(UI_TYPE.MP, V.Player.Mp);
        }

    }

    [SerializeField]
    private int hp = 0;
    private float mp = 0;

    public int MaxHp = 0;
    public int Dmg = 0;

    public CharacterController CC;
    public GameObject Body;
      
    public void C_Init(int _hp, int _mp, int _dmg, float _speed) 
    {
        MaxHp = _hp;
        hp = MaxHp;
        mp = _mp;
        Dmg = _dmg;
        MoveSpeed = _speed;

        if (CC == null) CC = GetComponent<CharacterController>();
        if (Body == null) Body = V.FInd_Child_Name("Body", gameObject);
    }

    public void DIsableCCAndMovePos(Vector3 pos) 
    {
        CC.enabled = false;
        transform.position = pos;
        CC.enabled = true;
    }

    public virtual void Hit() 
    {
        V.Sound.SFX_Play(SFX_SOUND.HIT);
    }
    public virtual void Die()
    {
        V.Sound.SFX_Play(SFX_SOUND.BOMB);
    }
    public virtual void Kill()
    {

    }
    public virtual void Attack()
    {

    }

}
