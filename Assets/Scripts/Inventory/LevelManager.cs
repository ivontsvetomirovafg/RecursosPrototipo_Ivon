using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameData gameData;

    [Header("UI")]
    public Text[] textos;
    public GameObject[] imageInventory;

    [Header("Crafteos")]
    public List<Receta> todosObjetos;

    public void AddItem(string itemName, int amount)
    {
        for (int i = 0; i < gameData.recursos.Length; i++)
        {
            if (gameData.recursos[i].nombreObj == itemName)
            {
                gameData.recursos[i].cantidad += amount;
            }
        }
        UpdateUI();
    }

    public void AddItemInventario(int itemIndex)
    {
        gameData.inventarioUsuario.Add(todosObjetos[itemIndex]);
    }

    private void UpdateUI()
    {
        for (int i = 0; i < gameData.recursos.Length; i++)
        {
            if (gameData.recursos[i].cantidad == 0)
            {
                imageInventory[i].SetActive(false);
                textos[i].gameObject.SetActive(false);
            }
            else
            {
                imageInventory[i].SetActive(true);
                imageInventory[i].GetComponent<Image>().sprite = gameData.recursos[i].imageObj;
                textos[i].gameObject.SetActive(true);
                textos[i].text = gameData.recursos[i].cantidad.ToString();
            }
        }
    }
}