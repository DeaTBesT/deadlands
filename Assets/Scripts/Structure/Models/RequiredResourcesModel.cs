using System.Collections.Generic;
using DL.Data.Resource;

namespace DL.StructureRuntime.Model
{
    [System.Serializable]
    public class RequiredResourcesModel
    {
        public List<ResourceDataModel> RequiredResources;
        public int Level;
    }
}