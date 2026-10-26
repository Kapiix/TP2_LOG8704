using System.Collections;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

// Garde la planète fixee dans le monde réel avec un ancrage AR
// Un nouvel ancrage est créé à la pose, puis à chaque fois qu'on relâche la planète
[RequireComponent(typeof(XRGrabInteractable))]
public class AncragePlanete : MonoBehaviour
{
    XRGrabInteractable m_Interactable;
    ARAnchor m_Ancre;

    void Awake()
    {
        m_Interactable = GetComponent<XRGrabInteractable>();
    }

    void OnEnable()
    {
        m_Interactable.selectExited.AddListener(QuandRelachee);
    }

    void OnDisable()
    {
        m_Interactable.selectExited.RemoveListener(QuandRelachee);
    }

    void Start()
    {
        StartCoroutine(AncrerAuProchainImage());
    }

    void QuandRelachee(SelectExitEventArgs args)
    {
        StartCoroutine(AncrerAuProchainImage());
    }

    IEnumerator AncrerAuProchainImage()
    {
        yield return null;

        var ancienneAncre = m_Ancre;

        var objetAncre = new GameObject($"Ancre {name}");
        objetAncre.transform.SetPositionAndRotation(transform.position, transform.rotation);
        m_Ancre = objetAncre.AddComponent<ARAnchor>();
        transform.SetParent(objetAncre.transform, true);

        if (ancienneAncre != null)
            Destroy(ancienneAncre.gameObject);
    }

    void OnDestroy()
    {
        // Quand la planète est supprimée, son ancrage ne sert plus à rien
        if (m_Ancre != null)
            Destroy(m_Ancre.gameObject);
    }
}