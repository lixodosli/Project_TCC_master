using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{

   public List<Transform> menuAniBu = new List<Transform>();

   public string cena;
   public float fadeTime = 1f;
   public float fadeLogoTime = 1f;
   public float fadeElasticTime = 1f;
   public float fadeOutTime = 1f;
   public float duration = 1f;
   public float strength = 1f;
   public AudioClip popupSFX;

   private AudioSource audioSource;

   public CanvasGroup canvasGroupOpcoes;
   public RectTransform rectTransformOpcoes;

   public CanvasGroup canvasGroupMenu;
   public RectTransform rectTransformMenu;

   public CanvasGroup canvasGroupCreditos;
   public RectTransform rectTransformCreditos;

   public CanvasGroup canvasGroupLogo;
   public RectTransform rectTransformLogo;

   public void Start()
    {
        FadeInLogoJogo();
        MenuAniBu();
        audioSource = GetComponent<AudioSource>();
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
    }
   public void PanelFadeInOpcoes()
   {
       canvasGroupOpcoes.alpha = 0f;
       rectTransformOpcoes.transform.localPosition = new Vector3(0f, -1000f, 0f);
       rectTransformOpcoes.DOAnchorPos(new Vector2(0f, 0f), fadeElasticTime, false).SetEase(Ease.OutElastic);
       canvasGroupOpcoes.DOFade(1, fadeTime);
       canvasGroupMenu.DOFade(0, fadeOutTime);
       canvasGroupLogo.alpha = 0f;

    }
   public void PanelFadeInMenu()
   {
        MenuAniBu();
        canvasGroupMenu.alpha = 0f;
       rectTransformMenu.transform.localPosition = new Vector3(0f, -1000f, 0f);
       rectTransformMenu.DOAnchorPos(new Vector2(0f, 0f), fadeElasticTime, false).SetEase(Ease.OutElastic);
       canvasGroupMenu.DOFade(1, fadeTime);
       canvasGroupLogo.DOFade(0, fadeOutTime);
        StartCoroutine(TimeMenu());
    }
   public void PanelFadeInCreditos()
   {
       canvasGroupCreditos.alpha = 0f;
       rectTransformCreditos.transform.localPosition = new Vector3(0f, -1000f, 0f);
       rectTransformCreditos.DOAnchorPos(new Vector2(0f, 0f), fadeElasticTime, false).SetEase(Ease.OutElastic);
       canvasGroupCreditos.DOFade(1, fadeTime);
       canvasGroupMenu.DOFade(0, fadeOutTime);
       canvasGroupLogo.alpha = 0f;
    }

   public void PanelFadeOutOpcoes()
   {
       canvasGroupOpcoes.alpha = 1f;
       rectTransformOpcoes.transform.localPosition = new Vector3(0f, 0f, 0f);
       rectTransformOpcoes.DOAnchorPos(new Vector2(0f, -1000f), fadeElasticTime, false).SetEase(Ease.InOutQuint);
       canvasGroupOpcoes.DOFade(0, fadeTime);
        PanelFadeInMenu();
    }
   public void PanelFadeOutMenu()
   {
       canvasGroupMenu.alpha = 1f;
       rectTransformMenu.transform.localPosition = new Vector3(0f, 0f, 0f);
       rectTransformMenu.DOAnchorPos(new Vector2(0f, -1000f), fadeElasticTime, false).SetEase(Ease.InOutQuint);
       canvasGroupMenu.DOFade(0, fadeTime);
        FadeInLogoJogo();
    }
   public void PanelFadeOutCreditos()
   {
       canvasGroupCreditos.alpha = 1f;
       rectTransformCreditos.transform.localPosition = new Vector3(0f, 0f, 0f);
       rectTransformCreditos.DOAnchorPos(new Vector2(0f, -1000f), fadeElasticTime, false).SetEase(Ease.InOutQuint);
       canvasGroupCreditos.DOFade(0, fadeTime);
       
        PanelFadeInMenu();
    }
   IEnumerator TimeMenu()
    {
        for (int i = 0; i < menuAniBu.Count; i++)
        {
            audioSource.PlayOneShot(popupSFX);
            menuAniBu[i].DOScale(1.5f, .25f);
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
        SceneManager.LoadScene(cena);
    }
}
