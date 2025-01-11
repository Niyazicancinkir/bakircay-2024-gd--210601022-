using UnityEngine;
using TMPro; // TextMeshProUGUI kullanmak için

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private int score = 0;
    public TextMeshProUGUI scoreText; // TextMeshProUGUI referansý

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        // Skor UI'sýný baþlat
        UpdateScoreText();
    }

    public void AddScore(int amount)
    {
        score += amount;
        UpdateScoreText(); // Skoru UI'da güncelle
    }

    public int GetScore()
    {
        return score;
    }

    // Skor metnini güncelleyen fonksiyon
    void UpdateScoreText()
    {
        if (scoreText != null)
        {
            scoreText.text = "Skor: " + score;
        }
        else
        {
            UnityEngine.Debug.LogWarning("Skor TextMeshProUGUI bileþeni atanmadý!");
        }
    }

    // Resetleme fonksiyonu
    public void ResetGame()
    {
        // Skoru sýfýrla
        score = 0;
        UpdateScoreText(); // UI'daki skoru güncelle

        // Oyun nesnelerini temizle (örneðin tüm objeleri sahneden kaldýr)
        GameObject[] objectsToDestroy = GameObject.FindGameObjectsWithTag("spawnedObject");
        foreach (GameObject obj in objectsToDestroy)
        {
            Destroy(obj); // Nesneleri sahneden kaldýr
        }

        // Gerekirse diðer oyun baþlangýcý iþlemleri burada yapýlabilir
        UnityEngine.Debug.Log("Oyun sýfýrlandý ve yeniden baþlatýldý!");
    }
}
