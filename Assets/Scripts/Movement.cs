using UnityEngine;

public class Movement : MonoBehaviour
{
    // parameters
    [SerializeField] float mainThrust = 800f;
    [SerializeField] float rotationThrust = 100f;
    [SerializeField] AudioClip mainEngine;

    // cache
    Rigidbody rbd;
    AudioSource audioSource;

    // state

    void Start()
    {
        rbd = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            rbd.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            if (!audioSource.isPlaying)
                audioSource.PlayOneShot(mainEngine);
        }
        else
        {
            audioSource.Stop();
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationThrust);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            ApplyRotation(-rotationThrust);
        }
    }

    void ApplyRotation(float rotationThisFrame)
    {
        // freezing rotation so we can manually rotate
        rbd.freezeRotation = true;

        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);

        // unfreezing rotation so the physics system wil ltake over
        rbd.freezeRotation = false;
    }
}
