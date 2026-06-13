using UnityEngine;

public class PiedraGolpes : MonoBehaviour
{
    private int golpesParaRomper = 3;
    public Sprite piedraActual;
    [SerializeField]
    private Sprite piedraRomper1;
    [SerializeField]
    private Sprite piedraRomper2;
    private LevelManager levelManager;

    [SerializeField] 
    private AudioClip stone;

    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    public void RomperRoca()
    {
        AudioManager.Instance.PlaySFX(stone);
        if (levelManager.picoActual == null || levelManager.picoActual.LVL == "2")
        {
            levelManager.AddItem("Stone", 1);
            Destroy(gameObject);
        }

        golpesParaRomper--;
        SpriteRenderer sprite = GetComponent<SpriteRenderer>();

        if (golpesParaRomper == 2)
        {
            sprite.sprite = piedraRomper1;
        }

        else if (golpesParaRomper == 1)
        {
            sprite.sprite = piedraRomper2;
        }

        else if (golpesParaRomper <= 0)
        {
            levelManager.AddItem("Stone", 1);
            Destroy(gameObject); 
        }
    }
}
