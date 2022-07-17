using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 此腳本適用於玩家和發牌員

    // 取得其他腳本
    public Card cardScript;
    public Deck deckScript;

    // 全部的數值取決於 玩家/發牌員 的手牌
    public int handValue = 0;

    // 投注金錢
    private int money = 1000;

    // 桌子上的卡片物件數組
    public GameObject[] hand;

    // 下一張要翻牌的索引
    public int cardIndex = 0;
    public int aceCount = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
