using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float hp;
    public float maxHp;
    public float att;
    public int setAttrib = 0;
    public bool isDead;

    //han�� virtual�� �߰���. �ݼ� ������
    public float Hp
    {
        get
        { return hp; }
        set
        {
            hp = value;
            if (Hp <= 0)
                Dead();
            else if (Hp <= 1)
                OnCrisis();
        }
    }
    public float MaxHp
    {
        get
        { return maxHp; }
        set
        { maxHp = value; }
    }
    public float Att
    {
        get
        { return att; }
        set
        { att = value; }
    }
    public bool OnAttack(bool state)
    {
        if (state)
            return true;
        else
            return false;
    }
    public virtual void Dead(string caller = "") {}
    public virtual void OnHit(int playerAttrib, float[,] item, float damage) { }
    public virtual void OnCrisis(){}
    //��ư�� ������ ������� �ٲ�
}
