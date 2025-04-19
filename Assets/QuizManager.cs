using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class QuizManager : MonoBehaviour
{
    [SerializeField] private QuestionData questions;
    [SerializeField] private Button[] buttons;
    [SerializeField] private TMP_Text questionText;
    [SerializeField] private TMP_Text questionsCounter;
    [SerializeField] private TMP_Text correctAnswersCounter;
    [SerializeField] private GameObject resultPanel;
    [SerializeField] private GameObject buttonsGroup;
    [SerializeField] private TMP_Text resultText;

    [Space]

    [SerializeField] private AudioClip rightAnswer;
    [SerializeField] private AudioClip wrongAnswer;

    private AudioSource source;
    private int questionIndex = 0;
    private int scores = 0;

    private void Start()
    {
        source = GetComponent<AudioSource>();
        UpdateButtons();
    }

    void UpdateButtons()
    {
        questionText.text = questions.Questions[questionIndex].QuestionText;
        questionsCounter.text = $"{questionIndex + 1}/100";

        foreach(Button button in buttons)
        {
            button.onClick.RemoveAllListeners();
        }

        for(int i = 0; i < buttons.Length; i++)
        {
            int index = i;

            buttons[i].onClick.AddListener(()=> CheckReply(index));
            buttons[i].GetComponentInChildren<TMP_Text>().text = questions.Questions[questionIndex].Answers[i];
        }
    }

    void CheckReply(int index)
    {
        source.Stop();
        if(index == questions.Questions[questionIndex].RightAnswerIndex)
        {
            source.PlayOneShot(rightAnswer);
            scores++;
            correctAnswersCounter.text = $"Правильных ответов: {scores}";
        }
        else
        {
            source.PlayOneShot(wrongAnswer);
        }

        questionIndex++;

        if(questionIndex >= questions.Questions.Length)
        {
            Result();
        }
        else
        {
            UpdateButtons();
        }
    }

    void Result()
    {
        questionText.gameObject.SetActive(false);
        buttonsGroup.SetActive(false);
        correctAnswersCounter.gameObject.SetActive(false);
        questionsCounter.gameObject.SetActive(false);

        resultPanel.SetActive(true);

        switch (scores) {
            case < 40:
                resultText.text = $"Ваш результат: {scores}/100\nСкорее всего вы были двоечником";
                break;
            case < 60:
                resultText.text = $"Ваш результат: {scores}/100\nСкорее всего вы были троечником";
                break;
            case < 80:
                resultText.text = $"Ваш результат: {scores}/100\nСкорее всего вы были хорошистом";
                break;
            case <= 100:
                resultText.text = $"Ваш результат: {scores}/100\nСкорее всего вы были отличником";
                break;
        }
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
