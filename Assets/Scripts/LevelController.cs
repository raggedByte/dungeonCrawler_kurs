using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    [SerializeField] private string nextLevel;
    public void NextLevel()
    {
        SceneManager.LoadScene(nextLevel);
    }
}