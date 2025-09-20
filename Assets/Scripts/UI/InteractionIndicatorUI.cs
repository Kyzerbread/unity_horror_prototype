using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionIndicatorUI : MonoBehaviour
{
    [SerializeField] private FirstPersonInteractionObserver observer;
    [SerializeField] private TMP_Text interactionText;

    [SerializeField] private Image handIndicatorImage;
    [SerializeField] private Sprite grabIcon;
    [SerializeField] private Sprite pressIcon;
    [SerializeField] private Sprite talkIcon;


    void OnEnable()
    {
        observer.OnHoverEnter += OnHoverEnter;
        observer.OnHoverExit += OnHoverExit;
    }

    // Update is called once per frame
    void OnDisable()
    {
        observer.OnHoverEnter -= OnHoverEnter;
        observer.OnHoverExit -= OnHoverExit;
    }

    private void Reset()
    {
        interactionText.text = "";
    }

    private void OnHoverEnter(IInteractionStrategy strategy)
    {
        interactionText.text = strategy.InteractionText;
    }

    private void OnHoverExit()
    {
        HideIndicator();
    }

    private void HideIndicator()
    {
        Reset();
    }
}
