using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
//关卡选择
public class LevelSelect : MonoBehaviour
{
    public bool isSelect=false;//是否可选

    public Sprite levelBG;

    private Image image;

    public GameObject [] stars;//星星数组
    private void Awake()
    {
        //GetCompoment <T>()从当前游戏对象获取组件T，只在当前游戏对象中获取，没得到的就返回null，不会去子物体中去寻找。
        image=GetComponent<Image>();
    }
    private void Start(){
        if(transform.parent.GetChild(0).name==gameObject.name){//第一关
            isSelect=true;
        }else{//判断当前关卡是否可选
            int beforeNum=int.Parse(gameObject.name)-1;//前一个关卡的名字
            if(PlayerPrefs.GetInt("level"+beforeNum.ToString())>0){
                isSelect=true;

            }

        }

        if(isSelect){
            image.overrideSprite=levelBG;//更改关卡图片由锁住状态为可选状态
            transform.Find("num").gameObject.SetActive(true);

            //获取当前关卡的名字，然后获得对应的星星个数
            int count=PlayerPrefs.GetInt("level"+gameObject.name);
            if(count>0){
                for(int i=0;i<count;i++){
                    stars[i].SetActive(true);
                }
            }
        }

    }

    //关卡被选择
    public void Selected(){
        if(isSelect){
            PlayerPrefs.SetString("nowLevel","level"+gameObject.name);
            SceneManager.LoadScene(2);
        }
    }

}
