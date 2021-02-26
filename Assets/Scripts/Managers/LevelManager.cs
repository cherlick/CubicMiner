using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PoolingSystem;

public class LevelManager : Singleton<LevelManager>
{
    [SerializeField] private int _stageLevel = 0;
    public int GetCurrentStage => _stageLevel;
    private ObjectPooling _pooling = null;

    [Tooltip("Properties for the stages, blocks and there generation change")]
    [SerializeField] private List<StagesProperties> _stagesProperties = new List<StagesProperties>();

    [Tooltip("Position where the 1st block will appear. Moving then to right and top")]
    [SerializeField] private Vector2 _startPosition = default;

    [Tooltip("Value of the gap between blocks")]
    [SerializeField] private float _spacementInGrid = 0.5f;

    [Tooltip("Value for the grid size")]
    [SerializeField] private Vector2 _grid = new Vector2(7,14);
    private int _stageDifficulty = 0;


    [SerializeField] private Dictionary<int, List<GameObject>> _stagesCreated = new Dictionary<int, List<GameObject>>();

    private void OnEnable() {
        StageTrigger.onStageTrigger += OnTriggerRequest;
    }
    private void OnDisable() {   
        StageTrigger.onStageTrigger -= OnTriggerRequest;
    }
    private void Start() {
        _pooling = ObjectPooling.Instance;
        StageGenerator();
    }

    private void OnTriggerRequest(float notUsed) {
        _stageLevel++;
        Debug.Log(_stageDifficulty+" - "+_stagesProperties.Count);
        if(_stageLevel%2==0 && _stagesProperties.Count-1>_stageDifficulty)
            _stageDifficulty++;
        StageGenerator();
        
    }
    private void StageGenerator(){
        
        CreateGrid();
        CreateStageGate();
        StageCleaner();
        _startPosition.y += _spacementInGrid * (_grid.y+2);
    }

    private void CreateStageGate()
    {
        GameObject stageGate = _pooling?.GetObjectOnStartPool("StageGate");
        if (stageGate==null) return;

        stageGate.SetActive(false);
        stageGate.transform.position = new Vector2(_startPosition.x, _startPosition.y + _grid.y*_spacementInGrid);
        stageGate.SetActive(true);
        _pooling.ReturnObject(stageGate,"StageGate");
    }

    private void StageCleaner(){
        if (_stagesCreated.Count>3)
        {
            List <GameObject> tempBlocks = null;
            if(_stagesCreated.TryGetValue(_stageLevel-3, out tempBlocks))
            {
                foreach (var block in tempBlocks)
                    _pooling.ResetObject(block, "Block"); 

            }
            _stagesCreated.Remove(_stageLevel-2);
            

        }
        
    }

    private void CreateGrid(){
        List <GameObject> tempBlocks = new List<GameObject>();
        try
        {
            for (var i = 0; i < _grid.y; i++)
            {
                Vector2 nextPosition = new Vector2(_startPosition.x, _startPosition.y+_spacementInGrid*i);
                
                for (var h = 0; h < _grid.x; h++)
                {
                    BlockBase blockPooled = _pooling.GetObjectOnStartPool("Block").GetComponent<BlockBase>();

                    
                    Debug.Log(_stageDifficulty+" _stageDifficulty");
                    blockPooled.LoadBlockData(BlocksSelector.SellectBlockType(_stagesProperties[_stageDifficulty].blocksProperties));

                    blockPooled.transform.position = nextPosition;
                    blockPooled.gameObject.SetActive(true);
                    nextPosition.x += _spacementInGrid;
                    tempBlocks.Add(blockPooled.gameObject);
                }
            }
            _stagesCreated[_stageLevel]=tempBlocks;
            }
        catch (System.Exception ex)
        {
             Debug.LogWarning(ex);
        }
        
    }

    
}

[Serializable]
public class BlocksTypesProperties
{
    public ScriptableBlock blockType;
    public float dropChance;
    public int limitPerRow;

}

[Serializable]
public class StagesProperties
{
    public string name;
    public List<BlocksTypesProperties> blocksProperties;
}

