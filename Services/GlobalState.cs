namespace Indego.Services
{
    public class GlobalState
    {
        private bool canGoBack;

        public bool CanGoBack
        {
            get => canGoBack;
            set
            {
                canGoBack = value;
                NotifyStateChanged();
            }
        }

        private string pageTitle = "Indego";

        public string PageTitle
        {
            get => pageTitle;
            set
            {
                pageTitle = value;
                NotifyStateChanged();
            }
        }



        public event Action? OnChange;

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}