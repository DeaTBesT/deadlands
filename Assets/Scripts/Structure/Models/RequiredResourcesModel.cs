using System.Collections.Generic;
using DL.Data.Resource;

namespace DL.StructureRuntime.Model
{
    [System.Serializable]
    public class RequiredResourcesModel
    {
        public List<ResourceDataModel> RequiredResources;
        public int Level;

        public RequiredResourcesModel()
        {
            
        }

        public RequiredResourcesModel(List<ResourceDataModel> requiredResources, int level)
        {
            RequiredResources = requiredResources;
            Level = level;
        }
    }
}