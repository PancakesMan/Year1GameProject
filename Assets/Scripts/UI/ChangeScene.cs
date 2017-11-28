using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
public class ChangeScene : MonoBehaviour {

	public void ChangeToScene(string sceneName)
    {
		SceneManager.LoadScene (sceneName);
        Time.timeScale = 1;
	}

    public void ChangeToScene(int index)
    {
        SceneManager.LoadScene(index);
        Time.timeScale = 1;
    }

	public void ResetLevel ()
    {
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}

	public void QuitGame ()
    {
		Application.Quit ();
        Debug.Log("Player has quit the game");
	}
}
