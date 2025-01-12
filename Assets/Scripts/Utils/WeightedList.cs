using System;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class WeightedList<T>
{
    [System.Serializable]
    public class WeightedItem
    {
        public T Item;
        public int Weight;

        public WeightedItem(T item, int weight)
        {
            Item = item;
            Weight = weight;
        }
    }

    [SerializeField] private List<WeightedItem> items = new List<WeightedItem>();
    private int totalWeight = 0;

    /// <summary>
    /// Добавляет элемент в список с указанным весом.
    /// </summary>
    /// <param name="item">Объект типа T.</param>
    /// <param name="weight">Вес объекта (влияет на вероятность его выпадения).</param>
    public void Add(T item, int weight)
    {
        if (weight <= 0)
        {
            throw new ArgumentException("Weight must be greater than zero.", nameof(weight));
        }

        items.Add(new WeightedItem(item, weight));
        totalWeight += weight;
    }

    /// <summary>
    /// Возвращает случайный элемент из списка на основе его веса.
    /// </summary>
    /// <returns>Случайный объект типа T.</returns>
    public T GetRandomItem()
    {
        if (items.Count == 0)
        {
            throw new InvalidOperationException("The list is empty.");
        }

        var randomWeight = UnityEngine.Random.Range(0, totalWeight);

        foreach (var weightedItem in items)
        {
            if (randomWeight < weightedItem.Weight)
            {
                return weightedItem.Item;
            }

            randomWeight -= weightedItem.Weight;
        }

        throw new InvalidOperationException("Failed to select an item. This should never happen.");
    }
}
