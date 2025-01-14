using System.Collections.Generic;
using GameResources.Core;

namespace Place.Models
{
    [System.Serializable]
    public class ResourcesUpgradeModel
    {
        public List<ResourceData> RequiredResources;
        public int Level;
    }
}