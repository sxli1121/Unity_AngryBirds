using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlackBird : Bird
{
    public List<Pig> blocks=new List<Pig>();

    /// <summary>
    /// Sent when another object enters a trigger collider attached to this
    /// object (2D physics only).
    /// </summary>
    /// <param name="collision">The other Collider2D involved in this collision.</param>
   private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Enemy"){
            blocks.Add(collision.gameObject.GetComponent<Pig>());

        }
    }

    /// <summary>
    /// Sent when another object leaves a trigger collider attached to
    /// this object (2D physics only).
    /// </summary>
    /// <param name="collider">The other Collider2D involved in this collision.</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.tag=="Enemy"){
            blocks.Remove(collision.gameObject.GetComponent<Pig>());
        }
        
    }

    public override void ShowSkill(){//鼠标左键点击触发，清除碰撞检测范围内的物体

        base.ShowSkill();
        if(blocks.Count>0&&blocks!=null){
            for(int i=0;i<blocks.Count;i++){
                blocks[i].Dead();
            }
        }
        Onclear();
    }

    void Onclear(){//处理爆炸小鸟爆炸后的效果
        rg.velocity=Vector3.zero;
        Instantiate(boom,transform.position,Quaternion.identity);
        render.enabled=false;
        GetComponent<CircleCollider2D>().enabled=false;
        myTrail.ClearTrails();

    }

    protected override void Next(){//重写next方法，不需爆炸效果 Instantiate(boom,transform.position,Quaternion.identity);

        GameManager._instance.birds.Remove(this);
        Destroy(gameObject);  
        GameManager._instance.NextBird();

    }

}
