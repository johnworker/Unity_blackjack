using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // ���}���A�Ω󪱮a�M�o�P��

    // ���o��L�}��
    public Card cardScript;
    public Deck deckScript;

    // �������ƭȨ��M�� ���a/�o�P�� ����P
    public int handValue = 0;

    // ��`����
    private int money = 1000;

    // ��l�W���d������Ʋ�
    public GameObject[] hand;

    // �U�@�i�n½�P������
    public int cardIndex = 0;
    public int aceCount = 0;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
