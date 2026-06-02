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
    [SerializeField]
    private GameObject panelPausa;
    [SerializeField]
    private Text espadaText;
    [SerializeField]
    private Text armaduraText;
    [SerializeField]
    private Text picoText; 

    [Header("Crafteos")]
    public List<Receta> todosObjetos;
    public GameObject slotPico;

    [Header("Niveles")]
    public Receta picoActual;
    public Receta espadaActual;
    public Receta armaduraActual;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }
    public void Pause()
    {
        if (panelPausa.activeInHierarchy == false)
        {
            panelPausa.SetActive(true);
            ActualizarStats();
            Time.timeScale = 0;
        }
        else
        {
            panelPausa.SetActive(false);
            Time.timeScale = 1;
        }
    }
    private void ActualizarStats() //CambiarloBien
    {
        espadaText.text = "LVL " + espadaActual.itemName;
        armaduraText.text = "LVL " + armaduraActual.itemName;
        picoText.text = "LVL " + picoActual.itemName;
    }
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
                textos[i].text = "x" + gameData.recursos[i].cantidad.ToString();
            }
        }
    }
}