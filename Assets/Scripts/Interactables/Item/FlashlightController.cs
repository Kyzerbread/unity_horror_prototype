using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    [SerializeField] AudioClip _onSFX;
    [SerializeField] AudioClip _offSFX;
    [SerializeField] KeyCode _toggleKey;

    private GameObject _cameraObject;
    private GameObject _lightSource;
    public AudioSource PlayerAudioSource;
    private Vector3 _offset;

    public bool IsOn { get; private set; }
    private readonly float _speed = 5f;

    public bool IsEnabled = true;

    private void Awake()
    {
        _cameraObject = Camera.main.gameObject;
        _lightSource = transform.GetChild(0).gameObject;
    }

    private void Start()
    {
        _lightSource.gameObject.SetActive(false);
        _offset = transform.position - _cameraObject.transform.position;
    }
    private void Update()
    {
        transform.position = _cameraObject.transform.position + _offset;
        transform.rotation = Quaternion.Slerp(transform.rotation, _cameraObject.transform.rotation, _speed * Time.deltaTime);

        if (!IsEnabled)
        {
            _lightSource.gameObject.SetActive(false);
            IsOn = false;
            return;
        }

        if (Input.GetKeyDown(_toggleKey))
        {
            Debug.Log("ON FLASHLIGHT");
            PlayerAudioSource.PlayOneShot(_onSFX);
        }

        if (Input.GetKeyUp(_toggleKey))
        {
            PlayerAudioSource.PlayOneShot(_offSFX);

            if (IsOn == false)
            {
                _lightSource.gameObject.SetActive(true);
                IsOn = true;
            }
            else
            {
                _lightSource.gameObject.SetActive(false);
                IsOn = false;
            }
        }
    }

    public void PlayFlashlightOffSFX()
    {
        PlayerAudioSource.PlayOneShot(_onSFX);
        PlayerAudioSource.PlayDelayed(2f);
        PlayerAudioSource.PlayOneShot(_offSFX);
    }
}
