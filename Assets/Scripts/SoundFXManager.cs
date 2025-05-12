using UnityEngine;
using TMPro;



public class SoundFXManager : MonoBehaviour
{

    public static int soundVolume;
    public static int coins;
    public static bool coinCollected;

    public bool winScreen;
    public TextMeshProUGUI coinText;
    public static SoundFXManager instance;
    [SerializeField] private AudioSource soundObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        if (winScreen)
        {
            coinText.text = "You win! \n" + coins.ToString() + "/3 coins found!";
        }
    }

    public void PlaySoundFXClip(AudioClip audioClip, Transform spawnTransform, float volume)
    {
        //Instantiate object
        AudioSource audioSource = Instantiate(soundObject, spawnTransform.transform.position, Quaternion.identity); 

        //Assign audio clip to object
        audioSource.clip = audioClip;

        audioSource.volume = volume;

        audioSource.Play();

        float clipLength = audioSource.clip.length;

        Destroy(audioSource.gameObject, clipLength);

    }
}
