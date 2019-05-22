using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet
{
    public class Item
    {

        string title;
        string description;
        List<EnumGenre> gender;
        int year;
        int rate;
        string image;
        List<string> review;
        DateTime date;

        public string Title { get => title; set => title = value; }
        public string Description { get => description; set => description = value; }
        public int Rate { get => rate; set => rate = value; }
        public string Image { get => image; set => image = value; }
        public int Year { get => year; set => year = value; }
        public List<string> Review { get => review; set => review = value; }
        public List<EnumGenre> Gender { get => gender; set => gender = value; }
        public DateTime Date { get => date; set => date = value; }

        public Item()
        {
            Date = DateTime.Now;
        }

        public Item(string title, string description, int rate, string image, int year, List<string> review, List<EnumGenre> gender)
        {
            Title = title;
            Description = description;
            Rate = rate;
            Image = image + MainWindow.PATH;
            Year = year;
            Review = review;
            Gender = gender;
            Date = DateTime.Now;
        }

        public Item addTitle(string e)
        {
            this.Title = e;
            return this;
        }
        public Item addDescription(string e)
        {
            this.Description = e;
            return this;
        }
        public Item addImage(string e)
        {
            this.Image = e + MainWindow.PATH;
            return this;
        }
        public Item addReview(params string[] e)
        {
            if (this.Review == null)
            {
                review = new List<string>();
            }
            for (int i = 0; i < e.Length; i++)
            {
                this.Review.Add(e[i]);
            }
            return this;
        }
        public Item addGerne(params EnumGenre[] e)
        {
            if (this.Gender == null)
            {
                gender = new List<EnumGenre>();
            }
            for (int i = 0; i < e.Length; i++)
            {
                this.Gender.Add(e[i]);
            }
            return this;
        }
        public Item addYear(int e)
        {
            this.Year = e;
            return this;
        }
        public Item addRate(int e)
        {
            this.Rate = e;
            return this;
        }

        public override string ToString()
        {
            return this.Title;
        }
    }
}
