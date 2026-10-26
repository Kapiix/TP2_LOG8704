using TMPro;
using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;

// affiche une seule consigne à la fois. On ne passe à l'étape suivante que lorsque
// l'utilisateur a réussi l étape en cours, et chaque consigne reste visible un minimum de temps
public class GuideUtilisateur : MonoBehaviour
{
    enum Etape { Scanner, Poser, Toucher, Gestes, Approcher, Carte, Bravo, Termine }

    [Header("Interface")]
    [SerializeField] GameObject m_Bandeau;
    [SerializeField] TMP_Text m_TexteConsigne;

    [Header("AR")]
    [SerializeField] ARSession m_Session;
    [SerializeField] ARPlaneManager m_PlaneManager;

    [Header("Durées (en secondes)")]
    [SerializeField] float m_DureeMinimaleEtape = 2.5f;
    [SerializeField] float m_DureeAstuceGestes = 6f;
    [SerializeField] float m_DureeMessageFinal = 6f;

    Etape m_Etape;
    float m_DebutEtape;
    bool m_PlaneteTouchee;

    void OnEnable()
    {
        TapPlanete.PlaneteTouchee += QuandPlaneteTouchee;
    }

    void OnDisable()
    {
        TapPlanete.PlaneteTouchee -= QuandPlaneteTouchee;
    }

    void Start()
    {
        AllerA(Etape.Scanner);
    }

    void QuandPlaneteTouchee()
    {
        if (m_Etape == Etape.Toucher)
            m_PlaneteTouchee = true;
    }

    void Update()
    {
        if (EtapeReussie())
            AllerA(m_Etape + 1);

        // Un problème de suivi passe avant la consigne 
        string consigne = ProblemeDeSuivi() ?? Consigne(m_Etape);
        m_Bandeau.SetActive(consigne != null);
        if (consigne != null)
            m_TexteConsigne.text = consigne;
    }

    void AllerA(Etape etape)
    {
        m_Etape = etape;
        m_DebutEtape = Time.time;
        m_PlaneteTouchee = false;
    }

    bool EtapeReussie()
    {
        float duree = Time.time - m_DebutEtape;

        // Chaque consigne reste affichee un minimum de temps, pour que l'utilisateur puisse la lire
        if (duree < m_DureeMinimaleEtape)
            return false;

        // On n'avance pas tant que le suivi AR est perdu.
        if (ProblemeDeSuivi() != null)
            return false;

        switch (m_Etape)
        {
            case Etape.Scanner:   return m_PlaneManager.trackables.count > 0;
            case Etape.Poser:     return TapPlanete.NombreDePlanetes > 0;
            case Etape.Toucher:   return m_PlaneteTouchee;
            case Etape.Gestes:    return duree > m_DureeAstuceGestes;
            case Etape.Approcher: return ProximitePlanete.NombreDeProches > 0;
            case Etape.Carte:     return AffichageSelonSuivi.NombreDeSoleilsVisibles > 0;
            case Etape.Bravo:     return duree > m_DureeMessageFinal;
            default:              return false;
        }
    }

    static string Consigne(Etape etape)
    {
        switch (etape)
        {
            case Etape.Scanner:
                return "Bouge lentement le téléphone au-dessus d'une table pour la scanner.";
            case Etape.Poser:
                return "Choisis une planète avec le bouton +, puis touche la table pour la poser.";
            case Etape.Toucher:
                return "Touche une planète pour afficher sa fiche.";
            case Etape.Gestes:
                return "Astuce : glisse un doigt pour déplacer la planète. Avec deux doigts, tourne-la ou pince pour changer sa taille.";
            case Etape.Approcher:
                return "Approche le téléphone à moins de 40 cm d'une planète : son nom et sa distance s'affichent au-dessus.";
            case Etape.Carte:
                return "Pose la carte Soleil sur la table et vise-la avec la caméra.";
            case Etape.Bravo:
                return "Bravo, tu as tout découvert ! Le bouton en haut à droite permet de tout recommencer.";
            default:
                return null;
        }
    }

    static string ProblemeDeSuivi()
    {
        if (ARSession.state == ARSessionState.SessionTracking)
            return null;

        switch (ARSession.notTrackingReason)
        {
            case NotTrackingReason.InsufficientLight:
                return "Pas assez de lumière : allume la pièce.";
            case NotTrackingReason.ExcessiveMotion:
                return "Tu bouges trop vite : déplace le téléphone plus lentement.";
            case NotTrackingReason.InsufficientFeatures:
                return "Vise une surface avec des motifs de préférence";
            default:
                return "Démarrage de la réalité augmentée... bouge doucement le téléphone.";
        }
    }

    // Appelée par le bouton Réinitialiser
    public void Reinitialiser()
    {
        foreach (TapPlanete planete in FindObjectsByType<TapPlanete>(FindObjectsSortMode.None))
            Destroy(planete.gameObject);

        m_Session.Reset();

        AllerA(Etape.Scanner);
    }
}
