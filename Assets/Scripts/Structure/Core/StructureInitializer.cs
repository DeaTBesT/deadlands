using DL.CoreRuntime;
using UnityEngine;

namespace DL.StructureRuntime.Core
{
    public abstract class StructureInitializer : EntityInitializer
    {
        [SerializeField] protected InteractableStructure _interactableStructure;
    }
}