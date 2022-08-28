using System.Collections.Generic;
using TMPro;
using TweenComponents;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUI : MonoBehaviour
{
    [SerializeField] private ItemRarityColors _colorsList;
    [SerializeField] private Image _textBackground;
    [Space]
    [SerializeField] private TextMeshProUGUI[] _itemNameTexts;
    [SerializeField] private TextMeshProUGUI _itemDescription;
    [Space]
    [SerializeField] private RectTransform _charachteristicsParent;
    [SerializeField] private TextMeshProUGUI _characteristicPrefab;
    [Space]
    [SerializeField] private ChangeScaleTween _scaleAnimation;
    [Space]
    [Header("Debug")]
    [SerializeField] private Item _testItem;

    private List<TextMeshProUGUI> _characteriscticLines = new List<TextMeshProUGUI>();

    private RectTransform _rect;

    private bool _isEnabled = false;

    private void Awake()
    {
        _rect = transform as RectTransform;
    }

    public void ShowItemInfo(Item item)
    {
        if (!item) return;

        _textBackground.color = _colorsList.RarityColors[(int)item.Rarity];

        foreach(var nameText in _itemNameTexts)
        {
            nameText.text = item.DisplayName;
        }

        var itemCharacteristics = item.GetCharacteristics();

        var maxCount = Mathf.Max(itemCharacteristics.Length, _characteriscticLines.Count);

        for (int i = 0; i < maxCount; i++)
        {
            if(i >= _characteriscticLines.Count)
            {
                _characteriscticLines.Add(Instantiate(_characteristicPrefab, _charachteristicsParent));
            }

            if(i >= itemCharacteristics.Length)
            {
                _characteriscticLines[i].gameObject.SetActive(false);
            }
            else
            {
                _characteriscticLines[i].text = itemCharacteristics[i];
            }
        }

        _itemDescription.text = item.Description;

        _rect.anchoredPosition = Input.mousePosition;

        _scaleAnimation.Execute(true);

        _isEnabled = true;
    }

    public void HideInfo()
    {
        if (!_isEnabled) return;

        _isEnabled = false;
        _scaleAnimation.Execute(false);
    }

#if UNITY_EDITOR

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            SwitchTestInfo();
        }
    }

    private void SwitchTestInfo()
    {
        if (!_testItem) return;

        if (_isEnabled)
        {
            HideInfo();
        }
        else
        {
            ShowItemInfo(_testItem);
        }
    }

#endif
}
