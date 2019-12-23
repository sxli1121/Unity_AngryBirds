using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Pig : MonoBehaviour
{
   public float maxSpeed=20;
   public float minSpeed=8;
   private SpriteRenderer render;

   public Sprite hurt;

   public GameObject boom;
   public GameObject score;

   public bool isPig=false;

   public AudioClip hurtClip;
   public AudioClip dead;
   public AudioClip birdCollision;
   private void Awake() {
      render=GetComponent<SpriteRenderer>();
   }
   private void OnCollisionEnter2D(Collision2D collision) {
      float speed=collision.relativeVelocity.magnitude;

      if(collision.gameObject.tag=="Player"){//猪碰撞到的物体的tag为player;排除猪与地面碰撞的情况
         AudioPlay(birdCollision);
         collision.transform.GetComponent<Bird>().Hurt();

      }

      if (speed>maxSpeed)//直接死亡
      {
          Dead();
      }else if(speed>minSpeed && speed<maxSpeed ){//受到伤害
         render.sprite=hurt;
         AudioPlay(hurtClip);

      }
   }
   

public void Dead(){
   if(isPig){
      GameManager._instance.pigs.Remove(this);
   }
   //销毁猪对象，同时生成销毁动画对象
   Destroy(gameObject);
   Instantiate(boom,transform.position,Quaternion.identity);

   AudioPlay(dead);//播放音乐

   //生成分数对象，随后销毁
   GameObject go=Instantiate(score,transform.position+new Vector3(0,0.5f,0),Quaternion.identity);
   Destroy(go,1.5f);

}
 public void AudioPlay(AudioClip clip){
    //使用静态方法播放音乐，避免在物体上挂载播放组件时，物体被销毁后，音乐戛然而止
        AudioSource.PlayClipAtPoint(clip,transform.position);
    }
  
}
