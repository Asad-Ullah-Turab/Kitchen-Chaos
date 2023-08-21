using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounter : MonoBehaviour
{
    [SerializeField] private GameObject selectedCounterVisual;
    [SerializeField] private ClearCounter clearCounter;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == clearCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    private void Show()
    {
        selectedCounterVisual.gameObject.SetActive(true);
    }
    private void  Hide()
    {
        selectedCounterVisual.gameObject.SetActive(false);
    }
}
