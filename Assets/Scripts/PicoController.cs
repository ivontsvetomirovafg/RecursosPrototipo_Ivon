using UnityEngine;
using UnityEngine.UI;

public class PicoController : MonoBehaviour
{
    [SerializeField] 
    private Image iconoSlotPico;   
    [SerializeField] 
    private Sprite iconoPico;     
    [SerializeField] 
    private Sprite iconoEspada;     

    public CharacterControler player;
    private bool equipado = false;
    private Animator animator;
    private LevelManager levelManager;
    public GameObject espada;
    [SerializeField]
    private Sprite espadaUI;
    private PiedraGolpes piedras;
    public Button botonPico;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        levelManager = FindObjectOfType<LevelManager>();
    }
    // Update is called once per frame
    public void Attack()
    {
        animator.SetTrigger("Attack");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Stone"))
        {
            piedras = collision.GetComponent<PiedraGolpes>(); 
            if (piedras != null)
            {
                piedras.RomperRoca();
            }            
        }
    }
    public void Seleccionar()
    {
        if (equipado == false)
        {
            equipado = true;
            espada.SetActive(false);
            iconoSlotPico.sprite = iconoEspada; 
        }
        else
        {
            equipado = false;
            espada.SetActive(true);
            iconoSlotPico.sprite = iconoPico; 
        }
        player.pico.SetActive(equipado);
    }
}
