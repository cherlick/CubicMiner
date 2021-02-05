using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour {
    [SerializeField] private TextMeshProUGUI _scoreTxt;
     private void OnEnable() {
        ScoreManager.OnUpdateScore += UpdateScoreUI;
        UpdateScoreUI(0);
    }
     private void OnDisable() {
         ScoreManager.OnUpdateScore -= UpdateScoreUI;
     }

     private void UpdateScoreUI(float score){
        _scoreTxt.text =score.ToString();
    }
}