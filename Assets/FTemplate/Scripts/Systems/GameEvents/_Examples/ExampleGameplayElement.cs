
using UnityEngine;

public class ExampleGameplayElement : MonoBehaviour
{
    public void OnGameStarted(Void data)
    {
        Debug.Log("Example Gameplay Element acknowleged game start.");
    }
    
    public void OnGameFinished(bool data)
    {
        Debug.Log($"Example Gameplay Element acknowleged game finished. {data}");
    }
}