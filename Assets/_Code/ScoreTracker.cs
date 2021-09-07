using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Code
{
    public class ScoreTracker : MonoBehaviour
    {
        public static ScoreTracker instance;
        public Text Textbox;
        public int score;

        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }

            score = 0;
            Textbox.text = score.ToString();
        }

        public void EnemyKilled()
        {
            score++;
            Textbox.text = score.ToString();
        }
    }
}