using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgeMenetSign
{
    public Caric Attacker = null;
    public Caric Defender = null;
    public int Mutiple;
    public JudgeMenetSign(Caric attacker, Caric defender, int mutiple = 1) 
    {
        Attacker = attacker;
        Defender = defender;
        Mutiple = mutiple;
        V.Judge.JudgeMenetSignQueue.Enqueue(this);
    }
}
public class JudgeMent : MonoBehaviour
{
    public Queue<JudgeMenetSign> JudgeMenetSignQueue = new Queue<JudgeMenetSign>();
    // Start is called before the first frame update
    private void Awake()
    {
        V.Judge = this;
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(JudgeMenetSignQueue.Count > 0) 
        {
            JudgeMenetSign sign = JudgeMenetSignQueue.Dequeue();

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
