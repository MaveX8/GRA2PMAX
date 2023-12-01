using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text livesText;
    [SerializeField] private Slider abilitySlider;
    public float abilityFillRate = 0.1f; // Adjust the rate at which the slider fills
    private bool isShootingFast = false;
    private float shootingAbilityDuration = 5f;
    private float shootingAbilityTimer = 0f;

    private Player player;
    private Invaders invaders;
    private MysteryShip mysteryShip;
    private Bunker[] bunkers;

    public int score { get; private set; }
    public int lives = 3;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        player = FindObjectOfType<Player>();
        invaders = FindObjectOfType<Invaders>();
        mysteryShip = FindObjectOfType<MysteryShip>();
        bunkers = FindObjectsOfType<Bunker>();

        NewGame();
    }

    private void Update()
    {
        if (lives <= 0 && Input.GetKeyDown(KeyCode.Return))
        {
            NewGame();
        }

        // Your shooting ability logic
        if (Input.GetButtonDown("Fire1") && CanShoot())
        {
            Shoot();
        }

        // Key bind to reset the slider and use the ability
        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetSliderAndUseAbility();
        }

        // Check if shooting ability is active and update the timer
        if (isShootingFast)
        {
            shootingAbilityTimer -= Time.deltaTime;

            if (shootingAbilityTimer <= 0f)
            {
                // Disable shooting ability when the timer runs out
                isShootingFast = false;
                shootingAbilityTimer = 0f;
            }
        }
    }

    private void NewGame()
    {
        gameOverUI.SetActive(false);

        SetScore(0);
        SetLives(3);
        NewRound();
    }

    private void NewRound()
    {
        invaders.ResetInvaders();
        invaders.gameObject.SetActive(true);

        for (int i = 0; i < bunkers.Length; i++)
        {
            bunkers[i].ResetBunker();
        }

        Respawn();
    }

    private void Respawn()
    {
        Vector3 position = player.transform.position;
        position.x = 0f;
        player.transform.position = position;
        player.gameObject.SetActive(true);
    }

    private void Shoot()
    {
        if (IsShootingFast())
        {
            // Perform shooting faster logic
            Debug.Log("Shooting Faster!");
            // Add your shooting faster logic here
        }
        else
        {
            // Perform regular shooting logic
            Debug.Log("Normal Shooting");
            // Add your regular shooting logic here
        }
    }

    bool CanShoot()
    {
        // Check if the ability slider is filled
        return abilitySlider.value >= 1f;
    }

    public void IncreaseSliderValue()
    {
        // Call this method when an enemy is killed to increase the slider value
        abilitySlider.value += abilityFillRate;
        abilitySlider.value = Mathf.Clamp01(abilitySlider.value); // Ensure the value is within [0, 1]
    }

    private void ResetSliderAndUseAbility()
    {
        if (abilitySlider.value >= 1f)
        {
            abilitySlider.value = 0f; // Reset the slider
            isShootingFast = true;   // Set the shooting ability to true

            // Set the timer for shooting ability duration
            shootingAbilityTimer = shootingAbilityDuration;

            // Perform any additional actions related to using the ability here
            Debug.Log("Ability Activated!");
        }
        else
        {
            // Optionally, provide feedback or a message indicating that the slider is not full
            Debug.Log("Slider is not full. Ability cannot be activated.");
        }
    }

    private void GameOver()
    {
        SceneManager.LoadSceneAsync("Menu");
    }

    private void SetScore(int score)
    {
        this.score = score;
        scoreText.text = score.ToString().PadLeft(4, '0');
    }

    private void SetLives(int lives)
    {
        this.lives = Mathf.Max(lives, 0);
        livesText.text = this.lives.ToString();
    }

    public void OnPlayerKilled(Player player)
    {
        SetLives(lives - 1);

        player.gameObject.SetActive(false);

        if (lives > 0)
        {
            Invoke(nameof(NewRound), 1f);
        }
        else
        {
            GameOver();
        }
    }

    public void OnInvaderKilled(Invader invader)
    {
        invader.gameObject.SetActive(false);

        SetScore(score + invader.score);
        IncreaseSliderValue(); // Call the method to increase the slider value

        if (invaders.GetAliveCount() == 0)
        {
            NewRound();
        }
    }

    public void OnMysteryShipKilled(MysteryShip mysteryShip)
    {
        SetScore(score + mysteryShip.score);
        IncreaseSliderValue(); // Call the method to increase the slider value
    }

    public void OnBoundaryReached()
    {
        if (invaders.gameObject.activeSelf)
        {
            invaders.gameObject.SetActive(false);

            OnPlayerKilled(player);
        }
    }

    // Method to check if shooting ability is active
    public bool IsShootingFast()
    {
        return isShootingFast;
    }
}
