using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 遊戲按鈕
    public Button dealBtn;
    public Button hitBtn;
    public Button standBtn;
    public Button betBtn;
    private int standClicks = 0;

    // 訪問玩家和莊家的腳本
    public Player playerScript;
    public Player dealerScript;


    // 公開訪問和更新的 文字 顯示器
    public Text scoreText;
    public Text dealerScoreText;
    public Text betsText;
    public Text cashText;
    // 公開 Text 主要文字
    public Text standBtnText;

    // 隱藏莊家第二張牌
    public GameObject hideCard;
    // 有多少賭本
    int pot = 0;

    void Start()
    {
        // 增加點擊按鈕 (監聽器 AddListener)
        // 監聽器發派指定動作
        dealBtn.onClick.AddListener(() => DealClicked());
        hitBtn.onClick.AddListener(() => HitClicked());
        standBtn.onClick.AddListener(() => StandClicked());
    }

    #region 遊戲進行派牌函式
    private void DealClicked()
    {
        GameObject.Find("Deck").GetComponent<Deck>().shuffle();
        playerScript.StartHand();
        dealerScript.StartHand();
    }

    private void HitClicked()
    {
        // 檢查桌子上是否還有空間
        if (playerScript.GetCard() <= 10)
        {
            playerScript.GetCard();
        }
    }

    private void StandClicked()
    {
        standClicks++;
        if (standClicks > 1) Debug.Log("end function");
        HitDealer();
        standBtnText.text = "Call";
    }

    private void HitDealer()
    {
        while(dealerScript.handValue < 16 && dealerScript.cardIndex < 10)
        {
            dealerScript.GetCard();
            // 莊家分數
        }
    }

    #endregion
}
