using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    [SerializeField] float velocidad;
    [SerializeField]
    float sensitivy;
    [SerializeField]
    [Range(0f, 0.3f)] float smoothing;
    [SerializeField]
    [Range(-90, 90)] float upperLookLimit;
    [SerializeField]
    [Range(-90, 90)] float lowerLookLimit;

    Camera cam;
    float rotacionEjeX;
    Vector2 suavidadV;

    private void Awake()
    {
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        Rotar();
        Mover();
    }

    private void Rotar()
    {
        if (Time.timeScale == 0)
        {
            return;
        }

        Vector2 movimiento = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        suavidadV.x = Mathf.SmoothStep(suavidadV.x, movimiento.x * sensitivy * 1, smoothing);
        suavidadV.y = Mathf.SmoothStep(suavidadV.y, movimiento.y * sensitivy * 1, smoothing);

        transform.Rotate(Vector3.up, suavidadV.x);

        rotacionEjeX += suavidadV.y;
        rotacionEjeX = Mathf.Clamp(rotacionEjeX, lowerLookLimit, upperLookLimit);
        cam.transform.localRotation = Quaternion.Euler(-rotacionEjeX, 0, 0);
    }

    private void Mover()
    {
        Vector3 movimiento = Vector3.forward * Input.GetAxis("Vertical") + Vector3.right * Input.GetAxis("Horizontal");
        movimiento = Vector3.ClampMagnitude(movimiento, 1f);
        movimiento = Quaternion.Euler(0, transform.rotation.y, 0) * movimiento;
        transform.Translate(movimiento * Time.deltaTime * velocidad);
    }
}
