namespace AnySocialNetwork.Models
{
    public class Post
    {
        public Post(string content, string userId)
        {
            Content = content;
            UserId = userId;
        }
        
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
    }
}