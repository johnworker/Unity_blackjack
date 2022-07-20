using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // ���}���A�Ω󪱮a�M���a

    // ���o��L�}��
    public CardScript cardScript;
    public Deck deckScript;

    // �������ƭȨ��M�� ���a/���a ����P
    public int handValue = 0;

    // ��`����
    private int money = 1000;

    // ��l�W���d������Ʋ�
    public GameObject[] hand;

    // �U�@�i�n½�P������
    public int cardIndex = 0;

    // �l�� 1 �� 11 �ഫ�� ���P
    List<CardScript> aceList = new List<CardScript>();
    public void StartHand()
    {
        GetCard();
        GetCard();
    }

    // �o�P�����a/���a ���⤤
    public int GetCard()
    {
        // ���o���J�P�A�εo�P�N�Ϲ��M���Ȥ��t����l�W���d
        int cardValue = deckScript.DealCard(hand[cardIndex].GetComponent<CardScript>());
        // �i�ܼ��J�P�b�C���ù�
        hand[cardIndex].GetComponent<Renderer>().enabled = true;
        // �N���J�P���ȥ[���P�`�Ƥ�
        handValue += cardValue;
        // �p�G�Ȭ� 1�A�h�� ���P
        if(cardValue == 1)
        {
            aceList.Add(hand[cardIndex].GetComponent<CardScript>());
        }

        // �ˬd�ڭ̬O�_���Өϥ� 11 �Ӥ��O 1
        // AceCheck();
        cardIndex++;
        return handValue;
    }
}
