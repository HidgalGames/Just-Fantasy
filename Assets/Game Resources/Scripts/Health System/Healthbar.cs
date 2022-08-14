using TMPro;
using TweenComponents;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    [SerializeField] protected HealthSystem _healthSystem;
    [Space]
    [SerializeField] protected ImageFillAmountTween _redFillTween;
    [SerializeField] protected ImageFillAmountTween _greenFillTween;
    [SerializeField] protected TweenSequence _tweenSequence;
    [Space]
    [SerializeField] protected TextMeshProUGUI _healthText;
    [SerializeField] protected PushScaleTween _scaleTween;

    protected virtual void Awake()
    {
        _healthSystem.OnHealed += OnHealed;
        _healthSystem.OnDamaged += OnDamaged;
    }

    private void Start()
    {
        OnHealthCountChanged(true);
    }

    private void OnDestroy()
    {
        _healthSystem.OnHealed -= OnHealed;
        _healthSystem.OnDamaged -= OnDamaged;
    }

    protected void OnHealed(int count)
    {
        OnHealthCountChanged(false);
    }

    protected void OnDamaged(DamageInfo dmg)
    {
        OnHealthCountChanged(true);
    }

    protected void OnHealthCountChanged(bool straight)
    {
        _redFillTween.FinishValue = _healthSystem.CurrentHealthPercent;
        _greenFillTween.FinishValue = _healthSystem.CurrentHealthPercent;

        _tweenSequence.Execute(straight);

        _healthText.text = $"{_healthSystem.CurrentHealth}/{_healthSystem.MaxHealth}";
        _scaleTween.Execute();
    }
}
