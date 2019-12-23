using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//地图选择
public class MapSelect : MonoBehaviour
{
    public int starsNum=0;//解锁关卡所需星星数
    private bool isSelect=false;//该大关是否可被选择
    //[HideInInspector] 可设置公有属性在面板上不显示
    public GameObject locks;
    public GameObject stars;
    public GameObject panel;
    public GameObject map;

    public Text starsText;
    public int startNum=1;//开始的关卡数
    public int endNum=3;//最后的关卡数



    private void Start(){
        //PlayerPrefs.DeleteAll();//移除所有的键值

    //获得当前个关卡所获得的星星总数，设置默认值为0，
      if(PlayerPrefs.GetInt("totalNum",0)>=starsNum){
          isSelect=true;
      }  

      if(isSelect){
          //锁隐藏，星星显示
          locks.SetActive(false);
          stars.SetActive(true);
        
        //显示已经获得的星星数和总星星数之和
        int counts=0;
        for(int i=startNum;i<=endNum;i++){
            //从第startNum关到endNum关，所获得的星星数之和，如果没有一颗星星，默认为0
                counts+=PlayerPrefs.GetInt("level"+i.ToString(),0);
        }
        starsText.text=counts.ToString()+"/9";

      }
    }

    public void Selected(){//地图被选择
        if(isSelect){
            panel.SetActive(true);
            map.SetActive(false);

        }
    }

    public void panelSelect(){//在panel面板中选择返回
        panel.SetActive(false);
        map.SetActive(true);
    }

    

}
