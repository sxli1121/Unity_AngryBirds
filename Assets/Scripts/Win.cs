using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    //动画播放完后显示星星
    public void show(){
        GameManager._instance.ShowStars();
    }
}
