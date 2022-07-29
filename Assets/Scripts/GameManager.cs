using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // �C�����s
    public Button dealBtn;
    public Button hitBtn;
    public Button standBtn;
    public Button betBtn;

    private int standClicks = 0;

    // �X�ݪ��a�M���a���}��
    public Player playerScript;
    public Player dealerScript;


    // ���}�X�ݩM��s�� ��r ��ܾ�
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI dealerScoreText;
    public TextMeshProUGUI betsText;
    public TextMeshProUGUI cashText;
    public TextMeshProUGUI mainText;
    public TextMeshProUGUI standBtnText;

    // ���ò��a�ĤG�i�P
    public GameObject hideCard;
    // ���h�ֽ䥻
    int pot = 0;

    void Start()
    {
        // �W�[�I�����s (��ť�� AddListener)
        // ��ť���o�����w�ʧ@
        dealBtn.onClick.AddListener(() => DealClicked());
        hitBtn.onClick.AddListener(() => HitClicked());
        standBtn.onClick.AddListener(() => StandClicked());
        betBtn.onClick.AddListener(() => BetClicked());
    }

    #region �C���i�欣�P�禡
    private void DealClicked()
    {
        // ���m �^�X�A���� ��r�A�ǳƷs���o�P
        playerScript.ResetHand();
        dealerScript.ResetHand();

        // �b�o�P�}�l�����õo���P�n��
        mainText.gameObject.SetActive(false);
        dealerScoreText.gameObject.SetActive(false);
        GameObject.Find("Deck").GetComponent<DeckScript>().shuffle();
        playerScript.StartHand();
        dealerScript.StartHand();

        // ��s��ܪ�����
        scoreText.text ="Hand:" + playerScript.handValue.ToString();
        dealerScoreText.text = "Hand:" + dealerScript.handValue.ToString();
        // �ҥ����ò��a���䤤�@�i�P
        hideCard.GetComponent<Renderer>().enabled = true;
        // �վ���s�ਣ��
        dealBtn.gameObject.SetActive(false);
        hitBtn.gameObject.SetActive(true);
        standBtn.gameObject.SetActive(true);
        standBtnText.text = "Stand";

        // �]�w�з�pot�j�p
        pot = 40;

        betsText.text = "Bets: $" + pot.ToString();
        playerScript.AdjustMoney(-20);
        cashText.text = "$" + playerScript.Getmoney().ToString();
    }


    private void HitClicked()
    {
        // �ˬd��l�W�O�_�٦��Ŷ�
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
            // ���a����
            dealerScoreText.text = "Hand: " + dealerScript.handValue.ToString();
            if (dealerScript.handValue > 20) RoundOver();
        }
    }

    // �ˬd���Ĺ�A��P���ȶW�L
    void RoundOver()
    {
        // ���L�� (�O/�_) �z�P �M �³ǧJ/21
        bool playerBust = playerScript.handValue > 21;
        bool dealerBust = dealerScript.handValue > 21;
        bool player21 = playerScript.handValue == 21;
        bool dealer21 = dealerScript.handValue == 21;

        // �p�G�g�L�Q�I�������Ƥ֩�⦸�A�S�� 21�I �� �z�P�A�h�X�\��
        if (standClicks < 2 && !playerBust && !dealerBust && !player21 && !dealer21) return;
        bool roundOver = true;

        // �z�P�A�䥻���Ʀ^��
        if(playerBust && dealerBust)
        {
            mainText.text = "All Bust: Bets Returned";
            playerScript.AdjustMoney(pot / 2);
        }

        // �p�G���a�z�P�A���a�S�z�P �� �p�G���a�I�Ʀh�󪱮a �h���aĹ
        else if(playerBust || (!dealerBust && dealerScript.handValue > playerScript.handValue))
        {
            mainText.text = "Dealer Wins";
        }

        // �p�G���a�z�P�A���a�S�z�P �� �p�G���a�I�Ʀh����a �h���aĹ
        else if (dealerBust || playerScript.handValue > dealerScript.handValue)
        {
            mainText.text = "You Wins";
            playerScript.AdjustMoney(pot);
        }

        // �ˬd�O�_�����A�k�ٽ䥻
        else if(playerScript.handValue == dealerScript.handValue)
        {
            mainText.text = "Push: Bets returned";
            playerScript.AdjustMoney(pot / 2);
        }

        else
        {
            roundOver = false;
        }

        // ���U�@�B�]�m ui / �⤤ / ½��
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

    // �p�G�I���U�`�A�h�V�i���[��
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

