using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ShopController : MonoBehaviour
{
    private List<Cube> shopList;
    private List<ShopElement> elementGameObjectList;

    [SerializeField]private List<Cube> defaultList;
    [SerializeField] GameObject shopElement;
    [SerializeField] GameObject shopParent;
    [SerializeField] TextMeshProUGUI labelCoins;
    [SerializeField] TextMeshProUGUI labelInfo;
    [SerializeField] UISounds uISounds;

    public float currentX;
    public float currentY;

    public float stepRight;
    public float stepDown;
    public int maxInRow;


    private int _money;
    private string filePath;


    private const string MESSAGE_NOT_ENOUGH_MONEY = "Not enough money!";

    private void Start()
    {
        labelInfo.gameObject.SetActive(false);
        _money = PlayerPrefs.GetInt("Coins", 0);
        labelCoins.text = _money.ToString();

        filePath = Path.Combine(Application.persistentDataPath, "shop.dat");

        if (File.Exists(filePath))
        {
            OnLoadFile();
            if(shopList.Count < defaultList.Count)
            {
                for(int i = shopList.Count; i < defaultList.Count; i++)
                {
                    shopList.Add(defaultList[i]);
                }
                OnSaveFile();
            }
        }
        else
        {
            shopList = new List<Cube>(defaultList);
            OnSaveFile();
        }


        CreateGameObjectList();
    }

    public void OnBackToMenu()
    {
        OnSaveFile();
        SceneManager.LoadScene("MainScene");
    }

    public void OnLoadFile()
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = File.Open(filePath, FileMode.Open);
        shopList = formatter.Deserialize(stream) as List<Cube>;
        stream.Close();
    }

    public void OnSaveFile()
    {
        FileStream stream = File.Create(filePath);
        BinaryFormatter formatter = new BinaryFormatter();
        formatter.Serialize(stream, shopList);
        stream.Close();
    }


    public void OnButtonBuyClick(int index)
    {
        if (_money >= shopList[index].GetCost())
        {
            shopList[index].SetBought(true);

            _money -= shopList[index].GetCost();
            PlayerPrefs.SetInt("Coins", _money);
            labelCoins.text = _money.ToString();

            elementGameObjectList[index].SetCube(shopList[index]);
            OnSaveFile();
        }
        else
        {
            ShowLabelInfo(MESSAGE_NOT_ENOUGH_MONEY);
        }

        uISounds.OnBuyCube();
    }

    public void OnButtonEqipClick(int index)
    {
        if (!shopList[index].IsEquiped())
        {
            shopList[index].SetEquip(true);

            PlayerPrefs.SetInt("EquipedPlayerType",(int)shopList[index].GetPlayerType());

            for (int i = 0; i < shopList.Count; i++)
            {
                if (i != index)
                    shopList[i].SetEquip(false);

                elementGameObjectList[i].SetCube(shopList[i]);
            }

            OnSaveFile();
        }
        uISounds.OnSwtichCube();
    }

    public void CreateGameObjectList()
    {
        int row = 1;
        elementGameObjectList = new List<ShopElement>();

        for (int i = 0; i < shopList.Count; i++ )
        {
            GameObject shopEl = Instantiate(shopElement);

            RectTransform rectTransform = shopEl.GetComponent<RectTransform>();
            rectTransform.SetParent(shopParent.GetComponent<RectTransform>());
            rectTransform.GetComponent<RectTransform>().anchoredPosition = new Vector2(currentX, currentY);
            rectTransform.GetComponent<RectTransform>().localScale = new Vector3(1,1, 1);

            ShopElement element = shopEl.GetComponent<ShopElement>();
            element.SetCube(shopList[i]);

            int index = i;
            element.buttonEquip.onClick.AddListener( () => OnButtonEqipClick(index));
            element.buttonBuy.onClick.AddListener( () => OnButtonBuyClick(index));

            elementGameObjectList.Add(element);

            if (row < maxInRow)
            {
                currentX += stepRight;
                row++;
            }
            else
            {
                currentX -= stepRight*(maxInRow-1);
                currentY -= stepDown;
                row = 1;
            }
        }

    }

    private void ShowLabelInfo(string text)
    {
        labelInfo.gameObject.SetActive(true);
        labelInfo.text = text;
        labelInfo.GetComponent<Animator>().Play("ShowText", 0, 0);
        StartCoroutine(CloseLabelInfo());
    }

    private IEnumerator CloseLabelInfo()
    {
        yield return new WaitForSeconds(1.8f);
        labelInfo.gameObject.SetActive(false);
    }
}
