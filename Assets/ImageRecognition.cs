using UnityEngine;
using UnityEngine.XR.ARFoundation;
using UnityEngine.XR.ARSubsystems;
using TMPro;

[RequireComponent(typeof(ARTrackedImageManager))]
public class ImageRecognition : MonoBehaviour
{
    [System.Serializable]
    public class ImageText
    {
        public string imageName;
        [TextArea] public string text;
    }

    [SerializeField] ImageText[] m_Entries;

    ARTrackedImageManager m_Manager;

    void Awake() => m_Manager = GetComponent<ARTrackedImageManager>();
    void OnEnable() => m_Manager.trackablesChanged.AddListener(OnChanged);
    void OnDisable() => m_Manager.trackablesChanged.RemoveListener(OnChanged);

    void OnChanged(ARTrackablesChangedEventArgs<ARTrackedImage> args)
    {
        foreach (var img in args.added) UpdateImage(img);
        foreach (var img in args.updated) UpdateImage(img);
    }

    void UpdateImage(ARTrackedImage img)
    {
        var label = img.GetComponentInChildren<TMP_Text>(true);
        if (label == null) return;

        var entry = System.Array.Find(m_Entries, e => e.imageName == img.referenceImage.name);
        if (entry != null) label.text = entry.text;

        label.gameObject.SetActive(img.trackingState == TrackingState.Tracking);
    }
}