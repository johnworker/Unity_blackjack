using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // ���}���A�Ω󪱮a�M���a

    // ���o��L�}��
    public CardScript cardScript;
    public DeckScript deckScript;

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
        AceCheck();
        cardIndex++;
        return handValue;
    }

    //�j���һݪ� �d�P �ഫ�A1 �� 11 �Ϥ���M
    public void AceCheck()
    {
        foreach(CardScript ace in aceList)
        {
            // ���C�����C�ӥd�P�ˬd
            if (handValue + 10 < 22 && ace.GetValueOfCard() == 1)
            {
                // �p�G�ഫ�A�վ�d��H�ȩM��
                ace.SetValue(11);
                handValue += 10;
            }

            else if(handValue > 21 && ace.GetValueOfCard() == 11)
            {
                // �p�G�ഫ�A�վ�C����H�ȩM��W����
                ace.SetValue(1);
                handValue -= 10;
            }
        }
    }

    // �[�δ���A��`
    public void AdjustMoney(int amount)
    {
        money += amount;
    }

    // ��X���a��e���B
    public int GetMoney()
    {
        return money;
    }

    // ���éҦ����J�P�A���m�һݪ��ܶq
    public void ResetHand()
    {
        for (int i = 0; i < hand.Length; i++)
        {
            hand[i].GetComponent<CardScript>().ResetCard();
            hand[i].GetComponent<Renderer>().enabled = false;
        }

        cardIndex = 0;
        handValue = 0;
        aceList = new List<CardScript>();
    }

}
