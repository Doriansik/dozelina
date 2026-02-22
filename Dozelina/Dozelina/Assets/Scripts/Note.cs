using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] private Transform notePos;

    [Header("U¿ywaj local jeœli Note jest dzieckiem czegoœ")]
    [SerializeField] private bool useLocal = false;

    [Header("FMOD")]
    [SerializeField] private FMODUnity.EventReference noteOpenEvent;
    [SerializeField] private FMODUnity.EventReference noteCloseEvent; // opcjonalnie

    private bool isOpen;

    private Vector3 startPos;
    private Quaternion startRot;
    private static readonly Quaternion NOTE_ROT = Quaternion.Euler(90, 0f, 90);

    private void Awake()
    {
        if (useLocal)
        {
            startPos = transform.localPosition;
            startRot = transform.localRotation;
        }
        else
        {
            startPos = transform.position;
            startRot = transform.rotation;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isOpen = !isOpen;

            if (isOpen)
            {
                GoToNote();
                PlayOpenSound();
            }
            else
            {
                GoToStart();
                PlayCloseSound(); // usuñ, jeœli nie chcesz dŸwiêku zamkniêcia
            }
        }
    }

    private void GoToNote()
    {
        if (!notePos) return;

        if (useLocal)
        {
            transform.localPosition = notePos.localPosition;
            transform.localRotation = NOTE_ROT;
        }
        else
        {
            transform.position = notePos.position;
            transform.rotation = NOTE_ROT;
        }
    }

    private void GoToStart()
    {
        if (useLocal)
        {
            transform.localPosition = startPos;
            transform.localRotation = startRot;
        }
        else
        {
            transform.SetPositionAndRotation(startPos, startRot);
        }
    }

    private void PlayOpenSound()
    {
        if (noteOpenEvent.IsNull) return;

        // 2D (UI) click / paper rustle:
        FMODUnity.RuntimeManager.PlayOneShot(noteOpenEvent);

        // Jeœli wolisz 3D, zamieñ na:
        // FMODUnity.RuntimeManager.PlayOneShot(noteOpenEvent, transform.position);
    }

    private void PlayCloseSound()
    {
        if (noteCloseEvent.IsNull) return;

        FMODUnity.RuntimeManager.PlayOneShot(noteCloseEvent);
        // albo 3D:
        // FMODUnity.RuntimeManager.PlayOneShot(noteCloseEvent, transform.position);
    }
}