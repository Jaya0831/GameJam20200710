using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    
    public float walkSpeed;
    public float jumpForce;

    // 测试字段
    public string[] curActs = { "Jump", "WalkRight", "WalkLeft" , "Crouch"};

    [Header("行为冷却CD（s/秒）")]
    public float intervalTime = 0.5f;
    private float intervalCount;

    public bool isRunning = false;

    private int actIndex = 0;
    public List<string> stageActs = new List<string>();

    private Actions acts = new Actions();
    Type t = typeof(Actions);

    private Rigidbody2D rb;
    private Vector2 boxSize;
    private Vector2 modBox;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        boxSize = GetComponent<BoxCollider2D>().size;
        modBox = boxSize + Vector2.one * 18.0f;
        intervalCount = intervalTime;
        acts.JumpForce = jumpForce;
        acts.WalkSpeed = walkSpeed;

    }

    // Update is called once per frame
    void Update()
    {
        // TODO: 等待添加根据游戏状态来运行内容的判断
        if (isRunning)
        {
            intervalCount += Time.deltaTime;
            HandleInput();
            ModPosition();
        }
    }

    void ModPosition()
    {
        Vector3 pos = transform.position;
        pos.x = (boxSize.x / 2 + pos.x + modBox.x) % modBox.x - boxSize.x / 2;
        pos.y = (boxSize.y / 2 + pos.y + modBox.y) % modBox.y - boxSize.y / 2;
        transform.position = pos;
    }

    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space) && intervalCount >= intervalTime)
        {
            string actName = curActs[actIndex++%curActs.Length];
            Debug.Log(actName);
            if(actName == "Crouch" && !rb.IsTouchingLayers())
            {
                return;
            }

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
