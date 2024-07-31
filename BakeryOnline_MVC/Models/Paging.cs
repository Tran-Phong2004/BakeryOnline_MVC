namespace BakeryOnline_MVC.Models
{
    public class Paging<T>
    {
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }

        public List<T> Result { get; set; }
    }
}
