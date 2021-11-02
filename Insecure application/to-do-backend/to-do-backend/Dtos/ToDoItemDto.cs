namespace to_do_backend.Dtos
{
    public class ToDoItemDto
    {
        public int id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        public bool isDone { get; set; }
        public string username { get; set; }
        public int userId { get; set; }
    }
}
