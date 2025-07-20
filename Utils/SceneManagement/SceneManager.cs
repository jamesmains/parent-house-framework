using System.Collections;
using parent_house_framework.Interactions;
using Sirenix.OdinInspector;
using UnityEngine;

namespace parent_house_framework.Utils.SceneManagement {
    public class SceneManager : MonoBehaviour {
        [SerializeField, FoldoutGroup("Debug")]
        private Trigger SceneStateTrigger;

        [SerializeField, FoldoutGroup("Debug")]
        private string DebugTargetScene;

        [SerializeField, FoldoutGroup("Status"), ReadOnly]
        private string TargetScene;

        public static SceneManager Singleton;

        private void Awake() {
            if (Singleton != null) {
                Destroy(gameObject);
            }
            else {
                Singleton = this;
                DontDestroyOnLoad(gameObject);
            }
        }

        [Button]
        public void Debug_SetTargetScene() {
            TargetScene = DebugTargetScene;
        }

        [Button]
        public void GotoScene() {
            StartCoroutine(LoadScene());
        }

        IEnumerator LoadScene() {
            SceneStateTrigger.OnChangeState.Invoke(true, null);
            yield return new WaitForSeconds(1f);
            AsyncOperation asyncLoad = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(TargetScene);
            if (asyncLoad == null) {
                Debug.LogError($"No scene to load: {TargetScene}");
                yield return null;
            }
            else {
                // Wait until the asynchronous scene fully loads
                while (!asyncLoad.isDone) {
                    yield return null;
                    
                }
                yield return new WaitForSeconds(1f);
                SceneStateTrigger.OnChangeState.Invoke(false, null);
            }
        }
    }
}