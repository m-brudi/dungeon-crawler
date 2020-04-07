using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericLootDropTable<T, U> where T : GenericLootDropItem<U>
{
    [SerializeField]
    public List<T> lootDropItems;

    float probabilityTotalWeight;

    public void ValidateTable()
    {
        if (lootDropItems != null && lootDropItems.Count > 0)
        {
            float currentProbabilityWeightMaximum = 0f;

            foreach(T lootDropItem in lootDropItems)
            {
                if(lootDropItem.probabilityWeight < 0f)
                {
                    lootDropItem.probabilityWeight = 0f;
                }
                else
                {
                    lootDropItem.probabilityRangeFrom = currentProbabilityWeightMaximum;
                    currentProbabilityWeightMaximum += lootDropItem.probabilityWeight;
                    lootDropItem.probabilityRangeTo = currentProbabilityWeightMaximum;
                }
            }

            probabilityTotalWeight = currentProbabilityWeightMaximum;

            foreach(T lootDropItem in lootDropItems)
            {
                lootDropItem.probabilityPercent = ((lootDropItem.probabilityWeight) / probabilityTotalWeight) * 100;
            }

        }
    }

    public T PickLootDropItem()
    {
        float pickedNumber = Random.Range(0, probabilityTotalWeight);

        foreach(T lootDropItem in lootDropItems)
        {
            if(pickedNumber > lootDropItem.probabilityRangeFrom && pickedNumber < lootDropItem.probabilityRangeTo)
            {
                return lootDropItem;
            }
        }
        return lootDropItems[0];
    }


}
