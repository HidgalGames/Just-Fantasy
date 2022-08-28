using UnityEngine;

public class ItemInfoUiTest : MonoBehaviour
{
    [SerializeField] private ItemInfoUI _itemUI;
    [SerializeField] private Item _testItem;

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

        if (_itemUI.IsEnabled)
        {
            _itemUI.HideInfo();
        }
        else
        {
            _itemUI.ShowItemInfo(_testItem);
        }
    }

#endif
}
