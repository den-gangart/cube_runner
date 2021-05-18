using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerBonuses
{
    public static float _moneyBonus = 1;
    public static float _scoreBonus = 1;
    public static float _speedImprove = 1;
    public static float _jumpImprove = 1;

    public static void ResetBonuses()
    {
        _moneyBonus = 1;
        _scoreBonus = 1;
        _speedImprove = 1;
        _jumpImprove = 1;
    }
}
