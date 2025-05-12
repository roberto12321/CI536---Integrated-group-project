using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ChangeScene(int scene)
    {
        SceneManager.LoadScene(scene);
        print("Current coins" + SoundFXManager.coins); 
        SoundFXManager.coins = 0;
        SoundFXManager.coinCollected = false;
        print("Current coins" + SoundFXManager.coins);
    }
    public void QuitGame()
    {
        Application.Quit();
    }

}
