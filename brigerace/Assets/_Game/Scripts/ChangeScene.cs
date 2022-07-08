#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.SceneManagement;


public class ChangeScene : Editor {

    [MenuItem("Open Scene/Game #1")]
    public static void OpenLoading()
    {
        OpenScene("Game");
    }

   

    private static void OpenScene (string sceneName) {
		if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo ()) {
			EditorSceneManager.OpenScene ("Assets/_Game/Scene/" + sceneName + ".unity");
		}
	}
}
#endif