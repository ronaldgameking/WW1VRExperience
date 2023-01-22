using UnityEngine;
using UnityEngine.SceneManagement;

public class CutSceneTransition : MonoBehaviour
{
    [SerializeField] private int endingLocation = 1;
    public void SwitchToEnding()
    {
        SceneManager.LoadScene(endingLocation);
    }
}
