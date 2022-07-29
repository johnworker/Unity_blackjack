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

    // 眔汲礟计よΑ
    void GetCardValues()
    {
        int Num = 0;

        // ノ癹伴だ皌倒汲礟计
        for (int i = 0; i < cardSprites.Length; i++)
        {
            Num = i;

            // 璸计汲礟计秖 (52)
            Num %= 13;

            // 狦 x/13 ぇΤ緇计玥緇计
            // ㄏノ埃獶禬筁10玥ㄏノ10
            if (Num > 10 || Num == 0)
            {
                Num = 10;
            }

            cardValues[i] = Num++;
        }


    }

    public void shuffle()
    {
        // 夹非皚戈计沮ユ传м砃
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

        // (currentIndex 碞琌讽玡ま)
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
