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
    // ���} Text �D�n��r
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
    }

    #region �C���i�欣�P�禡
    private void DealClicked()
    {
        // �b�o�P�}�l�����õo���P�n��
        dealerScoreText.gameObject.SetActive(false);
        GameObject.Find("Deck").GetComponent<Deck>().shuffle();
        playerScript.StartHand();
        dealerScript.StartHand();

        // ��s��ܪ�����
        scoreText.text ="Hand:" + playerScript.handValue.ToString();
        dealerScoreText.text = "Hand:" + dealerScript.handValue.ToString();
        // �վ���s�ਣ��
        dealBtn.gameObject.SetActive(false);
        hitBtn.gameObject.SetActive(true);
        standBtn.gameObject.SetActive(true);
        standBtnText.text = "Stand";

        // �]�w�з�pot�j�p
        pot = 40;

        betsText.text = pot.ToString();
        // ���a�}��.�վ����(-20);
        // cashText.text = playerScript.GetMoney().Tostring();
    }


    private void HitClicked()
    {
        // �ˬd��l�W�O�_�٦��Ŷ�
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
            // ���a����
        }
    }

    #endregion
}

