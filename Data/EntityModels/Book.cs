using System;
using System.Collections.Generic;

namespace BookCave.Data.EntityModels
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Author { get; set; }
        public string Image { get; set; }
        public double Price { get; set; }
        public Genre Genre { get; set; }
        public DateTime ReleaseDate { get; set; }
        public double Rating { get; set; }
    }
}