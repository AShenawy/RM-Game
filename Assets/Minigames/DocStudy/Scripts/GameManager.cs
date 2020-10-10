using UnityEngine;

namespace Methodyca.Minigames.DocStudy
{
    [System.Serializable]
    public class Thread
    {
        public bool IsCompleted;
        public int Id;
        public string Title;
        public Post Post;
    }

    [System.Serializable]
    public class Post
    {
        public string ProfileName;
        public string ProfilePost;
        public Sprite ProfileImage;
        public Selection[] Selections;
    }

    [System.Serializable]
    public class Selection
    {
        public bool IsCorrect;
        public bool IsSelected;
        public string Text;
    }

    public class GameManager : Singleton<GameManager>
    {
        public static event System.Action<Post> OnPostInitiated = delegate { };
        public static event System.Action<Question> OnForumInitiated = delegate { };
        public static event System.Action<int, int> OnPostCompleted = delegate { };
        public static event System.Action<int, int> OnScoreUpdated;

        private Question _currentQuestion;
        private Thread _currentThread;
        private int _maxThreadToComplete = 3;
        private int _correctPostCount = 0;
        private int _correctlySelectedPostCount = 0;

        public void InitiateForumThread(Question question)
        {
            _currentQuestion = question;
            OnForumInitiated?.Invoke(question);
            OnPostCompleted?.Invoke(GetCompletedThreadCount(), _maxThreadToComplete);
        }

        public void HandlePostInitiation(Thread thread)
        {
            _currentThread = thread;
            OnPostInitiated?.Invoke(thread.Post);
        }

        public void HandlePostCompletion()
        {
            _currentThread.IsCompleted = true;
            OnPostCompleted?.Invoke(GetCompletedThreadCount(), _maxThreadToComplete);
        }

        public void GetFeedback()
        {
            var selections = _currentThread.Post.Selections;

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

        public int GetCompletedThreadCount()
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