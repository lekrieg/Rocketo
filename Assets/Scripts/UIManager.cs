// ////////////////////////
// File: UIManager.cs
// Created at: 08 22, 2023
// Description:
// 
// Modified by: Daniel Henrique
// 08 24, 2023
// ////////////////////////

using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Transform _launchAreaPanel;

    [SerializeField] private Transform _endAreaPanel;

    public GameObject quitButton;
    public static UIManager Instance { get; set; }

    private void Awake()
    {
        Instance = this;

        _launchAreaPanel.Find("LaunchButton").GetComponent<Button>()
            .onClick.AddListener(() => { EventManager.Instance.Launch(); });
        _launchAreaPanel.Find("ChangeLaunchButton").GetComponent<Button>()
            .onClick.AddListener(() => { EventManager.Instance.ChangeLaunchType(); });

        _endAreaPanel.Find("ResetButton").GetComponent<Button>()
            .onClick.AddListener(() => { GameManager.Instance.LoadScene("Level"); });
        _endAreaPanel.Find("AccelerateButton").GetComponent<Button>()
            .onClick.AddListener(() => { EventManager.Instance.LandingImpulse(); });

        quitButton.GetComponent<Button>().onClick.AddListener(() => { Quit(); });

        _endAreaPanel.gameObject.SetActive(false);
    }

    public void DeactivateLaunchPanel()
    {
        _launchAreaPanel.gameObject.SetActive(false);
    }

    public void ActivateEndPanel()
    {
        _endAreaPanel.gameObject.SetActive(true);
    }

    private void Quit()
    {
        Application.Quit();
    }
}