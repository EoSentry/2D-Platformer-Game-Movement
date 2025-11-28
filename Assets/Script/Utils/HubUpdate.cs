using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HubUpdate : MonoBehaviour
{
    public ItemManager itemManager;
    public TextMeshProUGUI coinsText;
    
    private int _currentCoins;
    private int _lastCoins;

    public Image coinsReference;

    public void Update()
    {
        _currentCoins = itemManager.coins;
        
        if(_currentCoins != _lastCoins)
        {
            coinsText.text = _currentCoins.ToString();
            CoinSize();
            _lastCoins = _currentCoins;
        }

    }

    public void CoinSize()
    {
        coinsReference.transform.DOScale(1.2f, .1f).From();
    }


}
