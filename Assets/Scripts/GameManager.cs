using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // 遊戲按鈕
    public Button dealBtn;
    public Button hitBtn;
    public Button standBtn;
    public Button betBtn;

    // 訪問玩家和莊家的腳本
    public Player playerScript;
    public Player dealerScript;

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
        playerScript.StartHand();
    }

    private void HitClicked()
    {
        throw new NotImplementedException();
    }

    private void StandClicked()
    {
        throw new NotImplementedException();
    }

    #endregion
}
