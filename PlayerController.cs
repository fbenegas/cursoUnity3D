using UnityEngine;

/* Autor: Francisco Benegas Cortés
 * Descripción: Script de movimiento que permite usar o no gravedad.
 * El script esta diseñado para funcionar con ratón mientras este en pc
 * y con la head camera cuando estés en Android VR.
 */

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float playerMaxSpeed;
    public float rotationSpeed;
    public bool useGravity;

    //private Camera playerCamera;
    private CharacterController controller;     // Referencia a CharacterController
    private float vSpeed = 0;                   // Acumulador de la velocidad gravitatoria

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Vector3 auxM = Vector3.zero;

        auxM = transform.right * Input.GetAxis("Horizontal");
        auxM += transform.forward * Input.GetAxis("Vertical");
        if (useGravity)
            auxM.y = 0;
        auxM.Normalize();
        auxM = auxM * Time.deltaTime * playerMaxSpeed;
        if (useGravity)
        {
            if (controller.isGrounded)
                vSpeed = 0;
            else
            {
                vSpeed += Physics.gravity.y * Time.deltaTime;
                auxM.y = vSpeed;
            }
        }
        controller.Move(auxM);
        if (Application.platform != RuntimePlatform.Android)
        {
            float auxY = Input.GetAxis("Mouse X") * rotationSpeed * Time.deltaTime;
            float auxX = Input.GetAxis("Mouse Y") * rotationSpeed * Time.deltaTime * -1;
            transform.Rotate(Vector3.right * auxX, Space.Self);
            transform.Rotate(Vector3.up * auxY, Space.World);
        }
    }
}
