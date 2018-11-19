namespace TOT.Web.ViewModels
{
    public class DialogViewModel
    {
        public DialogViewModel(string id)
        {
            Id = id;
        }

        public string Id { get; }
        public string Title { get; set; } = "Confirmation";
        public string Content { get; set; }
        public string SubmitAction { get; set; }
        public string SubmitController { get; set; }

    }
}

