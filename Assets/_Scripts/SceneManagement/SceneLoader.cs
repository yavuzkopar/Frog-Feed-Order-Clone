using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace FrogFeedOrder.SceneManagement
{
    public class SceneLoader : MonoBehaviour
    {
        //called by button
        public void LoadNextScene()
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
            SceneManager.LoadSceneAsync(nextSceneIndex);
        }
        //called by button
        public void RestartScene()
        {
            int nextSceneIndex = SceneManager.GetActiveScene().buildIndex;
            SceneManager.LoadSceneAsync(nextSceneIndex);
        }
    }
}
