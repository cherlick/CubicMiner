using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HealthCareSystem;

public class BlockBase : MonoBehaviour
{
    [SerializeField] private ScriptableBlock _blockData;
    [SerializeField] private GameObject _healthBarPrefab;
    private SpriteRenderer _spriteRenderer;
    private HealthSystem _healthSystem;

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    private void OnEnable() {
        //LoadBlockData();
    }
    public void LoadBlockData(ScriptableBlock blockData)
    {
        _blockData = blockData;
        if (_blockData == null) return;

        name = _blockData.blockName;
        _spriteRenderer.sprite = _blockData.displaySprites;

        GameObject hpBarUI = Instantiate(_healthBarPrefab);
        hpBarUI.transform.position = transform.position;
        hpBarUI.transform.SetParent(transform);
        _healthSystem = hpBarUI.GetComponent<HealthSystem>();
        Debug.Log(_blockData.health+"HP");
        _healthSystem.HealthSystemSetup(_blockData.health);
    }

    private void Update()
    {
        if (!_healthSystem.IsAlive)
        {
            /*
            Call event GainScore and GainCoins
            Destroy/Disable Block
            */
            gameObject.SetActive(false);
        }
        

}
    
    
    
}
