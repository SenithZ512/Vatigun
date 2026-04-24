using UnityEngine;

public static class SoundManger 
{
  public static void PlaySoundNow(this AudioSource adio)
    {
        adio.Play();
    }
    public static void PlayOneshotNow(this AudioSource adio,AudioClip clip)
    {
        adio.PlayOneShot(clip);
    }
}
