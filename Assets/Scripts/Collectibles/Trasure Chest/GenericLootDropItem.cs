using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GenericLootDropItem<T>
{
    public T item;

    public float probabilityWeight;

    public float probabilityPercent;

    [HideInInspector]
    public float probabilityRangeFrom;
    [HideInInspector]
    public float probabilityRangeTo;
}
