using System.Collections.Generic;
using GameResources.Core;

namespace Utils
{
    public static class ResourceListSorter
    {
        public static void SortResources(this List<ResourceData> dataList)
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