using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public List<Bird> birds;
    public List<Pig> pigs;
    public static GameManager _instance;
    private Vector3 originPos;//小鸟初始位置

    
    public GameObject win;
    public GameObject lose;
    public GameObject[] stars;

    private int starsNum=0;//星星数量

    public int totalNum=9;//关卡数
    
    private void Awake() {
        _instance=this;
        if(birds.Count>0)
        originPos=birds[0].transform.position;
        

    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
   private void Start()
    {
        Initialized();
    }
    private void Initialized() {
        for(int i=0;i<birds.Count;i++){
            if(i==0){//是集合中的第一只小鸟
                birds[i].transform.position=originPos;//解决小鸟轮换速度突然变大的问题
                birds[i].enabled=true;
                birds[i].sp.enabled=true;
                birds[i].canMove=true;//只有上弹弓的小鸟才可被点击
            }else{
                birds[i].enabled=false;
                birds[i].sp.enabled=false;
                birds[i].canMove=false;
            }
        }
    }

  public  void NextBird(){
        if(pigs.Count>0){
            if(birds.Count>0){
                //下一只小鸟飞
                Initialized();
            }else{
                //游戏失败
                lose.SetActive(true);
            }

        }else{
            //游戏胜利
            win.SetActive(true);
        }
    }

    // public void ShowStars(){
    //      for(int i=0;i<birds.Count+1;i++){
            
    //         stars[i].SetActive(true);               

    //     }
    // }
    
    public void Replay(){
        SaveData();
        SceneManager.LoadScene(2);

    }

    public void Home(){
        SaveData();
        SceneManager.LoadScene(1);
    }

public void ShowStars(){
    StartCoroutine("show");
}

    IEnumerator show(){
        for(;starsNum<birds.Count+1;starsNum++){

            if(starsNum>=stars.Length){
                break;
            }

            yield return new WaitForSeconds(0.2f);
            stars[starsNum].SetActive(true);       

        }
        print("星星数");
        Debug.Log(starsNum);
        SaveData();
    }

    //存储本关卡的星星数量
    public void SaveData(){
        if(starsNum>PlayerPrefs.GetInt(PlayerPrefs.GetString("nowLevel"))){
            
            PlayerPrefs.SetInt(PlayerPrefs.GetString("nowLevel"),starsNum);
        }
        
        //存储所有的星星个数
        int sum=0;
        for(int i=1;i<=totalNum;i++){
            sum+=PlayerPrefs.GetInt("level"+i.ToString());
        }
        PlayerPrefs.SetInt("totalNum",sum);
    }

}
