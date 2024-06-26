using UnityEngine;
using UnityEngine.UI;

public class SoundController : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private Slider _slider;
    [SerializeField] private Text _text;

    [Header("Parameters")]
    [SerializeField] private string _saveVolumeKey;
    [SerializeField] private string _sliderTag;
    [SerializeField] private string _textVolumeTag;
    [SerializeField] private float _volume;


    private void Awake()
    {
        GameObject sliderObject = GameObject.FindWithTag(_sliderTag);

        if (sliderObject != null)
        {
            _slider = sliderObject.GetComponent<Slider>();
            _slider.value = PlayerPrefs.GetFloat(_saveVolumeKey, 100); 
        }
    }

    private void LateUpdate()
    {
        GameObject sliderObject = GameObject.FindWithTag(_sliderTag);
        if (sliderObject != null)
        {
            _slider = sliderObject.GetComponent<Slider>();
            _volume = _slider.value;

            GameObject textObject = GameObject.FindWithTag(_textVolumeTag);
            if (textObject != null)
            {
                _text.text = Mathf.Round(_volume * 100) + "%";
            }
           UpdateVolume();
        }
    }

    public void UpdateVolume()
    {   
        _audioSource.volume = _volume;
        PlayerPrefs.SetFloat(_saveVolumeKey, _volume);       
    }
}
