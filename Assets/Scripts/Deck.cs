using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class Deck : MonoBehaviour
{
    public Sprite[] cardSprites;
    int[] cardValues = new int[53];
    int currentIndex = 0;
    void Start()
    {
        GetCardValues();
    }

    // ���o���J�P�ƭȪ��覡
    void GetCardValues()
    {
        int Num = 0;

        // �ΰj����t�����J�P�ƭ�
        for (int i = 0; i < cardSprites.Length; i++)
        {
            Num = i;

            // �p�Ƽ��J�P�ƶq (52)
            Num %= 13;

            // �p�G x/13 ���ᦳ�l�ơA�h�l��
            // �ϥΪ��ȡA���D�W�L10�A�h�ϥ�10
            if (Num > 10 || Num == 0)
            {
                Num = 10;
            }

            cardValues[i] = Num++;
        }


    }

    public void shuffle()
    {
        // �зǰ}�C��Ƽƾڥ洫�޳N
        for (int i = cardSprites.Length -1; i > 0; --i)
        {
            int j = Mathf.FloorToInt(Random.Range(0.0f, 1.0f) * cardSprites.Length - 1) + 1;
            Sprite face = cardSprites[i];
            cardSprites[i] = cardSprites[j];
            cardSprites[j] = face;

            int value = cardValues[i];
            cardValues[i] = cardValues[j];
            cardValues[j] = value;
        }

        // (currentIndex �N�O��e����)
        currentIndex = 1;

    }

    public int DealCard(CardScript card)
    {
        card.SetSprite(cardSprites[currentIndex]);
        card.SetValue(cardValues[currentIndex]);
        currentIndex++;
        return card.GetValueOfCard();
    }

    public Sprite GetCardBack()
    {
        return cardSprites[0];
    }

}
