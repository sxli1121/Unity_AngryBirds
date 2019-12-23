using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PausePanel : MonoBehaviour
{
    private Animator anim;
    public GameObject button;
    private void Awake() {
        // anim=GetComponent<Animator>();
        anim=GetComponent<Animator>();
    }

    public void Retry(){
        Time.timeScale=1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);

    }
    //点击了暂停按钮
    public void Pause(){
        
       button.SetActive(false);
       anim.SetBool("isPause",true);
       
    }   
   
    // 点击了继续按钮
    public void Resume(){
        Time.timeScale=1;
        anim.SetBool("isPause",false);
  

    }
     public void Home(){
        
        Time.timeScale=1;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }
    // Pause动画播放完调用
    public void PauseAnimEnd(){

        Time.timeScale=0;

    }
    // Resume动画播放完后调用
    public void ResumeAnimEnd(){
       button.SetActive(true);

    }

}
