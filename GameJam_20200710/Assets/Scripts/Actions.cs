using System;
using System.Reflection;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions
{
    public float JumpForce;
    public float WalkSpeed;
    private bool isCrouch = false;
    public void Jump(Rigidbody2D rb)
    {
        CheckBoxSize(rb);
        // 取消xy速度，并跳跃
        rb.velocity = Vector2.zero;

        rb.AddForce(Vector2.up * JumpForce, ForceMode2D.Impulse);
    }

    public void WalkRight(Rigidbody2D rb)
    {
        CheckBoxSize(rb);
        rb.velocity = Vector2.right * WalkSpeed;
    }

    public void WalkLeft(Rigidbody2D rb)
    {
        CheckBoxSize(rb);
        rb.velocity = Vector2.left * WalkSpeed;
    }

    public void Crouch(Rigidbody2D rb)
    {

        // 获取物体的碰撞盒，改变形状
        Collider2D[] results = new Collider2D[1];
        rb.GetAttachedColliders(results);
        BoxCollider2D box = (BoxCollider2D)results[0];
        float height = box.size.y;
        if (isCrouch)
        {
            height *= 2;
            box.size = new Vector2(box.size.x, height);
            isCrouch = false;
        }
        else
        {
            height /= 2;
            box.size = new Vector2(box.size.x, height);
            box.offset = new Vector2(0, -height / 2);
            isCrouch = true;
        }

        rb.velocity = Vector2.zero;
    }

    public void CheckBoxSize(Rigidbody2D rb)
    {
        Collider2D[] results = new Collider2D[1];
        rb.GetAttachedColliders(results);
        BoxCollider2D box = (BoxCollider2D)results[0];
        float height = box.size.y;
        if (isCrouch)
        {
            height *= 2;
            box.offset = Vector2.zero;
            box.size = new Vector2(box.size.x, height);
            isCrouch = false;
        }

    }
}
