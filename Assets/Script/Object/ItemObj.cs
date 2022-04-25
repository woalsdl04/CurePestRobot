using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemObj : CBase
{
    // Start is called before the first frame update
    void Start()
    {
        MoveSpeed = 6f;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * MoveSpeed * Time.deltaTime);
        transform.Rotate(Vector3.up, 1);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) //아이템 획득
        {
            V.Item.GetItem();
            V.pool.Return(gameObject);
        }
    }
}
