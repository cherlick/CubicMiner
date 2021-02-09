using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StagesEnding : MonoBehaviour
{
    [SerializeField] private List<GameObject> _differentEndings = default;
    [SerializeField] private TextMeshProUGUI _stageText = null;
    private int _randomSelected = 0;

    private void OnEnable() {
        _randomSelected = Random.Range(0, _differentEndings.Count);
        _differentEndings[_randomSelected].SetActive(true);
        _stageText.text = "Stage " + LevelManager.instance?.GetCurrentStage;
    }
    private void OnDisable() {
        foreach (var endings in _differentEndings){_differentEndings[_randomSelected].SetActive(false); }
    }
}
