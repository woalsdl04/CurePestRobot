using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgMentSign //판정 사인
{
    public Caric Attacker = null; //공격자
    public Caric Defender = null; //방어자
    public int Mutiple; //데미지 배수
    public JudgMentSign(Caric attacker, Caric defender, int mutiple = 1) 
    {
        Attacker = attacker;
        Defender = defender;
        Mutiple = mutiple;
        V.Judg.JudgMentSignQueue.Enqueue(this);
    }
}

public class JudgMent : MonoBehaviour //판정 매니저
{
    public Queue<JudgMentSign> JudgMentSignQueue = new Queue<JudgMentSign>(); //판정 큐
    // Start is called before the first frame update
    private void Awake()
    {
        V.Judg = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(JudgMentSignQueue.Count > 0) //판정
        {
            JudgMentSign sign = JudgMentSignQueue.Dequeue();

            sign.Defender.Hp -= sign.Attacker.Dmg * sign.Mutiple;

            if(sign.Defender.Hp > 0) 
            {
                sign.Defender.Hit();
                sign.Attacker.Attack();
            }
            else
            {
                sign.Defender.Die();
                sign.Attacker.Kill();
            }
        }

    }
}
