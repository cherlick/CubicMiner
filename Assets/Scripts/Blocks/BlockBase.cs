using System;
using UnityEngine;
using HealthCareSystem;

public class BlockBase : MonoBehaviour
{
    [SerializeField] private ScriptableBlock _blockData;
    [SerializeField] private GameObject _healthBarPrefab;
    public static Action<ScriptableBlock> OnBreakBlock;
    private SpriteRenderer _spriteRenderer;
    private HealthSystem _healthSystem;
    private bool _isUnbreakable;

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        
    }
    public void LoadBlockData(ScriptableBlock blockData)
    {
        _blockData = blockData;
        if (_blockData == null) return;

        name = _blockData.blockName;
        _spriteRenderer.sprite = _blockData.displaySprites;

        if (_blockData.health>0)
        {
            if(_healthSystem==null){
                GameObject hpBarUI = Instantiate(_healthBarPrefab);
                hpBarUI.transform.position = transform.position;
                hpBarUI.transform.SetParent(transform);
                _healthSystem = hpBarUI.GetComponent<HealthSystem>();
            }
                
            //Debug.Log(_blockData.health+"HP");
            _healthSystem.HealthSystemSetup(_blockData.health);
        }
        
    }

    private void Update()
    {
        if (_healthSystem?.IsAlive==false)
        {
            OnBreakBlock?.Invoke(_blockData);
            gameObject.SetActive(false);
        }

    }
    
    private void OnDisable() {
        
    }
    
    
}
