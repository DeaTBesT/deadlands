using System.Collections.Generic;
using DL.Data.Resource;

namespace DL.UtilsRuntime
{
    public static class ResourceListSorter
    {
        public static void SortResources(this List<ResourceDataModel> dataList)
        {
            dataList.Sort((x1, x2) =>
            {
                if (x1.ResourceConfig.SortPriority < x2.ResourceConfig.SortPriority)
                {
                    return -1;
                }

                return 1;
            });
        }
    }
}