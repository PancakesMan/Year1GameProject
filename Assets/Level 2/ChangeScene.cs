using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour {]

	public void ChangeToScene(string sceneName)
    {
		SceneManager.LoadScene (sceneName);
	}

	public void ResetLevel ()
    {
		SceneManager.LoadScene (SceneManager.GetActiveScene().name);
	}

	public void QuitGame ()
    {
		Application.Quit ();
	}
}
