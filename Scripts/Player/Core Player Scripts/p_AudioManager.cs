using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class p_AudioManager : MonoBehaviour
{
    private AudioSource _audioSource = default;
    private p_movement _pm;


    [Header("References")]
    [SerializeField] private Transform RayOrigin;
    [Header("Values")]
    [SerializeField] private float rayDistance = 3f;
    [SerializeField] private float baseStepSpeed = 0.5f;
    [SerializeField] private float crouchStepMultiplier = 1.5f;

    [Header("Sound Files")]
    [Header("Defaults")]
    [SerializeField] private AudioClip[] defaultClips = default;
    [SerializeField] private AudioClip[] defaultJumpClips = default;
    [SerializeField] private AudioClip[] defaultLandClips = default;
    [Header("Footsteps - All Other")]
    [SerializeField] private AudioClip[] woodClips = default;
    [SerializeField] private AudioClip[] grassClips = default;
    [SerializeField] private AudioClip[] mudClips = default;
    [SerializeField] private AudioClip[] metalClips = default;
    [Header("Jumps")]
    [SerializeField] private AudioClip[] woodJumpClips = default;
    [SerializeField] private AudioClip[] grassJumpClips = default;
    [SerializeField] private AudioClip[] mudJumpClips = default;
    [SerializeField] private AudioClip[] metalJumpClips = default;
    [Header("Lands")]
    [SerializeField] private AudioClip[] woodLandClips = default;
    [SerializeField] private AudioClip[] grassLandClips = default;
    [SerializeField] private AudioClip[] mudLandClips = default;
    [SerializeField] private AudioClip[] metalLandClips = default;


    private void Awake()
    {
        _pm = FindObjectOfType<p_movement>();
        _audioSource = FindObjectOfType<AudioSource>();
    }

    private float footstepTimer = 0f;
    bool isCrouching => _pm.state == p_movement.MovementState.CROUCHING;
    private float GetCurrentOffset => isCrouching ? baseStepSpeed * crouchStepMultiplier : baseStepSpeed;
    public void HandleFootsteps()
    {
        if (_pm.getBoolData("noInput")) return;
        if (!_pm.getBoolData("isGrounded")) return;
        footstepTimer -= Time.deltaTime;

        if (footstepTimer <= 0)
        {
            RaycastHit2D hit = Physics2D.Raycast(RayOrigin.position, Vector2.down, rayDistance);
            switch (hit.collider.tag)//Include new tags by making new cases
            {
                case "Footsteps/Wood":
                    _audioSource.PlayOneShot(woodClips[Random.Range(0, woodClips.Length - 1)]);
                    break;
                case "Footsteps/Dirt":
                    _audioSource.PlayOneShot(mudClips[Random.Range(0, mudClips.Length - 1)]);
                    break;
                case "Footsteps/Grass":
                    _audioSource.PlayOneShot(grassClips[Random.Range(0, grassClips.Length - 1)]);
                    break;
                case "Footsteps/Metal":
                    _audioSource.PlayOneShot(metalClips[Random.Range(0, metalClips.Length - 1)]);
                    break;
                default:
                    _audioSource.PlayOneShot(defaultClips[Random.Range(0, defaultClips.Length - 1)]);
                    break;
            }
            footstepTimer = GetCurrentOffset;
        }
    }
    public void HandleJumpSounds()
    {
        if (!_pm.getBoolData("isGrounded")) return;
        RaycastHit2D hit = Physics2D.Raycast(RayOrigin.position, Vector2.down, rayDistance);
        switch (hit.collider.tag)
        {
            case "Footsteps/Wood":
                _audioSource.PlayOneShot(woodClips[Random.Range(0, woodJumpClips.Length - 1)]);
                break;
            case "Footsteps/Dirt":
                _audioSource.PlayOneShot(mudClips[Random.Range(0, mudJumpClips.Length - 1)]);
                break;
            case "Footsteps/Grass":
                _audioSource.PlayOneShot(grassClips[Random.Range(0, grassJumpClips.Length - 1)]);
                break;
            case "Footsteps/Metal":
                _audioSource.PlayOneShot(metalClips[Random.Range(0, metalJumpClips.Length - 1)]);
                break;
            default:
                _audioSource.PlayOneShot(defaultClips[Random.Range(0, defaultJumpClips.Length - 1)]);
                break;
        }

    }
    public void HandleLandSounds()
    {
        //if (_pm.getBoolData("isGrounded")) return;
        RaycastHit2D hit = Physics2D.Raycast(RayOrigin.position, Vector2.down, rayDistance);

        switch (hit.collider.tag)
        {
            case "Footsteps/Wood":
                _audioSource.PlayOneShot(woodClips[Random.Range(0, woodLandClips.Length - 1)]);
                break;
            case "Footsteps/Dirt":
                _audioSource.PlayOneShot(mudClips[Random.Range(0, mudLandClips.Length - 1)]);
                break;
            case "Footsteps/Grass":
                _audioSource.PlayOneShot(grassClips[Random.Range(0, grassLandClips.Length - 1)]);
                break;
            case "Footsteps/Metal":
                _audioSource.PlayOneShot(metalClips[Random.Range(0, metalLandClips.Length - 1)]);
                break;
            default:
                _audioSource.PlayOneShot(defaultClips[Random.Range(0, defaultLandClips.Length - 1)]);
                break;
        }
    }
}