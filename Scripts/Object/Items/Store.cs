using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Store : MonoBehaviour
{
    Player player;
    public int itemList = 0; // Item.Kind
    public int price = 0; // ����
    //UIó��
    public TextMeshProUGUI itemPrice;
    public TextMeshProUGUI playerMoney;
    //ui���� ó��
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        itemPrice.text = "���� : " + price + "Gold";
    }
    //�÷��̾� �������� �ǽð����� �ҷ���
    void Update()
    {
        playerMoney.text = "������ : " + player.money + "Gold";
    }
    //������ ����(�÷��̾��κ��丮[itemList]++)
    public void SaleItem() // ������ ���� 
    {
        if (player.money >= price)
        {
            Player.inventory[itemList]++;
            player.money -= price;
        }
        else
            Debug.Log("���� ��� ����");
    }
}
