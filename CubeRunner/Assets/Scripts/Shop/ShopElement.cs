using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ShopElement : MonoBehaviour
{
    [SerializeField] private Cube _cube;
    [SerializeField] private TextMeshProUGUI labelName;
    public Button buttonBuy;
    public Button buttonEquip;
    [SerializeField] private Image image;
    [SerializeField] private Color equipColor;

    public void SetCube(Cube cube)
    {
        _cube = cube;
        UpdateInfo();
    }

    public void UpdateInfo()
    {
        labelName.text = _cube.GetName();

        if(_cube.IsBought())
        {
            buttonBuy.gameObject.SetActive(false);
            buttonEquip.gameObject.SetActive(true);

            if(_cube.IsEquiped())
            {
                buttonEquip.GetComponentInChildren<TextMeshProUGUI>().text = "Equiped";
                buttonEquip.GetComponent<Image>().color = equipColor;
            }
            else
            {
                buttonEquip.GetComponentInChildren<TextMeshProUGUI>().text = "Equip";
                buttonEquip.GetComponent<Image>().color = Color.white;
            }
        }
        else
        {
            buttonBuy.gameObject.SetActive(true);
            buttonEquip.gameObject.SetActive(false);

            buttonBuy.GetComponentInChildren<TextMeshProUGUI>().text = _cube.GetCost().ToString();
        }

        Sprite sprite = Resources.Load<Sprite>("PlayerSprites/" + _cube.GetPlayerType().ToString());
        image.sprite = sprite;
    }
}
