using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Animations
{
    public static class LootDropAnimation
    {
        public static void AnimateItemDrop(this GameObject item, Vector3 startPosition, float dropRadius, float dropHeight,
            float animationDuration, Action onComplete = null)
        {
            item.transform.position = startPosition;

            var randomOffset = new Vector3(
                Random.Range(-dropRadius, dropRadius),
                0,
                Random.Range(-dropRadius, dropRadius)
            );

            var endPosition = startPosition + randomOffset;

            // Вычисляем контрольную точку для кривой (с подъёмом вверх)
            var controlPoint = (startPosition + endPosition) / 2 + Vector3.up * dropHeight;

            var sequence = DOTween.Sequence();
            sequence.Append(item.transform
                .DOPath(new Vector3[] { startPosition, controlPoint, endPosition }, animationDuration,
                    PathType.CatmullRom)
                .SetEase(Ease.OutQuad));
            sequence.Join(item.transform.DORotate(new Vector3(0, 360, 0), animationDuration, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear));
            
            sequence.OnComplete(() => onComplete?.Invoke());
            sequence.SetAutoKill(true);
            
            sequence.Play();
        }
    }
}