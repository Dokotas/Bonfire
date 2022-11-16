using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static  readonly float WorldSize = 10;
    public static bool IsPaused;
    
    public static GameManager Instance { get; private set; }

    public static Player Player { get; private set; }
    public static Bonfire Bonfire { get; private set; }
    [SerializeField] private GameObject loseMenu;

    private float _startTime;

    void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        Player = FindObjectOfType<Player>();
        Bonfire = FindObjectOfType<Bonfire>();
        
        _startTime = Time.time;
        IsPaused = false;
    }
    

    public void EndGame()
    {
        IsPaused = true;
        loseMenu.SetActive(true);
        loseMenu.transform.GetComponentInChildren<TextMeshProUGUI>().text =
            "Вы продержались " + (int)(Time.time - _startTime) + " секунд!";
    }

    public void Restart()
    {
        SceneManager.LoadScene("Game");
    }
}