using UnityEngine;

// Fait tourner l'objet sur lui-même en continu, autour de son axe vertical.
public class RotationContinue : MonoBehaviour
{
    [SerializeField] float m_DegresParSeconde = 20f;

    void Update()
    {
        transform.Rotate(0f, m_DegresParSeconde * Time.deltaTime, 0f, Space.Self);
    }
}