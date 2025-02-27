using UnityEngine;

public class MainMenuBG : MonoBehaviour
{
    [Header("Audio Source")]
    [SerializeField] AudioSource musicSource;

    [Header("Audio Clip")]
    public AudioClip Menubackground;

    void Awake()
    {
        if (this == null)
        {

            DontDestroyOnLoad(gameObject); // ทำให้ AudioManager อยู่ข้าม Scene
            musicSource.clip = Menubackground;
            musicSource.Play();
        }
        else
        {
            Destroy(gameObject); // ถ้ามีตัวอื่นอยู่แล้ว ให้ทำลายตัวใหม่
        }
    }
}
