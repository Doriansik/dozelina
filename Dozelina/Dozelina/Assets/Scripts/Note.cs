using UnityEngine;

public class Note : MonoBehaviour
{
    [SerializeField] private Transform notePos; // gdzie ma siê pojawiæ (docelowa pozycja/rotacja)

    [Header("U¿ywaj local jeœli Note jest dzieckiem czegoœ")]
    [SerializeField] private bool useLocal = false;

    private bool isOpen;

    private Vector3 startPos;
    private Quaternion startRot;
    private static readonly Quaternion NOTE_ROT = Quaternion.Euler(90f, 0f, 90f);
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

            if (isOpen) GoToNote();
            else GoToStart();
        }
    }

    private void GoToNote()
    {
        if (!notePos)
        {
            return;
        }

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
}