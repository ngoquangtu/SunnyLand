using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Opossum : Enemy
{
private Collider2D coll;
    protected override void Start()
    {
        base.Start();
        coll=GetComponent<Collider2D>();
    }
    private void moving(){
        
    }
}
