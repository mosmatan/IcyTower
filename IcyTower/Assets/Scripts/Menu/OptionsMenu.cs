using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Manages the options menu, allowing adjustment of audio settings.
/// </summary>
public class OptionsMenu : MonoBehaviour 
{
    [SerializeField] private Scrollbar audioScrollbar; // Scrollbar for adjusting audio volume.

    private void Start()
    {
        audioScrollbar.value = GameManager.Instance.AudioVolume; // Set scrollbar value to current audio volume.
    }

    public void AudioScrollValueChanged()
    {
        GameManager.Instance.AudioVolume = audioScrollbar.value; // Update audio volume based on scrollbar value.
    }

    public void Return()
    {
        GameManager.Instance.SceneMenu.isActive = true; // Activate the main menu.
        gameObject.SetActive(false); // Deactivate the options menu.
    }
}