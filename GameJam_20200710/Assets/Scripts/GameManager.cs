using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
<<<<<<< Updated upstream
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
=======
    /// <summary>
    /// 实例化
    /// </summary>
    private static GameManager _intance;
    public static GameManager gameManager
    {
        get
        {
            return _intance;
        }
    }

    /// <summary>
    /// 关卡总数
    /// </summary>
    public static int levelNum = 3;
    /// <summary>
    /// 当前关卡
    /// </summary>
    public int level;
    /// <summary>
    /// 技能（总）
    /// </summary>
    public enum Skill
    {
        Walk,
        Jump,
        Push,
        Pull
    }
    /// <summary>
    /// 每一关对应可使用技能
    /// </summary>
    public List<Skill>[] skillForEachLevel = new List<Skill>[levelNum];
    /// <summary>
    /// 技能贴图
    /// </summary>
    public Sprite[] skillSprites;
    /// <summary>
    /// 每轮可选择的技能数量
    /// </summary>
    public int[] selectNums = new int[levelNum];

    /// <summary>
    /// 返回关卡对应可使用的技能
    /// </summary>
    /// <returns></returns>
    public List<Skill> GetSkillsForThisLevel()
    {
        return skillForEachLevel[level];
    }

    public void LoadScence(int level)
    {//todo:更新level？
        SceneManager.LoadScene(level - 1);
    }
    private void Awake()
    {
        _intance = this;
        selectNums[0] = 2;
        List<Skill> level1 = new List<Skill> { Skill.Walk, Skill.Jump, Skill.Push, Skill.Pull };
        skillForEachLevel[0] = level1;
>>>>>>> Stashed changes
    }
    // Start is called before the first frame update
    void Start()
    {
<<<<<<< Updated upstream

=======
>>>>>>> Stashed changes
    }

    // Update is called once per frame
    void Update()
    {

    }
}