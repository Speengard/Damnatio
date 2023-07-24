using UnityEngine;
using System.Collections;

public class MusicManager : MonoBehaviour
{
    public AudioSource sfxSource;
    public AudioSource ostSource;

    public void ToggleOST() {
        ostSource.enabled = !ostSource.enabled;
    }

    public void ToggleSFX() {
        sfxSource.enabled = !sfxSource.enabled;
    }
}