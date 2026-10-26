using UnityEngine;

// Tourne l'objet pour qu'il fasse toujours face à la caméra 
public class FaceCamera : MonoBehaviour
{
    Transform m_Camera;

    void Start()
    {
        m_Camera = Camera.main.transform;
    }

    // après que la caméra a bougé pendant cette image.
    void LateUpdate()
    {
        Vector3 direction = transform.position - m_Camera.position;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }
}