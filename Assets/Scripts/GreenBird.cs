using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBird : Bird
{
    //小鸟回旋
    public override void ShowSkill(){
        base.ShowSkill();
        Vector3 speed=rg.velocity;
        speed.x*=-1;
        rg.velocity=speed;

    }

}
