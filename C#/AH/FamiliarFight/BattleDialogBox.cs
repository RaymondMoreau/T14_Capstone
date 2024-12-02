using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public enum BattleState { Start, PlayerAction, PlayerMove, EnemyMove, Busy }

[System.Serializable]
public class QandA
{
    public string question;  // Question text
    public string answer;    // Correct answer
    public int difficulty;   // Difficulty level (1 = easy, 2 = medium, 3 = hard)
}

public class BattleDialogBox : MonoBehaviour
{
    [SerializeField] int lettersPerSecond;
    [SerializeField] public Text dialogText;
    [SerializeField] public GameObject ActionSelector;
    [SerializeField] public GameObject MoveSelector;

    [SerializeField] public Text Actions;
    [SerializeField] List<Text> moveTexts;

    [SerializeField] public Text Question;

    public int correctAnswerIndex; // Stores the index of the correct answer
    public float correctAnswerValue; // Stores the actual correct answer value

    private string apiUrl = "https://capstone-api-5w9c.onrender.com/questions-by-difficulty";

    void Start()
    {
        StartCoroutine(SetQandAFromDatabase());
    }

    public void SetDialog(string dialog)
    {
        dialogText.text = dialog;
    }

    public IEnumerator TypeDialog(string dialog)
    {
        dialogText.text = "";
        foreach (var letter in dialog.ToCharArray())
        {
            dialogText.text += letter;
            yield return new WaitForSeconds(1f / lettersPerSecond);
        }
    }

    public void EnableDialogText(bool enabled)
    {
        dialogText.enabled = enabled;
    }

    public void EnableActionSelector(bool enabled)
    {
        ActionSelector.SetActive(enabled);
    }

    public void EnableMoveSelector(bool enabled)
    {
        MoveSelector.SetActive(enabled);
    }

    public void UpdateMoveSelection(int selectedMove)
    {
        for (int i = 0; i < moveTexts.Count; ++i)
        {
            moveTexts[i].color = (i == selectedMove) ? Color.blue : Color.black;
        }
    }

    public IEnumerator SetQandAFromDatabase()
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl);
        yield return request.SendWebRequest();

        if (request.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error fetching questions: " + request.error);
            yield break;
        }

        string jsonResult = request.downloadHandler.text;
        QandA[] fetchedQandAs = JsonHelper.FromJson<QandA>(jsonResult);

        if (fetchedQandAs == null || fetchedQandAs.Length == 0)
        {
            Debug.LogError("Failed to parse question data or no data returned.");
            yield break;
        }

        QandA mediumQuestion = System.Array.Find(fetchedQandAs, q => q.difficulty == 2);

        if (mediumQuestion == null)
        {
            Debug.LogError("No medium difficulty question found.");
            yield break;
        }

        Debug.Log("Fetched Medium QandA: " + JsonUtility.ToJson(mediumQuestion));

        if (!float.TryParse(mediumQuestion.answer, out correctAnswerValue))
        {
            Debug.LogError("Invalid answer format in data.");
            yield break;
        }

        Question.text = mediumQuestion.question;

        string formattedAnswer = correctAnswerValue.ToString("F2");

        int randSlot = UnityEngine.Random.Range(0, moveTexts.Count);
        correctAnswerIndex = randSlot;
        moveTexts[randSlot].text = formattedAnswer;

        for (int i = 0; i < moveTexts.Count; i++)
        {
            if (i != randSlot)
            {
                float fakeAnswer;
                do
                {
                    fakeAnswer = Mathf.Round(UnityEngine.Random.Range(correctAnswerValue - 10f, correctAnswerValue + 10f));
                }
                while (Mathf.Approximately(fakeAnswer, correctAnswerValue));

                moveTexts[i].text = fakeAnswer.ToString("F2"); // Display rounded number with .00
            }
        }
    }

    // Helper class to parse JSON arrays
    public static class JsonHelper
    {
        public static T[] FromJson<T>(string json)
        {
            string newJson = "{ \"Items\": " + json + "}";
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson);
            return wrapper.Items;
        }

        [System.Serializable]
        private class Wrapper<T>
        {
            public T[] Items;
        }
    }
}