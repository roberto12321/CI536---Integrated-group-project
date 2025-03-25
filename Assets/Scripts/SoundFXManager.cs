using UnityEngine;

public class SoundFXManager : MonoBehaviour
{ 


    public static SoundFXManager instance;
    [SerializeField] private AudioSource soundObject;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
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
