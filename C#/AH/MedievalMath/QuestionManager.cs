using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Networking;
using System.Collections.Generic;

[System.Serializable]
public class Question
{
    public string question;
    public int answer;
    public int difficulty;
}

[System.Serializable]
public class QuestionList
{
    public List<Question> questions;
}

public class QuestionManager : MonoBehaviour
{
    public GameObject unitButtonsContainer;
    public GameObject questionContainer;
    public TMP_Text questionText;
    public TMP_InputField answerInputField;
    public Button submitButton;
    public TMP_Text feedbackText;

    public GameObject easyUnitPrefab;
    public GameObject mediumUnitPrefab;
    public GameObject hardUnitPrefab;

    private string correctAnswer;
    private string currentDifficulty;  // Track the difficulty of the current question

    void Start()
    {
        // Initially show the unit buttons and hide the question area
        unitButtonsContainer.SetActive(true);
        questionContainer.SetActive(false);
        feedbackText.text = "";  // Clear feedback initially

        // Add listeners to unit buttons
        GameObject.Find("UnitButton1").GetComponent<Button>().onClick.AddListener(() => FetchAndShowQuestion("easy"));
        GameObject.Find("UnitButton2").GetComponent<Button>().onClick.AddListener(() => FetchAndShowQuestion("medium"));
        GameObject.Find("UnitButton3").GetComponent<Button>().onClick.AddListener(() => FetchAndShowQuestion("hard"));

        // Add listener for the submit button in the question area
        submitButton.onClick.AddListener(CheckAnswer);
    }

    void FetchAndShowQuestion(string difficulty)
    {
        StartCoroutine(FetchQuestionsFromServer(difficulty));
    }

    IEnumerator FetchQuestionsFromServer(string difficulty)
    {
        string url = "https://capstone-api-5w9c.onrender.com/questions-by-difficulty";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string jsonResponse = request.downloadHandler.text;

                // Wrap the JSON array in a root object for parsing
                string wrappedJson = "{\"questions\":" + jsonResponse + "}";
                QuestionList questionList = JsonUtility.FromJson<QuestionList>(wrappedJson);

                if (questionList != null && questionList.questions.Count > 0)
                {
                    // Filter the question by difficulty
                    Question fetchedQuestion = GetQuestionByDifficulty(questionList.questions, difficulty);
                    if (fetchedQuestion != null)
                    {
                        ShowQuestion(fetchedQuestion);
                    }
                    else
                    {
                        Debug.LogError("No question found for the selected difficulty.");
                    }
                }
                else
                {
                    Debug.LogError("No questions found in the response.");
                }
            }
            else
            {
                Debug.LogError("Failed to fetch questions: " + request.error);
            }
        }
    }

    Question GetQuestionByDifficulty(List<Question> questions, string difficulty)
    {
        int difficultyLevel = difficulty == "easy" ? 1 : (difficulty == "medium" ? 2 : 3);
        return questions.Find(q => q.difficulty == difficultyLevel);
    }

    void ShowQuestion(Question fetchedQuestion)
    {
        // Hide unit buttons and show question area
        unitButtonsContainer.SetActive(false);
        questionContainer.SetActive(true);
        feedbackText.text = ""; // Clear previous feedback

        // Set question and correct answer
        questionText.text = $"What is {fetchedQuestion.question}?";
        correctAnswer = fetchedQuestion.answer.ToString();

        // Track current difficulty for unit spawning
        currentDifficulty = fetchedQuestion.difficulty == 1 ? "easy" : fetchedQuestion.difficulty == 2 ? "medium" : "hard";

        // Clear input field
        answerInputField.text = "";
    }

    void CheckAnswer()
    {
        string playerAnswer = answerInputField.text.Trim();

        if (playerAnswer.Equals(correctAnswer, System.StringComparison.OrdinalIgnoreCase))
        {
            feedbackText.text = "Correct!";
            feedbackText.color = Color.green;

            // Spawn the unit based on difficulty
            SpawnUnit();
        }
        else
        {
            feedbackText.text = "Incorrect, try again!";
            feedbackText.color = Color.red;
        }

        // Clear feedback after a short delay
        StartCoroutine(ClearFeedbackAndReturnToButtons());
    }

    IEnumerator ClearFeedbackAndReturnToButtons()
    {
        yield return new WaitForSeconds(2); // Wait for 2 seconds

        feedbackText.text = "";  // Clear feedback text
        unitButtonsContainer.SetActive(true);
        questionContainer.SetActive(false);
    }

    void SpawnUnit()
    {
        GameObject unitToSpawn = null;

        // Determine which unit to spawn based on the current difficulty level
        if (currentDifficulty == "easy")
        {
            unitToSpawn = easyUnitPrefab;
        }
        else if (currentDifficulty == "medium")
        {
            unitToSpawn = mediumUnitPrefab;
        }
        else if (currentDifficulty == "hard")
        {
            unitToSpawn = hardUnitPrefab;
        }

        // Instantiate the selected unit at the PlayerBase position
        if (unitToSpawn != null)
        {
            GameObject newUnit = Instantiate(unitToSpawn, GameObject.Find("PlayerBase").transform.position, Quaternion.identity);

            Vector3 position = newUnit.transform.position;
            position.z -= 0.01f * GameObject.FindGameObjectsWithTag("PlayerUnit").Length;  // Offset based on number of player units
            newUnit.transform.position = position;
        }
    }
}