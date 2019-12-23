using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
//异步加载游戏场景，防止卡顿
public class LoadLevelAsync : MonoBehaviour
{
    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        Screen.SetResolution(800,460,false);//设置分辨率
       Invoke("Load",2);
    }

    void Load(){
        SceneManager.LoadSceneAsync(1);
    }
}
