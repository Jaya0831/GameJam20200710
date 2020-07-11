using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    public Button startButton;
    public Button[] skillButtons;
    public GameObject selectedButtonPrefabs;
    public Button[] selectedButtons;
    public GameObject selectedPanel;
    /// <summary>
    /// 不同SelectedButton数量时，每个按钮的位置
    /// </summary>
    private List<Vector2>[] positionOfSelectedButton = new List<Vector2>[GameManager.levelNum];
    /// <summary>
    /// 存储目前可以使用的技能
    /// </summary>
    private GameManager.Skill[] enableSkills;
    private int[] matchedSkillButtons;
    /// <summary>
    /// 指向将要被决定的selectButton位置
    /// </summary>
    private int selectButtonPoint;
    private bool[] isSelectButtonEmpty;

    private void Awake()
    {
        List<Vector2> temp1 = new List<Vector2> { new Vector2(0, 0) };
        //todo
    }
    private void Start()
    {
        ResetUI();
    }

    public void ResetUI()
    {
        selectButtonPoint = 0;
        isSelectButtonEmpty = new bool[selectedButtons.Length];
        for (int i = 0; i < isSelectButtonEmpty.Length; i++)
        {
            isSelectButtonEmpty[i] = true;
        }
        enableSkills = new GameManager.Skill[selectedButtons.Length];
        matchedSkillButtons = new int[selectedButtons.Length];
        for (int i = 0; i < matchedSkillButtons.Length; i++)
        {
            matchedSkillButtons[i] = -1;
        }
        List<GameManager.Skill> temp = GameManager.gameManager.GetSkillsForThisLevel();
        int len = temp.Count;
        for (int i = 0; i < len; i++)
        {
            skillButtons[i].image.sprite = GameManager.gameManager.skillSprites[(int)temp[i]];
        }
        // SelectButton初始化不可以点
        for (int i = 0; i < selectedButtons.Length; i++)
        {
            selectedButtons[i].enabled = false;
        }
    }
    public void Select(int num)
    {
        selectedButtons[selectButtonPoint].image.sprite = skillButtons[num].image.sprite;
        Debug.Log(selectButtonPoint);
        enableSkills[selectButtonPoint] = GameManager.gameManager.skillForEachLevel[GameManager.gameManager.level][num];
        matchedSkillButtons[selectButtonPoint] = num;
        isSelectButtonEmpty[selectButtonPoint] = false;
        selectedButtons[selectButtonPoint].enabled = true;
        NextPoint();
        skillButtons[num].enabled = false;
        
        //todo：传递给playercontroller
    }
    public void Unselect(int num)
    {
        selectedButtons[num].image.sprite = null;
        isSelectButtonEmpty[num] = true;
        NextPoint();
        skillButtons[matchedSkillButtons[num]].enabled = true;
        selectedButtons[num].enabled = false;
        matchedSkillButtons[num] = -1;
    }
    private void Update()
    {
        if (selectButtonPoint == selectedButtons.Length)
        {
            for (int i = 0; i < skillButtons.Length; i++)
            {
                skillButtons[i].enabled = false;
            }
            startButton.enabled = true;
        }
        else
        {
            for (int i = 0; i < skillButtons.Length; i++)
            {
                skillButtons[i].enabled = true;
            }
            for (int i = 0; i < matchedSkillButtons.Length; i++)
            {
                if (matchedSkillButtons[i]!=-1)
                {
                    skillButtons[matchedSkillButtons[i]].enabled = false;
                }
            }
            startButton.enabled = false;
        }
        //for (int i = 0; i < isSelectButtonEmpty.Length; i++)
        //{
        //    Debug.Log(i+":"+isSelectButtonEmpty[i]);
        //}
    }
    public void Determine()
    {
        for (int i = 0; i < enableSkills.Length; i++)
        {
            Debug.Log(enableSkills[i]);
        }
        for (int i = 0; i < selectedButtons.Length; i++)
        {
            selectedButtons[i].enabled = false;
        }
        startButton.enabled = false;
    }
    private void NextPoint()
    {
        for (int i = 0; i < isSelectButtonEmpty.Length; i++)
        {
            if (isSelectButtonEmpty[i])
            {
                selectButtonPoint = i;
                return;
            }
        }
        selectButtonPoint = isSelectButtonEmpty.Length;
    }
}

