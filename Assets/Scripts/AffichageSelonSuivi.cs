using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

public class AffichageSelonSuivi : MonoBehaviour
{
    public static int NombreDeSoleilsVisibles { get; private set; }

    [SerializeField] GameObject m_Contenu;

    ARTrackedImage m_Image;
    bool m_Visible;

    void Awake()
    {
        m_Image = GetComponentInParent<ARTrackedImage>();
        m_Contenu.SetActive(false);
    }

    void OnDisable()
    {
        if (m_Visible)
        {
            m_Visible = false;
            NombreDeSoleilsVisibles--;
        }
    }

    void Update()
    {
        bool suivie = m_Image != null && m_Image.trackingState == TrackingState.Tracking;
        if (suivie == m_Visible)
            return;

        m_Visible = suivie;
        m_Contenu.SetActive(suivie);
        NombreDeSoleilsVisibles += suivie ? 1 : -1;
    }
}
