using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    /// <summary>
    /// 单例模式
    /// </summary>
    private static GameManager _intance;
    public static GameManager gameManager
    {
        get
        {
            return _intance;
        }
    }
    public bool IsRunning;
    /// <summary>
    /// 关卡总数
    /// </summary>
    public static int levelNum = 3;
    /// <summary>
    /// 当前关卡
    /// </summary>
    public int level;
    /// <summary>
    /// 主角
    /// </summary>
    private GameObject player;
    /// <summary>
    /// 技能（总）
    /// </summary>
    public enum Skill
    {
        WalkRight,
        WalkLeft,
        Jump,
        Crouch
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
        List<Skill> level1 = new List<Skill> { Skill.WalkRight, Skill.WalkLeft, Skill.Jump, Skill.Crouch };
        skillForEachLevel[0] = level1;
        IsRunning = false;
    }
    public void StartTheGame()
    {
        IsRunning = true;
        player.GetComponent<PlayerController>().stageActs.Clear();
        for (int i = 0; i < UIController.uIController.enableSkills.Length; i++)
        {
            player.GetComponent<PlayerController>().stageActs.Add(UIController.uIController.enableSkills[i].ToString());
        }
    }
}