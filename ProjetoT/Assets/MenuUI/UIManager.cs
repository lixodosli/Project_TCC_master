using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{

   [SerializeField] private TextMeshProUGUI enter_txt;
   [SerializeField] private Ease MotionType;

   public List<Transform> menuAniBu = new List<Transform>();


    public Image LoadingBarFill;

   public string cena;
   public float fadeTime = 1f;
   public float fadeLogoTime = 1f;
   public float fadeElasticTime = 1f;
   public float fadeOutTime = 1f;
   public float duration = 1f;
   public float strength = 1f;
   public AudioClip popupSFX;
   public float timeToFadeNG;


   private AudioSource audioSource;
   private bool titleOn = false;

   public CanvasGroup canvasGroupOpcoes;
   public RectTransform rectTransformOpcoes;
   public GameObject gobjOpcoes;

   public CanvasGroup canvasGroupMenu;
   public RectTransform rectTransformMenu;
   public GameObject gobjMenu;

   public CanvasGroup canvasGroupCreditos;
   public RectTransform rectTransformCreditos;
   public GameObject gobjCerditos;

   public CanvasGroup canvasGroupLogo;
   public RectTransform rectTransformLogo;

   public CanvasGroup canvasGroupLoadPanel;
   public bool panelLogo = false;

   public void Start()
    {
        FadeInLogoJogo();
        MenuAniBu();
        audioSource = GetComponent<AudioSource>();
        gobjMenu.SetActive(false);
        gobjOpcoes.SetActive(false);
        gobjCerditos.SetActive(false);
    }

  
    public void Update()
   {
        if (canvasGroupLogo.alpha >= 1f)
        {
            panelLogo = true;
        }
        if (Input.GetKey(KeyCode.Return) && titleOn == false)
        {
            PanelFadeInMenu();          
        }
    }
   public void MenuAniBu()
    {
        for (int i = 0; i < menuAniBu.Count; i++)
        {
            menuAniBu[i].localScale = new Vector2(0, 0);
        }
    }
   public void FadeInLogoJogo()
    {
        canvasGroupLogo.DOFade(1, fadeLogoTime);
        enter_txt.transform.DOScale(1.1f, 1f).SetLoops(1000, LoopType.Yoyo).SetEase(MotionType);
    }
   public void PanelFadeInOpcoes()
   {
       gobjOpcoes.SetActive(true);
       canvasGroupOpcoes.alpha = 0f;
       canvasGroupOpcoes.DOFade(1, fadeTime);
       canvasGroupMenu.DOFade(0, fadeOutTime);
       canvasGroupLogo.alpha = 0f;


    }
   public void PanelFadeInMenu()
   { 
        if(panelLogo == true)
        {
            gobjMenu.SetActive(true);
            MenuAniBu();
            canvasGroupMenu.alpha = 0f;
            canvasGroupMenu.DOFade(1, fadeTime);
            canvasGroupLogo.DOFade(0, 0.1f);
            StartCoroutine(TimeMenu());
            titleOn = true;
        }
    }
   public void PanelFadeInCreditos()
   {
       gobjCerditos.SetActive(true);
       canvasGroupCreditos.alpha = 0f;
       canvasGroupCreditos.DOFade(1, fadeTime);
       canvasGroupMenu.DOFade(0, fadeOutTime);
       canvasGroupLogo.alpha = 0f;
    }

   public void PanelFadeOutOpcoes()
   {
        panelLogo = true;
        canvasGroupOpcoes.alpha = 1f;
        canvasGroupOpcoes.DOFade(0, fadeTime);
        PanelFadeInMenu();
        gobjOpcoes.SetActive(false);
    }
   public void PanelFadeOutMenu()
   {
       canvasGroupMenu.alpha = 1f;
       canvasGroupMenu.DOFade(0, fadeTime);        
       FadeInLogoJogo();
       titleOn = false;
       gobjMenu.SetActive(false);

    }
   public void PanelFadeOutCreditos()
   {
        panelLogo = true;
        canvasGroupCreditos.alpha = 1f;
        canvasGroupCreditos.DOFade(0, fadeTime);      
        PanelFadeInMenu();
        gobjCerditos.SetActive(false);
    }
   IEnumerator TimeMenu()
    {
        for (int i = 0; i < menuAniBu.Count; i++)
        {
            audioSource.PlayOneShot(popupSFX);
            menuAniBu[i].DOScale(1.2f, .25f);
            yield return new WaitForSeconds(.25f);
            menuAniBu[i].DOScale(1f, .25f);
        }
        
    }
   public void QuitGame()
    {
        Application.Quit();
    }
   public void StartGame()
    {
        StartCoroutine(LoadSceneWithDelay());
    }

   private IEnumerator LoadSceneWithDelay()
    {
        canvasGroupLogo.alpha = 0f;
        canvasGroupMenu.DOFade(0, fadeLogoTime);
        yield return new WaitForSeconds(timeToFadeNG);
        SceneManager.LoadScene(cena);
    }

   public void LoadSceneAsync()
    {
        StartCoroutine(PerformLoadSceneAsync());
    }

   private IEnumerator PerformLoadSceneAsync()
    {
        canvasGroupLogo.alpha = 0f;
        canvasGroupMenu.alpha = 0f;
        canvasGroupLoadPanel.DOFade(1, fadeLogoTime);
        var operation = SceneManager.LoadSceneAsync(cena);
        while(operation.isDone == false)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            LoadingBarFill.fillAmount = progressValue;
            yield return null;
        }
        canvasGroupLoadPanel.DOFade(0, fadeLogoTime);
    }
}
