using UnityEngine;

public class PuertasController : MonoBehaviour
{
    [SerializeField] 
    private Transform playerSpawnPoint;

    [Header("Limites Cam")]
    [SerializeField] 
    private float MinX;
    [SerializeField] 
    private float MaxX;
    [SerializeField] 
    private float MinY;
    [SerializeField] 
    private float MaxY;

    private CamaraController camController;

    private void Start()
    {
        camController = Camera.main.GetComponent<CamaraController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.transform.position = playerSpawnPoint.position;
            camController.SetLimits(MinX, MaxX, MinY, MaxY);
        }
    }
}
