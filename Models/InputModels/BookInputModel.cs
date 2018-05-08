using System;

namespace BookCave.Models.InputModels
{
    public class BookInputModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public string Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double Rating { get; set; }
        public string Description { get; set; }
    }
}