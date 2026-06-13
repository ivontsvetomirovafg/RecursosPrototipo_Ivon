using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class InventarioUI : MonoBehaviour
{
    public GameObject panelCrafteo;

    [Header("Info del item seleccionado")]
    public Text nombreText;
    public Text descripcionText;
    public Text costeText;
    public Button craftButton;
    public Text nombreBoton;
    [SerializeField]
    private GameObject infoPanel;
    private Receta receta;
    private CraftingManager craftingManager;
    private LevelManager levelManager;
    [SerializeField]
    private Animator animator;

    [Header("Buttons")]
    [SerializeField]
    private Button picoButton;
    [SerializeField]
    private Receta pico1;

    [SerializeField]
    private Button swordButton;
    [SerializeField]
    private Receta sword2;

    [SerializeField]
    private Button armaduraButton;
    [SerializeField]
    private Receta armadura1;
    [SerializeField]
    private Receta armadura2;
    private CharacterControler player;

    void Start()
    {
        craftingManager = FindObjectOfType<CraftingManager>();
        levelManager = FindObjectOfType<LevelManager>();
        player = FindObjectOfType<CharacterControler>();
        animator.SetBool("Closed", true);

        picoButton.onClick.AddListener(delegate { MostrarInfo(pico1); });
        swordButton.onClick.AddListener(delegate { MostrarInfo(sword2); });
        armaduraButton.onClick.AddListener(delegate { MostrarInfo(armadura1); });
    }

    void Update()
    {    
        if (player.currentLife <=0)
        {
            return;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            if (animator.GetBool("Closed") == false)
            {
                animator.SetBool("Closed", true);
                StartCoroutine(TiempoClose()); 
            }
            else
            {
                animator.SetBool("Closed", false);
            }
        }
    }
    public IEnumerator TiempoClose()
    {        
        yield return new WaitForSeconds(2f);
        infoPanel.SetActive(false);
    }

    public void MostrarInfo(Receta _receta)
    {
        nombreBoton.text = "CRAFT";
        craftButton.interactable = true;
        craftingManager.errorText.SetActive(false);
        receta = _receta;
        panelCrafteo.SetActive(true);
        nombreText.text = receta.itemName;
        descripcionText.text = receta.description;
        string coste = "";
        
        if (receta.woodCost > 0) 
        {
            coste += "x" + receta.woodCost + " Madera  ";
        }
        if (receta.stoneCost > 0) 
        {
            coste += "x" + receta.stoneCost + " Piedra  ";
        }
        if (receta.slimeCost > 0) 
        {
            coste += "x" + receta.slimeCost + " Slime  ";
        }
        if (receta.bonesCost > 0) 
        {
            coste += "x" + receta.bonesCost + " Huesos  ";
        }
        costeText.text = coste;
    }
    public void Craftear()
    {
        if (receta == null) 
        {
            return;
        }
        bool crafteoExitoso = craftingManager.Craft(receta);
        
        if (receta.PocionVida == true || receta.PocionDaño == true)
        {
            return; 
        }

        if (crafteoExitoso == true && receta.siguienteNivel != null)
        {
            MostrarInfo(receta.siguienteNivel); 
            
            switch (receta.item)
            {
                case 0: // pico nivel 1
                    levelManager.picoActual = receta;
                    picoButton.onClick.RemoveAllListeners();
                    picoButton.onClick.AddListener(delegate { MostrarInfo(pico1.siguienteNivel); });
                    break;
                case 1: // espada nivel 2
                    levelManager.espadaActual = receta;
                    swordButton.onClick.RemoveAllListeners();
                    swordButton.onClick.AddListener(delegate { MostrarInfo(sword2.siguienteNivel); });
                    break;
                case 2: // armadura nivel 1
                    levelManager.armaduraActual = receta;
                    armaduraButton.onClick.RemoveAllListeners();
                    armaduraButton.onClick.AddListener(delegate { MostrarInfo(armadura1.siguienteNivel); });
                    break;
                case 3: // armadura nivel 2
                    levelManager.armaduraActual = receta;
                    armaduraButton.onClick.RemoveAllListeners();
                    armaduraButton.onClick.AddListener(delegate { MostrarInfo(armadura2.siguienteNivel); });
                    break;
            }
        }
        else if (crafteoExitoso == true && receta.siguienteNivel == null)
        {
            nombreBoton.text = "MAX LVL";
            craftButton.interactable = false; 

            switch (receta.item)
            {
                case 0: 
                    levelManager.picoActual = null;
                    picoButton.interactable = false; 
                    break;
                case 1: 
                    levelManager.espadaActual = null;
                    swordButton.interactable = false; 
                    break;
                case 2: 
                    levelManager.armaduraActual = null;
                    armaduraButton.interactable = false; 
                    break;
            }
        }
    }
}
    


