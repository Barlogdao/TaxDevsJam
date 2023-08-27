using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    [SerializeField] private int levelIndex;
    IEnumerator Start()
    {
        yield return new WaitForSeconds(0.4f);
        SceneManager.LoadScene(levelIndex);

    }


}
