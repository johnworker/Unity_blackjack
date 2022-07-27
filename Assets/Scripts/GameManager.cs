using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.UI;

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
    public Text scoreText;
    public Text dealerScoreText;
    public Text betsText;
    public Text cashText;
    // ���} Text �D�n��r
    public Text standBtnText;

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
        GameObject.Find("Deck").GetComponent<Deck>().shuffle();
        playerScript.StartHand();
        dealerScript.StartHand();
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
