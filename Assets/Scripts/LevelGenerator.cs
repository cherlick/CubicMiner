using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PoolingSystem;
using System;
public class LevelGenerator : MonoBehaviour
{
    private ObjectPooling _pooling;

    public static System.Random rnd = new System.Random();
    [Tooltip("Block Types uses to populate block object")]
    [SerializeField] private List<TypesProperties> _blockTypes = default;
    [SerializeField] private GameObject _endStage;

    [Tooltip("Value of the gap between blocks")]
    [SerializeField] private float _positionGap = 0.5f;

    [Tooltip("Position where the 1st block will appear. Moving then to right and top")]
    [SerializeField] private Vector2 _startPosition = default;

    [Tooltip("Value for the vertical size")]
    [SerializeField] private int _verticalSize = 18;
    [Tooltip("Value for the horizontal size")]
    [SerializeField] private int _horizontalSize = 7;
    
    

    private void Start() {
        _pooling = ObjectPooling.Instance;
        GenerateBlocks();
    }

    public void GenerateBlocks()
    {
        Vector2 newPosition = _startPosition;
        for (var i = 0; i < _verticalSize; i++)
        {
            newPosition.x = _startPosition.x;
            for (var h = 0; h < _horizontalSize; h++)
            {
                ScriptableBlock newBlock = SellectBlockType(_blockTypes);
                GameObject obj = _pooling.GetObjectOnStartPool("Block");
                obj.GetComponent<BlockBase>().LoadBlockData(newBlock);
                obj.transform.position = newPosition;
                obj.SetActive(true);
                newPosition.x += _positionGap;


            }
            newPosition.y += _positionGap;
        }
        newPosition.x = _startPosition.x+1.5f;
        GameObject endStage = Instantiate(_endStage, transform);
        endStage.transform.position = newPosition;
        endStage.SetActive(true);
    }

    public ScriptableBlock SellectBlockType(List<TypesProperties> items)
    {
        // Calculate the sum of all portions.
        float poolSize = 0;
        for (int i = 0; i < items.Count; i++)
        {
            poolSize += items[i].dropChance;
        }

        // Get a random integer from 0 to PoolSize.
        int randomNumber = rnd.Next(0, (int)poolSize) + 1;

        // Detect the item, which corresponds to current random number.
        float accumulatedProbability = 0;
        for (int i = 0; i < items.Count; i++)
        {
            accumulatedProbability += items[i].dropChance;
            //Debug.Log(randomNumber + " Dice "+ accumulatedProbability+" prob "+ items[i].blockType+"Type ");
            if (randomNumber <= accumulatedProbability)
                return items[i].blockType;
        }
        return null;    // this code will never come while you use this programm right :)
    }
}

[Serializable]
public class TypesProperties
{
    public ScriptableBlock blockType;
    public float dropChance;

}
