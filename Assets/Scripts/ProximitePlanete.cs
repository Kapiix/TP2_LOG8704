using TMPro;
using UnityEngine;

// Fonctionnalité supplémentaire : interaction basee sur la position du téléphone
// Quand le téléphone s'approche de la planète, on affiche son nom et la distance
public class ProximitePlanete : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] Transform m_Visuel;
    [SerializeField] GameObject m_Etiquette;
    [SerializeField] TMP_Text m_TexteEtiquette;
    [SerializeField] GameObject m_Fiche;
    [SerializeField] string m_Nom = "Terre";

    [Header("Distances (en mètres)")]
    [SerializeField] float m_DistanceApparition = 0.4f;
    [SerializeField] float m_DistanceDisparition = 0.45f;

    Transform m_Camera;
    bool m_Proche;

    void Start()
    {
        m_Camera = Camera.main.transform;
        m_Etiquette.SetActive(false);
    }

    void Update()
    {
        float distance = Vector3.Distance(m_Camera.position, m_Visuel.position);

        // Deux seuils pour éviter que l'étiquette clignote quand on est exactement à la limite.
        if (!m_Proche && distance < m_DistanceApparition)
            m_Proche = true;
        else if (m_Proche && distance > m_DistanceDisparition)
            m_Proche = false;

        // Si la fiche est ouverte, elle contient déjà le nom on n'affiche pas les deux.
        bool afficher = m_Proche && !m_Fiche.activeSelf;
        if (m_Etiquette.activeSelf != afficher)
            m_Etiquette.SetActive(afficher);

        if (afficher)
            m_TexteEtiquette.text = $"<b>{m_Nom}</b>  ·  {distance * 100f:0} cm";
    }
}