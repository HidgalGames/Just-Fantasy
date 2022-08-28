using System.Collections;
using System.Collections.Generic;
using TMPro;
using TweenComponents;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoUI : MonoBehaviour
{
    [SerializeField] private Canvas _canvas;
    [Space]
    [SerializeField] private ItemRarityColors _colorsList;
    [SerializeField] private Image _textBackground;
    [SerializeField] private Image _infoBackground;
    [Space]
    [SerializeField] private TextMeshProUGUI[] _itemNameTexts;
    [SerializeField] private TextMeshProUGUI _itemDescription;
    [Space]
    [SerializeField] private RectTransform _charachteristicsParent;
    [SerializeField] private TextMeshProUGUI _characteristicPrefab;
    [Space]
    [SerializeField] private ChangeScaleTween _scaleAnimation;

    [Header("Settings")]
    [Tooltip("Min and Max size for characteristic line")]
    [SerializeField] private Vector2Int _fontSizes;

    private List<TextMeshProUGUI> _characteriscticLines = new List<TextMeshProUGUI>();

    private RectTransform _rect;

    public bool IsEnabled { get; private set; } = false;

    private void Awake()
    {
        _rect = transform as RectTransform;
    }

    private void Start()
    {
        UpdateLayoutOutOfScreen();
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

        for (int i = maxCount - 1; i >= 0; i--)
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

                //first line with characteristic should be greater than others
                if(i >= itemCharacteristics.Length - 1)
                {
                    _characteriscticLines[i].fontSize = _fontSizes.y;
                }
                else
                {
                    _characteriscticLines[i].fontSize = _fontSizes.x;
                }
            }
        }

        _itemDescription.text = item.Description;

        UpdateLayoutOutOfScreen();

        var pos = Input.mousePosition;
        pos.z = _canvas.planeDistance;       
        _rect.position = _canvas.worldCamera.ScreenToWorldPoint(pos);

        _scaleAnimation.Execute(true);
        IsEnabled = true;
    }

    public void HideInfo()
    {
        if (!IsEnabled) return;

        IsEnabled = false;
        _scaleAnimation.Execute(false);
    }

    private void UpdateLayoutOutOfScreen()
    {
        _rect.position = Vector3.right * 10000f;
        _rect.localScale = Vector3.one;

        LayoutRebuilder.ForceRebuildLayoutImmediate(_rect);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_charachteristicsParent);
        LayoutRebuilder.ForceRebuildLayoutImmediate(_infoBackground.rectTransform);
    }
}
