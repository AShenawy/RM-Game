using UnityEngine;

namespace Methodyca.Minigames.DocStudy
{
    [System.Serializable]
    public class Thread
    {
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
        public static event System.Action<int, int> OnScoreUpdated = delegate { };

        private int _correctPostCount = 0;
        private int _correctlySelectedPostCount = 0;
        private Thread _currentThread;
        private Question _currentQuestion;

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

        public void HandleSelectedPostCompletion()
        {
            _currentThread.IsCompleted = true;
            OnForumInitiated?.Invoke(_currentQuestion);
            OnPostCompleted?.Invoke(GetCompletedThreadCount(), _maxThreadToComplete);
        }

        public void GetFeedback()
        {
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

            OnScoreUpdated?.Invoke(_correctlySelectedPostCount, _correctPostCount);
        }

        public void ResetGame()
        {
            _correctPostCount = 0;
            _correctlySelectedPostCount = 0;
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
    }
}