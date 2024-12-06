using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

// Enum to define the various states of the battle system
public enum BattleState { Start, PlayerAction, PlayerMove, EnemyMove, Busy }

// Class to represent a Question and Answer (Q&A) with its difficulty level
[System.Serializable]
public class QandA
{
    public string question;  // The question text
    public string answer;    // The correct answer to the question
    public int difficulty;   // Difficulty level: 1 = Easy, 2 = Medium, 3 = Hard
}

// Class responsible for managing the battle dialog box and related actions
public class BattleDialogBox : MonoBehaviour
{
    [SerializeField] int lettersPerSecond; // Speed for typing effect in dialog
    [SerializeField] public Text dialogText; // UI text element for dialog
    [SerializeField] public GameObject ActionSelector; // UI for selecting actions
    [SerializeField] public GameObject MoveSelector; // UI for selecting moves

    [SerializeField] public Text Actions; // Text for actions (e.g., Attack, Defend)
    [SerializeField] List<Text> moveTexts; // List of UI text elements for moves

    [SerializeField] public Text Question; // UI text element to display the question

    public int correctAnswerIndex; // Index of the correct answer in the list
    public float correctAnswerValue; // Numerical value of the correct answer

    private string apiUrl = "https://capstone-api-5w9c.onrender.com/questions-by-difficulty"; // API URL to fetch questions

    // Called when the script instance is being loaded
    void Start()
    {
        // Fetch questions from the database when the game starts
        StartCoroutine(SetQandAFromDatabase());
    }

    // Sets a dialog string directly
    public void SetDialog(string dialog)
    {
        dialogText.text = dialog;
    }

    // Coroutine to simulate a typing effect for dialog text
    public IEnumerator TypeDialog(string dialog)
    {
        dialogText.text = ""; // Clear existing text
        foreach (var letter in dialog.ToCharArray())
        {
            dialogText.text += letter; // Add one letter at a time
            yield return new WaitForSeconds(1f / lettersPerSecond); // Wait between letters
        }
    }

    // Enables or disables the dialog text UI
    public void EnableDialogText(bool enabled)
    {
        dialogText.enabled = enabled;
    }

    // Enables or disables the action selector UI
    public void EnableActionSelector(bool enabled)
    {
        ActionSelector.SetActive(enabled);
    }

    // Enables or disables the move selector UI
    public void EnableMoveSelector(bool enabled)
    {
        MoveSelector.SetActive(enabled);
    }

    // Updates the move selection UI by highlighting the selected move
    public void UpdateMoveSelection(int selectedMove)
    {
        for (int i = 0; i < moveTexts.Count; ++i)
        {
            // Highlight the selected move with a blue color
            moveTexts[i].color = (i == selectedMove) ? Color.blue : Color.black;
        }
    }

    // Coroutine to fetch Q&A data from the API and set up the UI
    public IEnumerator SetQandAFromDatabase()
    {
        UnityWebRequest request = UnityWebRequest.Get(apiUrl); // Create a GET request
        yield return request.SendWebRequest(); // Send the request and wait for a response

        if (request.result != UnityWebRequest.Result.Success) // Check for errors
        {
            Debug.LogError("Error fetching questions: " + request.error);
            yield break;
        }

        string jsonResult = request.downloadHandler.text; // Get the JSON response
        QandA[] fetchedQandAs = JsonHelper.FromJson<QandA>(jsonResult); // Parse JSON into QandA objects

        if (fetchedQandAs == null || fetchedQandAs.Length == 0) // Check if data is valid
        {
            Debug.LogError("Failed to parse question data or no data returned.");
            yield break;
        }

        // Find a medium-difficulty question from the fetched data
        QandA mediumQuestion = System.Array.Find(fetchedQandAs, q => q.difficulty == 2);

        if (mediumQuestion == null) // If no medium question is found
        {
            Debug.LogError("No medium difficulty question found.");
            yield break;
        }

        Debug.Log("Fetched Medium QandA: " + JsonUtility.ToJson(mediumQuestion)); // Log the fetched question

        // Parse the answer into a float; handle invalid formats
        if (!float.TryParse(mediumQuestion.answer, out correctAnswerValue))
        {
            Debug.LogError("Invalid answer format in data.");
            yield break;
        }

        // Set the question text in the UI
        Question.text = mediumQuestion.question;

        // Format the correct answer to two decimal places
        string formattedAnswer = correctAnswerValue.ToString("F2");

        // Randomly assign the correct answer to one of the move slots
        int randSlot = UnityEngine.Random.Range(0, moveTexts.Count);
        correctAnswerIndex = randSlot;
        moveTexts[randSlot].text = formattedAnswer;

        // Populate the other slots with fake answers
        for (int i = 0; i < moveTexts.Count; i++)
        {
            if (i != randSlot)
            {
                float fakeAnswer;
                do
                {
                    // Generate a random fake answer within a range
                    fakeAnswer = Mathf.Round(UnityEngine.Random.Range(correctAnswerValue - 10f, correctAnswerValue + 10f));
                }
                while (Mathf.Approximately(fakeAnswer, correctAnswerValue)); // Ensure it's not the same as the correct answer

                moveTexts[i].text = fakeAnswer.ToString("F2"); // Display the fake answer
            }
        }
    }

    // Helper class to parse JSON arrays
    public static class JsonHelper
    {
        // Parses a JSON string into an array of objects
        public static T[] FromJson<T>(string json)
        {
            string newJson = "{ \"Items\": " + json + "}"; // Wrap JSON in a root object
            Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(newJson); // Deserialize the JSON
            return wrapper.Items; // Return the array of objects
        }

        // Wrapper class to handle JSON parsing
        [System.Serializable]
        private class Wrapper<T>
        {
            public T[] Items; // Array of items in the JSON
        }
    }
}