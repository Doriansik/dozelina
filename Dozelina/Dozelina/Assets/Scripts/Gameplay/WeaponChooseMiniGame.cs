using System.Collections;
using UnityEngine;

public class WeaponChooseMiniGame : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private GameObject leftArrow;
    [SerializeField] private GameObject rightArrow;

    [SerializeField] private LayerMask weaponLayer;
    [SerializeField] private LayerMask arrowLayer;

    [SerializeField] private float moveDuration;
    [SerializeField] private float rotateDuration = 0.35f;
    [SerializeField] private float stepAngle = 90f;
    [SerializeField] private GameObject weapon;
    [SerializeField] private GameObject transformPointLeft;
    [SerializeField] private GameObject transformPointRight;

    private bool isWeaponSelected;
    private bool isRotating;

    private void Update()
    {
        if (!Input.GetMouseButtonDown(0) || isRotating) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out RaycastHit hit)) return;

        if (((1 << hit.collider.gameObject.layer) & weaponLayer) != 0)
        {
            isWeaponSelected = true;
            leftArrow.SetActive(true);
            rightArrow.SetActive(true);
            return;
        }

        if (!isWeaponSelected) return;

        if (hit.collider.gameObject == leftArrow || hit.collider.transform.IsChildOf(leftArrow.transform))
        {
            StartCoroutine(RotateBy(-stepAngle));
            StartCoroutine(MoveTo(transformPointLeft.transform.position));
            StartCoroutine(RotateWeaponBy(-stepAngle));
        }
        else if (hit.collider.gameObject == rightArrow || hit.collider.transform.IsChildOf(rightArrow.transform))
        {
            StartCoroutine(RotateBy(stepAngle));
            StartCoroutine(MoveTo(transformPointRight.transform.position));
            StartCoroutine(RotateWeaponBy(stepAngle));
        }
    }

    private IEnumerator RotateBy(float deltaYaw)
    {
        isRotating = true;

        Quaternion start = cameraTransform.rotation;
        Quaternion target = start * Quaternion.Euler(0f, deltaYaw, 0f);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / rotateDuration;
            cameraTransform.rotation = Quaternion.Slerp(start, target, t);
            yield return null;
        }

        cameraTransform.rotation = target;
        isRotating = false;
    }

    private IEnumerator MoveTo(Vector3 targetPos)
    {
        Vector3 start = weapon.transform.position;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / moveDuration; // u¿yj moveDuration
            weapon.transform.position = Vector3.Lerp(start, targetPos, t);
            yield return null;
        }

        weapon.transform.position = targetPos;
    }

    private IEnumerator RotateWeaponBy(float deltaYaw)
    {
        Quaternion start = weapon.transform.rotation;
        Quaternion target = start * Quaternion.Euler(0f, deltaYaw, 0f);

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime / rotateDuration;
            weapon.transform.rotation = Quaternion.Slerp(start, target, t);
            yield return null;
        }

        weapon.transform.rotation = target;
    }
}