using System;
using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

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
    [SerializeField]
    private GameObject panelLoading;
    [SerializeField]
    private Animator loading; 

    [Header("Crafteos")]
    public List<Receta> todosObjetos;
    public GameObject slotPico;

    [Header("Niveles")]
    public Receta picoActual;
    public Receta espadaActual;
    public Receta armaduraActual;

    [SerializeField]
    private AudioClip musica;


    void Start()
    {
        AudioManager.Instance.PlayMusic(musica);
    }

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
            ActualizarStats();
            AudioManager.Instance.FadeOutMusic(1.5f);
            panelPausa.SetActive(true);        
            Time.timeScale = 0;
        }
        else
        {
            AudioManager.Instance.SetMusicVolume(0.4f);
            panelPausa.SetActive(false);
            Time.timeScale = 1;
        }
    }
    public void PlayButton()
    {
        panelLoading.SetActive(true);
        loading.SetBool("Load", true);
        StartCoroutine(Loading());
    }

    private IEnumerator Loading()
    {
        yield return new WaitForSeconds(3f);
        SceneManager.LoadScene(1);
        yield return new WaitForSeconds(1f);
        panelLoading.SetActive(false);
    }

    public void MainMenuButton()
    {
        //AudioManager.instance.StopMusic();
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void Exit()
    {
        Application.Quit();
    }
    
    public void Reiniciar()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ActualizarStats() 
    {
        if (espadaActual == null)
        {
            espadaText.text = "MAX LVL";
        }
        else
        {
            espadaText.text = espadaActual.LVL;
        }

        if (armaduraActual == null)
        {
            armaduraText.text = "MAX LVL";
        }
        else
        {
            armaduraText.text = armaduraActual.LVL;
        }

        if (picoActual == null)
        {
            picoText.text = "MAX LVL";
        }
        else
        {
            picoText.text = picoActual.LVL;
        }
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