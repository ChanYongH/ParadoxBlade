using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Store : MonoBehaviour
{
    Player player;
    public int itemList = 0; // Item.Kind
    public int price = 0; // 가격
    //UI처리
    public TextMeshProUGUI itemPrice;
    public TextMeshProUGUI playerMoney;
    //ui가격 처리
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        itemPrice.text = "가격 : " + price + "Gold";
    }
    //플레이어 소지금을 실시간으로 불러옴
    void Update()
    {
        playerMoney.text = "소지금 : " + player.money + "Gold";
    }
    //아이템 구매(플레이어인벤토리[itemList]++)
    public void SaleItem() // 아이템 구매 
    {
        if (player.money >= price)
        {
            Player.inventory[itemList]++;
            player.money -= price;
        }
        else
            Debug.Log("돈이 없어서 못삼");
    }
}
