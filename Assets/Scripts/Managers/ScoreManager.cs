using System;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : Singleton<ScoreManager>
{
    public static Action<float> OnUpdateScore;
    private float _currentScore;
    public float GetScore => _currentScore;
    private void OnEnable() {
        BlockBase.OnBreakBlock += IncreaseScore;
    }
    private void OnDisable() {
         BlockBase.OnBreakBlock -= IncreaseScore;
    }

    private void IncreaseScore(ScriptableBlock data)
    {
        _currentScore += data.scoreValue;
        OnUpdateScore?.Invoke(_currentScore);
    }

    
}
