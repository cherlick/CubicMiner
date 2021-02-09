using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocksSelector
{   
    public static System.Random rnd = new System.Random();
    public static ScriptableBlock SellectBlockType(List<BlocksTypesProperties> blocksProperties)
    {
        // Calculate the sum of all portions.
        float poolSize = 0;
        for (int i = 0; i < blocksProperties.Count; i++)
        {
            poolSize += blocksProperties[i].dropChance;
        }

        // Get a random integer from 0 to PoolSize.
        int randomNumber = rnd.Next(0, (int)poolSize) + 1;

        // Detect the item, which corresponds to current random number.
        float accumulatedProbability = 0;
        for (int i = 0; i < blocksProperties.Count; i++)
        {
            accumulatedProbability += blocksProperties[i].dropChance;
            //Debug.Log(randomNumber + " Dice "+ accumulatedProbability+" prob "+ items[i].blockType+"Type ");
            if (randomNumber <= accumulatedProbability)
                return blocksProperties[i].blockType;
        }
        return null;    // this code will never come while you use this programm right :)
    }
}
