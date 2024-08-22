using UnityEngine;

namespace Methodyca.Minigames.DocStudy
{
    [System.Serializable]
    public class Thread
    {
        public bool IsCorrect;
        public bool IsCompleted;
        public int Id;
        [TextArea(1, 3)] public string Title;
        public Post[] Posts;
    }

    [System.Serializable]
    public class Post
    {
        public bool IsCorrect;
        public bool IsSelected;
        public Sprite ProfileImage;
        public string Name;
        [TextArea(2, 4)] public string Message;
    }

    public class GameManager : Singleton<GameManager>
    {
        private const int _maxThreadToComplete = 3;

        public static event System.Action<string, Thread> OnPostInitiated = delegate { };
        public static event System.Action<Question> OnForumInitiated = delegate { };
        public static event System.Action<int, int> OnPostCompleted = delegate { };
        public static event System.Action<(int, int, int, int)> OnFeedbackInitiated = delegate { };
        public static event System.Action OnRestartGame = delegate { };

        [SerializeField] private Question[] questions;

        private int _correctPostCount = 0;
        private int _correctlySelectedPostCount = 0;
        private int _correctlySelectedThreadCount = 0;
        private Thread _currentThread;
        private Question _currentQuestion;
        private (int SelectedCorrectPosts, int TotalCorrectPosts, int SelectedCorrectThreads, int TotalCorrectThreads) _score;

        public void InitiateForumThread(Question question)
        {
            _currentQuestion = question;
            OnForumInitiated?.Invoke(question);
            OnPostCompleted?.Invoke(GetCompletedThreadCount(), _maxThreadToComplete);
        }

        public void HandlePostInitiation(Thread thread)
        {
            _currentThread = thread;
            OnPostInitiated?.Invoke(_currentQuestion.Title, thread);
        }

        public void HandlePostCompletion()
        {
            _currentThread.IsCompleted = true;

            var selections = _currentThread.Posts;

            for (int i = 0; i < selections.Length; i++)
            {
                if (selections[i].IsCorrect)
                {
                    _correctPostCount++;

                    if (selections[i].IsSelected)
                    {
                        _correctlySelectedPostCount++;
                    }
                }
            }

            OnPostCompleted?.Invoke(GetCompletedThreadCount(), _maxThreadToComplete);
        }

        public void GetFeedback()
        {
            var threads = _currentQuestion.Threads;

            for (int i = 0; i < threads.Length; i++)
            {
                if (threads[i].IsCorrect && threads[i].IsCompleted)
                {
                    _correctlySelectedThreadCount++;
                }
            }

            _score = (_correctlySelectedPostCount, _correctPostCount, _correctlySelectedThreadCount, _maxThreadToComplete);
            OnFeedbackInitiated?.Invoke(_score);
        }

        private int GetCompletedThreadCount()
        {
            int completedCount = 0;

            foreach (var thread in _currentQuestion.Threads)
            {
                if (thread.IsCompleted)
                {
                    completedCount++;
                }
            }

            return completedCount;
        }

        public Question GetCurrentQuestion()
        {
            return _currentQuestion;
        }


        public void ResetData()
        {
            _correctPostCount = 0;
            _correctlySelectedPostCount = 0;
            _correctlySelectedThreadCount = 0;

            for (int i = 0; i < questions.Length; i++)
            {
                for (int j = 0; j < questions[i].Threads.Length; j++)
                {
                    questions[i].Threads[j].IsCompleted = false;
                    for (int k = 0; k < questions[i].Threads[j].Posts.Length; k++)
                    {
                        questions[i].Threads[j].Posts[k].IsSelected = false;
                    }
                }
            }
        }

        private void OnDisable()
        {
            ResetData();
        }
    }
}