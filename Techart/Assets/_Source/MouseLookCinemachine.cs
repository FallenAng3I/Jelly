using UnityEngine;

public class MouseLookCinemachine : MonoBehaviour
{
    [SerializeField] private Transform fpsCamera; // Камера от первого лица
    [SerializeField] private Transform tpsCamera; // Камера от третьего лица

    [SerializeField] private float mouseSensitivity = 100f; // Чувствительность мыши
    [SerializeField] private float tpsVerticalClamp = 15f; // Ограничение угла вертикального вращения TPS-камеры

    private float xRotationFPS = 0f; // Угол вертикального вращения FPS камеры
    private float xRotationTPS = 0f; // Угол вертикального вращения TPS камеры
    private Transform activeCamera;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Определяем, какая камера активна
        activeCamera = tpsCamera.gameObject.activeSelf ? tpsCamera : fpsCamera;
    }

    private void Update()
    {
        HandleMouseLook();

        // Обновляем активную камеру
        if (tpsCamera.gameObject.activeSelf)
            activeCamera = tpsCamera;
        else if (fpsCamera.gameObject.activeSelf)
            activeCamera = fpsCamera;
    }

    private void HandleMouseLook()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        if (activeCamera == tpsCamera)
        {
            // --- TPS камера: ограниченное вертикальное вращение ---
            xRotationTPS -= mouseY;
            xRotationTPS = Mathf.Clamp(xRotationTPS, -tpsVerticalClamp, tpsVerticalClamp);
            activeCamera.localRotation = Quaternion.Euler(xRotationTPS, 0f, 0f);

            // Горизонтальное вращение персонажа
            transform.Rotate(Vector3.up * mouseX);
        }
        else if (activeCamera == fpsCamera)
        {
            // --- FPS камера: полное вертикальное вращение ---
            xRotationFPS -= mouseY;
            xRotationFPS = Mathf.Clamp(xRotationFPS, -90f, 90f);
            fpsCamera.localRotation = Quaternion.Euler(xRotationFPS, 0f, 0f);

            // Горизонтальное вращение персонажа
            transform.Rotate(Vector3.up * mouseX);
        }
    }
}
