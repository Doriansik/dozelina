using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] private Transform notePos; // gdzie ma siê pojawiæ (docelowa pozycja/rotacja)

    [Header("U¿ywaj local jeœli Note jest dzieckiem czegoœ")]
    [SerializeField] private bool useLocal = false;

    private bool isOpen;

    private Vector3 startPos;
    private Quaternion startRot;

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
            startRot = Quaternion.identity;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            isOpen = !isOpen;

            if (isOpen) GoToNote();
            else GoToStart();
        }
    }

    private void GoToNote()
    {
        if (!notePos)
        {
            Debug.LogWarning("[Note] notePos nie jest podpiête!");
            return;
        }

        if (useLocal)
        {
            transform.localPosition = notePos.localPosition;
            transform.localRotation = notePos.localRotation;
        }
        else
        {
            transform.SetPositionAndRotation(notePos.position, notePos.rotation);
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
}