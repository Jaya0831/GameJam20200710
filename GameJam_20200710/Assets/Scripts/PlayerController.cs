using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    public float walkSpeed;
    public float jumpForce;

    public Actions acts = new Actions();
    private string[] curActs = { "Jump", "WalkRight", "WalkLeft" };
    private int actIndex = 0;
    public float intervalTime = 0.5f;
    private float intervalCount;

    private Rigidbody2D rb;
    Type t = typeof(Actions);
    //Hack:宁再改位置
    public List<string> stageActs = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        intervalCount = intervalTime;
        acts.JumpForce = jumpForce;
        acts.WalkSpeed = walkSpeed;
    }

    void LoadStageActs()
    {
        // 从GameManager获取当前关卡的行为
        // 等待GM类施工，暂时用本地硬编码的凑合
    }

    // Update is called once per frame
    void Update()
    {
        intervalCount += Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.Space) && intervalCount >= intervalTime)
        {
            string actName = curActs[actIndex++%curActs.Length];
            Debug.Log(actName);

            DoAction(actName);
            intervalCount = 0;
        }
    }

    void DoAction(string actName)
    {
        MethodInfo mt = t.GetMethod(actName);
        mt.Invoke(acts, new object[]{ rb });
    }
}
