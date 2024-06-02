namespace Assignment2.Models
{
    public class RequestResponse<T>
    {
        public bool Success { get; set; }

        public string Message { get; set; }

        public T item { get; set; }

        public List<T> items { get; set; }
    }
}
