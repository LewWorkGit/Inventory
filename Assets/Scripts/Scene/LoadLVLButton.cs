using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLVLButton : MonoBehaviour
{
    [SerializeField] private string nameLVL;
    public void LoadScene()
    {
        SceneManager.LoadScene(nameLVL);
    }
}
