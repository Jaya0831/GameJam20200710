using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    // 实例化
    private static GameManager _intance;
    public GameManager GameManager{
        get{
            return _intance;
        }
    }
    // 关卡总数
    public static int levelNum=3;
    // 当前关卡
    public static int level;
    //技能（总）
    public enum Skills{
        Walk,
        Jump

    }
    //每一关对应可使用技能
    public List<Skills>[levelNum] skillForEachLevel;
    // 返回关卡对应可使用的技能
    public List<Skills> GetSkillsForThisLevel(){
        return skillForEachLevel[]
    }

    static public int level=1;
    public LoadScence(int level){
        ScenceManager.LoadScence(level-1);
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
