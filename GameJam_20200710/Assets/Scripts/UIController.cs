using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class UIController : MonoBehaviour
{
    public Button startButton;
    public Button[] skillButtons;
    public Button[] selectedButtonPrefabs;
    private Button[] selectedButtons;
    public GameObject selectedPanel;
    /// <summary>
    /// 不同SelectedButton数量时，每个按钮的位置
    /// </summary>
    private List<Vector2>[] positionOfSelectedButton = new List<Vector2>[GameManager.levelNum];
    /// <summary>
    /// 存储目前可以使用的技能
    /// </summary>
    public GameManager.Skill[] enableSkills;
    private int[] matchedSkillButtons;
    /// <summary>
    /// 指向将要被决定的selectButton位置
    /// </summary>
    private int selectButtonPoint;
    private bool[] isSelectButtonEmpty;
    /// <summary>
    /// 单例模式
    /// </summary>
    private static UIController _intance;
    public static UIController uIController
    {
        get
        {
            return _intance;
        }
    }
    private void Awake()
    {
        _intance = this;
        positionOfSelectedButton[0] = new List<Vector2> { new Vector2(410, 152) };
        positionOfSelectedButton[1] = new List<Vector2> { new Vector2(410, 192), new Vector2(410, 112) };
        positionOfSelectedButton[2] = new List<Vector2> { new Vector2(336, 191), new Vector2(490, 191), new Vector2(410, 110) };


        //todo
    }
    private void Start()
    {
        ResetUI();
    }


    public void ResetUI()
    {
        InstantiateSelectButtons();
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
        for (int i = len; i < 4; i++)
        {
            skillButtons[i].enabled = false;
        }
        // SelectButton初始化不可以点
        for (int i = 0; i < selectedButtons.Length; i++)
        {
            selectedButtons[i].enabled = false;
        }
    } 
    private void InstantiateSelectButtons()
    {
        int len = GameManager.gameManager.GetSelectNum();
        selectedButtons = new Button[len];
        for (int i = 0; i < len; i++)
        {
            Debug.Log("i1:" + i);
            selectedButtons[i] = Instantiate(selectedButtonPrefabs[i], positionOfSelectedButton[len - 1][i], Quaternion.identity, selectedPanel.transform);
        }
        if (selectedButtons.Length>0)
        {
            selectedButtons[0].onClick.AddListener(delegate ()
            {
                Unselect(0);
            });
        }
        if (selectedButtons.Length > 1)
        {
            selectedButtons[1].onClick.AddListener(delegate ()
            {
                Unselect(1);
            });
        }
        if (selectedButtons.Length > 2)
        {
            selectedButtons[2].onClick.AddListener(delegate ()
            {
                Unselect(2);
            });
        }
    }
    public void Select(int num)
    {
        selectedButtons[selectButtonPoint].image.sprite = skillButtons[num].image.sprite;
        enableSkills[selectButtonPoint] = GameManager.gameManager.skillForEachLevel[GameManager.gameManager.GetLevel()][num];
        matchedSkillButtons[selectButtonPoint] = num;
        isSelectButtonEmpty[selectButtonPoint] = false;
        selectedButtons[selectButtonPoint].enabled = true;
        NextPoint();
        skillButtons[num].enabled = false;
    }
    public void Unselect(int num)
    {
        Debug.Log(num);
        Debug.Log(selectedButtons.Length);
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
            List<GameManager.Skill> temp = GameManager.gameManager.GetSkillsForThisLevel();
            int len = temp.Count;
            for (int i = 0; i < len; i++)
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
        GameManager.gameManager.StartTheGame();
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

    public void Restart()
    {
        GameManager.gameManager.LoadScence(GameManager.gameManager.GetLevel());

    }
    public void Next()
    {
        GameManager.gameManager.NextLevel();
    }
}

