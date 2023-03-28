namespace Board.Enties
{
    public class Comment
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public User Author { get; set; }
        public Guid AuthorID { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public WorkItem WorkItem { get; set; }

        public int WorkItemId { get; set; }
    }
}
