using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using TMPro;

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
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI dealerScoreText;
    public TextMeshProUGUI betsText;
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI mainText;
    public TextMeshProUGUI standBtnText;

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
        // 在發牌開始時隱藏發放手牌積分
        dealerScoreText.gameObject.SetActive(false);
        GameObject.Find("Deck").GetComponent<Deck>().shuffle();
        playerScript.StartHand();
        dealerScript.StartHand();

        // 更新顯示的分數
        scoreText.text ="Hand:" + playerScript.handValue.ToString();
        dealerScoreText.text = "Hand:" + dealerScript.handValue.ToString();
        // 調整按鈕能見度
        dealBtn.gameObject.SetActive(false);
        hitBtn.gameObject.SetActive(true);
        standBtn.gameObject.SetActive(true);
        standBtnText.text = "Stand";

        // 設定標準pot大小
        pot = 40;

        betsText.text = pot.ToString();
        // 玩家腳本.調整金幣(-20);
        // cashText.text = playerScript.GetMoney().Tostring();
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

    // 檢查輸或贏，手牌的值超過
    void RoundOver()
    {
        // 布林值 (是/否) 爆牌 和 黑傑克/21
        bool playerBust = playerScript.handValue > 21;
        bool dealerBust = dealerScript.handValue > 21;
        bool player21 = playerScript.handValue == 21;
        bool dealer21 = dealerScript.handValue == 21;

        // 如果經過被點擊的次數少於兩次，沒有 21點 或 爆牌，退出功能
        if (standClicks < 2 && !playerBust && !dealerBust && !player21 && !dealer21) return;
        bool roundOver = true;

        // 爆牌，賭本全數回收
        if(playerBust && dealerBust)
        {
            mainText.text = "All Bust: Bets Returned";
            playerScript.AdjectMoney(pot / 2);
        }

        // 如果玩家爆牌，莊家沒爆牌 或 如果莊家點數多於玩家 則莊家贏
        else if(playerBust || dealerScript.handValue > playerScript.handValue)
        {
            mainText.text = "Dealer Wins";
        }

        // 如果莊家爆牌，玩家沒爆牌 或 如果玩家點數多於莊家 則玩家贏
        else if (dealerBust || playerScript.handValue > dealerScript.handValue)
        {
            mainText.text = "Dealer Wins";
            playerScript.AdjectMoney(pot);
        }

        // 檢查是否平局，歸還賭本
    }

    #endregion
}

