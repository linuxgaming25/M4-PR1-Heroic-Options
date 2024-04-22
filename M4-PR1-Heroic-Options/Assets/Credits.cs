using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Credits : MonoBehaviour
{
    public float restartDelay = 1f;

    public void RestartButton()
    {
        Debug.Log("RESTART");
        Invoke("Restart", restartDelay);
    }

    void Restart ()
	{
		SceneManager.LoadScene("Level01");
	}
}
