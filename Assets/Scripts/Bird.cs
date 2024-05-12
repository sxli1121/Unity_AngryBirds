using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Bird : MonoBehaviour
{
    private bool isClick=false;
    
    public float maxDis=2;//拖拽的最大距离
    [HideInInspector]//虽然是公有变量，但能在Inspector面板中隐藏此变量
    public SpringJoint2D sp;
    protected Rigidbody2D rg;//刚体组件中含有小鸟速度的属性

    public LineRenderer right;
    public LineRenderer left;
    public Transform rightPos;
    public Transform leftPos;

    public GameObject boom;
    protected TestMyTrail myTrail;
    [HideInInspector]
    //解决小鸟飞出后还能被点击的bug
    
    public bool canMove=false;

    public float smooth=3;

    public AudioClip select;
    public AudioClip fly;
    private bool isFly=false;//处于飞行状态
    public bool isRelease=false;

    public Sprite hurt;//小鸟受伤图片

    protected SpriteRenderer render;

    private void Awake() {//脚本实例化时加载组件
        sp=GetComponent<SpringJoint2D>();
        rg=GetComponent<Rigidbody2D>();
        myTrail=GetComponent<TestMyTrail>();
        render=GetComponent<SpriteRenderer>();
        
    }

    private void OnMouseDown() {
        if(canMove){
            isClick=true;
            rg.isKinematic=true;//刚体脱离物理的控制

            //激活画线组件
            right.enabled=true;
            left.enabled=true;

            //播放音效
            AudioPlay(select);

        }
        
    }

    private void OnMouseUp() {
        if (canMove)
        {
            isClick=false;
            rg.isKinematic=false;   
            Invoke("Fly",0.1f);//延时后再禁用springjoint2D组件，保证速度让小鸟飞出
        
            //禁用画线组件
            right.enabled=false;
            left.enabled=false;
            canMove=false;
        }
        
    }

    // 鼠标一直按着，进行位置的跟随；鼠标屏幕坐标转为小鸟的世界坐标
    private void Update() {
        //判断是否点击了UI界面
        if(EventSystem.current.IsPointerOverGameObject())
        return;

        if(isClick){
            transform.position=Camera.main.ScreenToWorldPoint(Input.mousePosition);
            transform.position+=new Vector3(0,0,-Camera.main.transform.position.z);
        
            if(Vector3.Distance(transform.position,rightPos.position)>maxDis){//拖拽小鸟最大距离限制
                Vector3 pos=(transform.position-rightPos.position).normalized;//单位化向量
                pos *=maxDis;//最大长度的向量
                transform.position=pos+rightPos.position;

            }

            Line();
        
        }


        if(isFly){
            // 当鸟在飞行状态的时候不允许再次点击
            return;
        }
    }

    void Fly(){//小鸟飞出
        isFly=true;
        isRelease=true;
        AudioPlay(fly);
        myTrail.StartTrails();//飞行时有小尾巴
        sp.enabled=false;
        Invoke("Next",5);
    }

    // 画皮筋的线
    void Line(){
        right.SetPosition(0,rightPos.position);
        right.SetPosition(1,transform.position);

        left.SetPosition(0,leftPos.position);
        left.SetPosition(1,transform.position);

    }

   protected virtual void Next(){//下一只小鸟飞出,从集合中移除当前小鸟并销毁该小鸟对象
        GameManager._instance.birds.Remove(this);
        Destroy(gameObject);
        Instantiate(boom,transform.position,Quaternion.identity);
        GameManager._instance.NextBird();

   }

    private void OnCollisionEnter2D(Collision2D collision) {
        isFly=false;
        myTrail.ClearTrails();//小鸟碰撞到物体时取消小尾巴
        
    }

    public void AudioPlay(AudioClip clip){
        AudioSource.PlayClipAtPoint(clip,transform.position);
    }

    /*
    展示技能效果
    */
    public virtual void ShowSkill(){


        isFly=false;

    }


    public void Hurt(){
        render.sprite=hurt;
    }

}
