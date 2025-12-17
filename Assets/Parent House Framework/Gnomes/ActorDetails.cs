using UnityEngine;

namespace Parent_House_Framework.Gnomes {
    [CreateAssetMenu(fileName = "ActorDetails", menuName = "ParentHouse/ActorDetails")]
    public class ActorDetails : ScriptableObject {
        public string ActorName;
        public Sprite ActorSprite;
        
    }
}
