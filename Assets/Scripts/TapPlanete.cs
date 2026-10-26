using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

// Un tap court sur la planète la fait tourner et affiche (ou cache) sa fiche d'information.
// Un glisser n'est pas un tap : on mesure la durée et le déplacement entre le toucher et le relâcher.
[RequireComponent(typeof(XRGrabInteractable))]
public class TapPlanete : MonoBehaviour
{
    [Header("Références")]
    [SerializeField] Transform m_Visuel;
    [SerializeField] GameObject m_Fiche;
    [SerializeField] TMP_Text m_TexteFiche;

    [Header("Contenu de la fiche")]
    [SerializeField] string m_Nom = "Terre";
    [SerializeField, TextArea(3, 6)] string m_Description;

    [Header("Détection du tap")]
    [SerializeField] float m_DureeMaxTap = 0.3f;
    [SerializeField] float m_DeplacementMaxTap = 0.01f;

    [Header("Animation")]
    [SerializeField] float m_DureeRotation = 1.5f;

    XRGrabInteractable m_Interactable;
    float m_HeureToucher;
    Vector3 m_PositionToucher;
    Coroutine m_Rotation;

    void Awake()
    {
        m_Interactable = GetComponent<XRGrabInteractable>();
        m_TexteFiche.text = $"<b>{m_Nom}</b>\n{m_Description}";
        m_Fiche.SetActive(false);
    }

    void OnEnable()
    {
        m_Interactable.selectEntered.AddListener(QuandTouchee);
        m_Interactable.selectExited.AddListener(QuandRelachee);
    }

    void OnDisable()
    {
        m_Interactable.selectEntered.RemoveListener(QuandTouchee);
        m_Interactable.selectExited.RemoveListener(QuandRelachee);
    }

    void QuandTouchee(SelectEnterEventArgs args)
    {
        m_HeureToucher = Time.time;
        m_PositionToucher = transform.position;
    }

    void QuandRelachee(SelectExitEventArgs args)
    {
        bool rapide = Time.time - m_HeureToucher <= m_DureeMaxTap;
        bool immobile = Vector3.Distance(transform.position, m_PositionToucher) <= m_DeplacementMaxTap;

        if (rapide && immobile)
            QuandTap();
    }

    void QuandTap()
    {
        m_Fiche.SetActive(!m_Fiche.activeSelf);

        if (m_Rotation != null)
            StopCoroutine(m_Rotation);
        m_Rotation = StartCoroutine(TournerUnTour());
    }

    IEnumerator TournerUnTour()
    {
        Quaternion depart = m_Visuel.localRotation;

        for (float t = 0f; t < m_DureeRotation; t += Time.deltaTime)
        {
            // SmoothStep : démarre doucement, accélère, puis ralentit avant de s'arrêter.
            float progression = Mathf.SmoothStep(0f, 1f, t / m_DureeRotation);
            m_Visuel.localRotation = depart * Quaternion.Euler(0f, 360f * progression, 0f);
            yield return null;
        }

        m_Visuel.localRotation = depart;
        m_Rotation = null;
    }
}