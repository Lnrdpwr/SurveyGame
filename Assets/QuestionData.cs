using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/QuestionData", order = 1)]
public class QuestionData : ScriptableObject
{
    [System.Serializable]
    public struct Question
    {
        public string QuestionText;
        public string[] Answers;
        public int RightAnswerIndex;
    }

    public Question[] Questions;
}
