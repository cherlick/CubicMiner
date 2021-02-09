using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using PoolingSystem;

public class LevelManager : Singleton<LevelManager>
{
    private int _stageLevel = 0;
    public int GetCurrentStage => _stageLevel=1;
    private ObjectPooling _pooling = null;

    [Tooltip("Properties for the stages, blocks and there generation change")]
    [SerializeField] private List<StagesProperties> _stagesProperties = null;

    [Tooltip("Position where the 1st block will appear. Moving then to right and top")]
    [SerializeField] private Vector2 _startPosition = default;

    [Tooltip("Value of the gap between blocks")]
    [SerializeField] private float _spacementInGrid = 0.5f;

    [Tooltip("Value for the grid size")]
    [SerializeField] private Vector2 grid = new Vector2(7,14);

    private Dictionary<int, List<GameObject>> _previousStages;


    private void Start() {
        _pooling = ObjectPooling.Instance;
        StageGenerator();
    }
    private void StageGenerator(){
        CreateGrid();
        _stageLevel++;
        CreateStageGate();
        //Generate full stage
    }

    private void CreateStageGate()
    {
        GameObject stageGate = _pooling?.GetObjectOnStartPool("StageGate");
        stageGate.transform.position = new Vector2(_startPosition.x, _startPosition.y + grid.y*_spacementInGrid);
        stageGate.SetActive(true);
    }

    private void StageCleaner(){
        if (_stageLevel-2>0)
        {
            List <GameObject> tempBlocks = null;
            if(_previousStages.TryGetValue(_stageLevel-2, out tempBlocks))
            {
                foreach (var block in tempBlocks)
                    _pooling.ReturnObject(block, "Block");     
            }
            
        }
        
    }

    private void CreateGrid(){
        for (var i = 0; i < grid.y; i++)
            GenerateRow(_startPosition.y+_spacementInGrid*i);
    }
    private void GenerateRow(float yPosition){
        Vector2 nextPosition = new Vector2(_startPosition.x, yPosition);
        for (var h = 0; h < grid.x; h++)
        {
            BlockBase blockPooled = _pooling.GetObjectOnStartPool("Block").GetComponent<BlockBase>();
            blockPooled.LoadBlockData(BlocksSelector.SellectBlockType(_stagesProperties[_stageLevel].blocksProperties));

            blockPooled.transform.position = nextPosition;
            blockPooled.gameObject.SetActive(true);
            nextPosition.x += _spacementInGrid;
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

