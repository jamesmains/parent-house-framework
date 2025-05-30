using System.Collections.Generic;

namespace parent_house_framework.Utils {
    public class AssetManagementTool {
        public static List<T> GetScriptableObjectCollection<T>() where T : SerializedManagedScriptableObject {
            return AssetManagementUtil.GetAllScriptableObjectInstances<T>();
        }
    }
}