using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // 此腳本適用於玩家和莊家

    // 取得其他腳本
    public CardScript cardScript;
    public Deck deckScript;

    // 全部的數值取決於 玩家/莊家 的手牌
    public int handValue = 0;

    // 投注金錢
    private int money = 1000;

    // 桌子上的卡片物件數組
    public GameObject[] hand;

    // 下一張要翻牌的索引
    public int cardIndex = 0;

    // 追蹤 1 到 11 轉換的 王牌
    List<CardScript> aceList = new List<CardScript>();
    public void StartHand()
    {
        GetCard();
        GetCard();
    }

    // 發牌給玩家/莊家 的手中
    public int GetCard()
    {
        // 取得撲克牌，用發牌將圖像和價值分配給桌子上的卡
        int cardValue = deckScript.DealCard(hand[cardIndex].GetComponent<CardScript>());
        // 展示撲克牌在遊戲螢幕
        hand[cardIndex].GetComponent<Renderer>().enabled = true;
        // 將撲克牌面值加到手牌總數中
        handValue += cardValue;
        // 如果值為 1，則為 王牌
        if(cardValue == 1)
        {
            aceList.Add(hand[cardIndex].GetComponent<CardScript>());
        }

        // 檢查我們是否應該使用 11 而不是 1
        // AceCheck();
        cardIndex++;
        return handValue;
    }
}
