using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReturnParticle : MonoBehaviour //파티클 반납
{
    float ReturnTime = 0f;
    // Start is called before the first frame update
    void OnEnable()
    {
        ReturnTime = 10f + V.WorldTIme; 
    }

    // Update is called once per frame
    void Update()
    {
        if(ReturnTime < V.WorldTIme)
        {
            V.pool.Return(gameObject);
            return;
        }
    }
}
