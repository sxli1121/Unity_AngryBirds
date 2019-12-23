using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boom : MonoBehaviour
{
    public void destroying(){//动画效果播放完后销毁动画对象
        Destroy(gameObject);
    }
}
