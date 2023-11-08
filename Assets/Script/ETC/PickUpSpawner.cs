using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpSpawner : MonoBehaviour
{
    [SerializeField] private GameObject goldCoinPrefab,healthGlobe,staminaGlobe;

    public void DropItem(int typeEnermy)
    {
        //0 = slime , 1 = Ghost ,2 = Grape
        if(typeEnermy == 0)
        {
            int randAmountGold = Random.Range(1, 3);

            for(int i = 0; i < randAmountGold; i++)
            {
                Instantiate(goldCoinPrefab, transform.position, Quaternion.identity);
            }
            
        }

        if(typeEnermy == 1)
        {
            int randNum = Random.Range(1, 4);
            int randAmountGold = Random.Range(2,5);
            if (randNum == 1)
            {
                for (int i = 0; i < randAmountGold; i++)
                {
                    Instantiate(goldCoinPrefab, transform.position, Quaternion.identity);
                }

            }
            if (randNum == 2)
            {
                Instantiate(healthGlobe, transform.position, Quaternion.identity);
            }
            if (randNum == 3)
            {
                Instantiate(staminaGlobe, transform.position, Quaternion.identity);
            }

        }
        if(typeEnermy == 2)
        {
            int AmountGold = 2;
            for (int i = 0; i < AmountGold; i++)
            {
                Instantiate(goldCoinPrefab, transform.position, Quaternion.identity);
            }
            Instantiate(healthGlobe, transform.position, Quaternion.identity);
        }
        if (typeEnermy == 5)
        {
            int randNum = Random.Range(1,4);
            int randType = Random.Range(1, 3);
            if (randNum == 1 || randNum == 2)
            {
                if (randType == 1)
                {
                    Instantiate(goldCoinPrefab, transform.position, Quaternion.identity);
                }
                else
                {
                    Instantiate(healthGlobe, transform.position, Quaternion.identity);
                }
            }
        }

    }

    

}
