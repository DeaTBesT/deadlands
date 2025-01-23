using System;
using System.Collections.Generic;
using UnityEngine;

namespace DL.UtilsRuntime
{
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
        /// ��������� ������� � ������ � ��������� �����.
        /// </summary>
        /// <param name="item">������ ���� T.</param>
        /// <param name="weight">��� ������� (������ �� ����������� ��� ���������).</param>
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
        /// ���������� ��������� ������� �� ������ �� ������ ��� ����.
        /// </summary>
        /// <returns>��������� ������ ���� T.</returns>
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
}