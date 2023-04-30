using TMPro;
using UnityEngine;

public class TextFormatter
{
    private const int maxChars = 160;
    private const int maxCharsPerLine = 40;
    private const int maxLines = 4;
    private const int font24Size = 22;
    private const int font20Size = 18;
    private const int font16Size = 14;

    public void FormatText(string inputText, TextMeshProUGUI tmp)
    {
        // Clamp the input text to the maximum allowed characters
        string clampedText = inputText.Substring(0, Mathf.Min(inputText.Length, maxChars));

        // Split the text into words
        string[] words = clampedText.Split(' ');

        // Build the formatted text based on the number of characters and lines
        string formattedText = "";
        int currentLineLength = 0;
        int currentLineCount = 0;
        int currentFontSize = font24Size;

        foreach (string word in words)
        {
            // If the word doesn't fit on the current line, move it to the next line
            if (currentLineLength + word.Length + 1 > maxCharsPerLine)
            {
                formattedText += "\n";
                currentLineLength = 0;
                currentLineCount++;
                currentFontSize = currentLineCount < maxLines - 1 ? font20Size : font16Size;
            }

            // Add the word to the current line
            formattedText += word + " ";
            currentLineLength += word.Length + 1;

            // Check if we need to switch to a smaller font size
            if (currentLineCount == 0 && currentLineLength >= maxCharsPerLine)
            {
                currentFontSize = font20Size;
                currentLineCount++;
            }
            else if (currentLineCount == 1 && currentLineLength >= maxCharsPerLine * 2)
            {
                currentFontSize = font16Size;
                currentLineCount++;
            }
        }

        // Set the formatted text and font size on the TextMeshProUGUI object
        tmp.text = formattedText;
        tmp.fontSize = currentFontSize;
    }

    public string FormatText(string inputText)
    {
        // Clamp the input text to the maximum allowed characters
        string clampedText = inputText.Substring(0, Mathf.Min(inputText.Length, maxChars));

        // Split the text into words
        string[] words = clampedText.Split(' ');

        // Build the formatted text based on the number of characters and lines
        string formattedText = "";
        int currentLineLength = 0;
        int currentLineCount = 0;
        int currentFontSize = font24Size;

        foreach (string word in words)
        {
            // If the word doesn't fit on the current line, move it to the next line
            if (currentLineLength + word.Length + 1 > maxCharsPerLine)
            {
                formattedText += "\n";
                currentLineLength = 0;
                currentLineCount++;
                currentFontSize = currentLineCount < maxLines - 1 ? font20Size : font16Size;
            }

            // Add the word to the current line
            formattedText += word + " ";
            currentLineLength += word.Length + 1;

            // Check if we need to switch to a smaller font size
            if (currentLineCount == 0 && currentLineLength >= maxCharsPerLine)
            {
                currentFontSize = font20Size;
                currentLineCount++;
            }
            else if (currentLineCount == 1 && currentLineLength >= maxCharsPerLine * 2)
            {
                currentFontSize = font16Size;
                currentLineCount++;
            }
        }

        // Set the formatted text and font size on the TextMeshProUGUI object
        return formattedText;
    }
}