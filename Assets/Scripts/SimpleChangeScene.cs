using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Utils
{
    public class SimpleChangeScene : MonoBehaviour
    {
        public static void Load(string sceneName) {
            SceneManager.LoadScene(sceneName);
        }
        public static void Load(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }
    }

}