namespace TaskBlog.ViewModels
{
    public class TagViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public char AlphabetLetter { get { return (Name != "") ? Name[0] : ' '; } }
    }
}