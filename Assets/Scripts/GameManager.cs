using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // �C�����s
    public Button dealBtn;
    public Button hitBtn;
    public Button standBtn;
    public Button betBtn;

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
        throw new NotImplementedException();
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
