using UnityEngine;
using System; // Добавлено для работы с Array.Find

[System.Serializable]
public class Audio
{
    public string audioName;
    public AudioClip audioClip; 
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    
    public Audio[] SFXclip;
    public Audio[] Musicclip;

    // Источники звука, которые будут управлять воспроизведением
    public AudioSource musicSource;
    public AudioSource sfxSource;

    private void Awake()
    {
        // Проверяем, существует ли уже экземпляр
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Уничтожаем дубликат
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject); 

        // Автоматически создаем и настраиваем AudioSource компоненты
        
        // Музыка обычно должна зацикливаться
        musicSource.loop = true; 
    }

    public void playSFX(string name)
    {
        // Ищем нужный аудиоклип в массиве SFX по имени
        Audio s = Array.Find(SFXclip, x => x.audioName == name);

        if (s != null)
        {
            // PlayOneShot позволяет звукам эффектов накладываться друг на друга
            sfxSource.PlayOneShot(s.audioClip);
        }
        else
        {
            Debug.LogWarning("SFX no: " + name);
        }
    }

    public void playMusic(string name)
    {
        // Ищем нужный аудиоклип в массиве Music по имени
        Audio s = Array.Find(Musicclip, x => x.audioName == name);

        if (s != null)
        {
            // Если эта музыка уже играет, ничего не делаем
            if (musicSource.clip == s.audioClip && musicSource.isPlaying) return;

            musicSource.clip = s.audioClip;
            musicSource.Play();
        }
        else
        {
            Debug.LogWarning("no music: " + name);
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }
}
