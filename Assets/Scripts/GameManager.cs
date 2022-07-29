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
        betBtn.onClick.AddListener(() => BetClicked());
    }

    #region 遊戲進行派牌函式
    private void DealClicked()
    {
        // 重置 回合，隱藏 文字，準備新局發牌
        playerScript.ResetHand();
        dealerScript.ResetHand();

        // 在發牌開始時隱藏發放手牌積分
        mainText.gameObject.SetActive(false);
        dealerScoreText.gameObject.SetActive(false);
        GameObject.Find("Deck").GetComponent<DeckScript>().shuffle();
        playerScript.StartHand();
        dealerScript.StartHand();

        // 更新顯示的分數
        scoreText.text ="Hand:" + playerScript.handValue.ToString();
        dealerScoreText.text = "Hand:" + dealerScript.handValue.ToString();
        // 啟用隱藏莊家的其中一張牌
        hideCard.GetComponent<Renderer>().enabled = true;
        // 調整按鈕能見度
        dealBtn.gameObject.SetActive(false);
        hitBtn.gameObject.SetActive(true);
        standBtn.gameObject.SetActive(true);
        standBtnText.text = "Stand";

        // 設定標準pot大小
        pot = 40;

        betsText.text = "Bets: $" + pot.ToString();
        playerScript.AdjustMoney(-20);
        cashText.text = "$" + playerScript.Getmoney().ToString();
    }


    private void HitClicked()
    {
        // 檢查桌子上是否還有空間
        if (playerScript.cardIndex <= 10)
        {
            playerScript.GetCard();
            scoreText.text = "Hand: " + playerScript.handValue.ToString();
            if (playerScript.handValue > 20) RoundOver();
        }
    }

    private void StandClicked()
    {
        standClicks++;
        if (standClicks > 1) RoundOver();
        HitDealer();
        standBtnText.text = "Call";
    }

    private void HitDealer()
    {
        while(dealerScript.handValue < 16 && dealerScript.cardIndex < 10)
        {
            dealerScript.GetCard();
            // 莊家分數
            dealerScoreText.text = "Hand: " + dealerScript.handValue.ToString();
            if (dealerScript.handValue > 20) RoundOver();
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
            playerScript.AdjustMoney(pot / 2);
        }

        // 如果玩家爆牌，莊家沒爆牌 或 如果莊家點數多於玩家 則莊家贏
        else if(playerBust || (!dealerBust && dealerScript.handValue > playerScript.handValue))
        {
            mainText.text = "Dealer Wins";
        }

        // 如果莊家爆牌，玩家沒爆牌 或 如果玩家點數多於莊家 則玩家贏
        else if (dealerBust || playerScript.handValue > dealerScript.handValue)
        {
            mainText.text = "You Wins";
            playerScript.AdjustMoney(pot);
        }

        // 檢查是否平局，歸還賭本
        else if(playerScript.handValue == dealerScript.handValue)
        {
            mainText.text = "Push: Bets returned";
            playerScript.AdjustMoney(pot / 2);
        }

        else
        {
            roundOver = false;
        }

        // 為下一步設置 ui / 手中 / 翻轉
        if (roundOver)
        {
            hitBtn.gameObject.SetActive(false);
            standBtn.gameObject.SetActive(false);
            dealBtn.gameObject.SetActive(true);
            mainText.gameObject.SetActive(true);
            dealerScoreText.gameObject.SetActive(true);
            hideCard.GetComponent<Renderer>().enabled = false;
            cashText.text = "$" + playerScript.Getmoney().ToString();
            standClicks = 0;
        }
    }
    #endregion

    // 如果點擊下注，則向檯面加錢
    void BetClicked()
    {
        Text newBtn = betBtn.GetComponentInChildren(typeof(Text)) as Text;
        int intBet = int.Parse(newBtn.text.ToString().Remove(0, 1));
        playerScript.AdjustMoney(-intBet);
        cashText.text = "$" + playerScript.Getmoney().ToString();
        pot += (intBet * 2);
        betsText.text = "Bets: $" + pot.ToString();
    }
}

