using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounter : MonoBehaviour
{
    [SerializeField] private GameObject[] selectedCounterVisualArray;
    [SerializeField] private BaseCounter counter;
    private void Start()
    {
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged;
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSelectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter == counter)
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
        foreach (GameObject selectedCounterVisual in selectedCounterVisualArray)
        {
            selectedCounterVisual.SetActive(true);
        }
    }
    private void  Hide()
    {
        foreach (GameObject selectedCounterVisual in selectedCounterVisualArray)
        {
            selectedCounterVisual.SetActive(false);
        }
    }
}
