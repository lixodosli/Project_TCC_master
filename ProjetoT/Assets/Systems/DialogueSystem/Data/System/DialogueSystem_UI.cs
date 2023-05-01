using UnityEngine;
using System.Collections;
using TMPro;

public class DialogueSystem_UI : MonoBehaviour
{
    [SerializeField] private float m_LettersPerSecond;
    [SerializeField] private TextMeshProUGUI m_DialogueNameText;
    [SerializeField] private TextMeshProUGUI m_DialogueText;
    [SerializeField] private GameObject m_DialogueBox;
    [SerializeField] private GameObject m_ChoiceBox;
    [SerializeField] private TMP_Text[] m_ChoiceTexts;
    public bool IsActive = false;

    private void Update()
    {
        IsActive = m_DialogueBox.activeSelf;
    }

    public void DisplayDialogue(string name, string text, int[] options)
    {
        StopAllCoroutines();
        TextFormatter formatter = new TextFormatter();

        m_DialogueNameText.text = name;
        formatter.FormatText(text, m_DialogueText);

        StartCoroutine(DisplayTextLetterByLetter(m_DialogueText));

        if (options == null || options.Length == 0)
        {
            // Hide choice box if there are no options
            m_ChoiceBox.SetActive(false);
        }
        else
        {
            // Show choice box and populate with option texts
            for (int i = 0; i < options.Length; i++)
            {
                if (i < m_ChoiceTexts.Length)
                {
                    m_ChoiceTexts[i].text = DialogueSystem.Instance.CurrentConversation.Dialogues[options[i]].Text;
                    m_ChoiceTexts[i].gameObject.SetActive(true);
                }
            }

            // Deactivate any remaining choice texts
            for (int i = options.Length; i < m_ChoiceTexts.Length; i++)
            {
                m_ChoiceTexts[i].gameObject.SetActive(false);
            }

            m_ChoiceBox.SetActive(true);
        }

        m_DialogueBox.SetActive(true);
    }

    private IEnumerator DisplayTextLetterByLetter(TextMeshProUGUI textMesh)
    {
        float delayTime = 1.0f / m_LettersPerSecond;
        textMesh.maxVisibleCharacters = 0;

        for (int i = 0; i < m_DialogueText.text.Length; i++)
        {
            textMesh.maxVisibleCharacters++;
            yield return new WaitForSeconds(delayTime);
        }
    }

    public void HideDialogue()
    {
        m_DialogueBox.SetActive(false);
        m_ChoiceBox.SetActive(false);
    }
}