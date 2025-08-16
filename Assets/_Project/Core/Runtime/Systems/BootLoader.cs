using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CardPackBattler.Core
{
    /// <summary>
    /// Place this on a GameObject in your Boot scene.
    /// At runtime it loads the listed scenes additively (e.g., Board, UI, DraftPhase).
    /// Ensure these scenes are added to Build Settings.
    /// </summary>
    [DefaultExecutionOrder(-1000)]
    public class BootLoader : MonoBehaviour
    {
        [Tooltip("Scenes to load additively after Boot. Must be in Build Settings.")]
        [SerializeField] private List<string> sceneNamesToLoad = new List<string>
        {
            "Board",
            "UI",
            "DraftPhase"
        };

        [Tooltip("Set the last loaded scene as the active scene so new objects spawn there.")]
        [SerializeField] private bool setLastLoadedAsActive = true;

        private void Start() => StartCoroutine(LoadRoutine());

        private IEnumerator LoadRoutine()
        {
            Scene last = gameObject.scene;

            foreach (var sceneName in sceneNamesToLoad)
            {
                if (string.IsNullOrWhiteSpace(sceneName)) continue;

                if (!IsLoaded(sceneName))
                {
                    var op = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);
                    while (!op.isDone) yield return null;
                    last = SceneManager.GetSceneByName(sceneName);
                }
            }

            if (setLastLoadedAsActive && last.IsValid())
            {
                SceneManager.SetActiveScene(last);
            }

            // Optionally unload Boot after everything is up:
            // yield return SceneManager.UnloadSceneAsync(gameObject.scene);
        }

        private static bool IsLoaded(string sceneName)
        {
            for (int i = 0; i < SceneManager.sceneCount; i++)
            {
                var s = SceneManager.GetSceneAt(i);
                if (s.name == sceneName && s.isLoaded) return true;
            }
            return false;
        }
    }
}
