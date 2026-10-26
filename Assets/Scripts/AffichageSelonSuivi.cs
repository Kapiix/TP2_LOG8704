using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

// Affiche le contenu seulement quand l'image est réellement suivie par la caméra.
public class AffichageSelonSuivi : MonoBehaviour
{
    [SerializeField] GameObject m_Contenu;

    ARTrackedImage m_Image;

    void Awake()
    {
        m_Image = GetComponentInParent<ARTrackedImage>();
    }

    void Update()
    {
        bool suivie = m_Image != null && m_Image.trackingState == TrackingState.Tracking;
        if (m_Contenu.activeSelf != suivie)
            m_Contenu.SetActive(suivie);
    }
}