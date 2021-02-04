using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StagesEnding : MonoBehaviour
{
    [SerializeField] private List<GameObject> _differentEndings;

    private void OnEnable() {
        int rnd = Random.Range(0, _differentEndings.Count);
        _differentEndings[rnd].SetActive(true);
    }
    private void OnDisable() {
        foreach (var endings in _differentEndings){ }
    }
}
