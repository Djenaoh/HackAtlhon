﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projet
{
    [Serializable()]
    public class Item
    {
        //attributs
        string title;
        string description;
        KeyValuePair<int, string> lstGenders;
        int year;
        int rating;
        string gender = "None";
        string image;
        List<List<string>> lstReviews;
        List<string> lstDirectors; 
        List<string> lstWriters;
        List<string> lstStars;
        DateTime date;
         //get / set
        public string Title { get => title; set => title = value; }
        public string Description { get => description; set => description = value; }
        public int Rating { get => rating; set => rating = value; }
        public string Image { get => image; set => image = value; }
        public int Year { get => year; set => year = value; }
        public List<List<string>> LstReviews { get => lstReviews; set => lstReviews = value; }
        public KeyValuePair<int, string> LstGenders { get => lstGenders; set => lstGenders = value; }
        public DateTime Date { get => date; set => date = value; }
        public List<string> LstDirectors { get => lstDirectors; set => lstDirectors = value; }
        public List<string> LstWriters { get => lstWriters; set => lstWriters = value; }
        public List<string> LstStars { get => lstStars; set => lstStars = value; }
        public string Gender { get => gender; set => gender = value; }
        
        public Item()
        {
            Date = DateTime.Now;
        }

        public Item(string title, string description, int rating, string image, int year, List<List<string>> lstReviews, KeyValuePair<int, string> lstGenders, DateTime date, List<string> lstDirectors, List<string> lstWriters, List<string> lstStars)
        {
            Title = title;
            Description = description;
            Rating = rating;
            Image = image;
            Year = year;
            LstReviews = lstReviews;
            LstGenders = lstGenders;
            Date = date;
            LstDirectors = lstDirectors;
            LstWriters = lstWriters;
            LstStars = lstStars;
            Date = DateTime.Now;
        }
        //methodes d'ajout
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
            this.Image = e;
            return this;
        }
        public Item addReviews(params List<string>[] e)
        {
            if (this.LstReviews == null)
            {
                lstReviews = new List<List<string>>();
            }
            for (int i = 0; i < e.Length; i++)
            {
                this.LstReviews.Add(e[i]);
            }
            return this;
        }
        public Item addGenres(KeyValuePair<int, string> e)
        {
            lstGenders = e;
            gender = e.Value;
            return this;
        }
        public Item addDirectors(params string[] e)
        {
            if (this.LstDirectors == null)
            {
                lstDirectors = new List<string>();
            }
            for (int i = 0; i < e.Length; i++)
            {
                this.LstDirectors.Add(e[i]);
            }
            return this;
        }
        public Item addWriters(params string[] e)
        {
            if (this.LstWriters == null)
            {
                lstWriters = new List<string>();
            }
            for (int i = 0; i < e.Length; i++)
            {
                this.LstWriters.Add(e[i]);
            }
            return this;
        }
        public Item addStars(params string[] e)
        {
            if (this.LstStars == null)
            {
                lstStars = new List<string>();
            }
            for (int i = 0; i < e.Length; i++)
            {
                this.LstStars.Add(e[i]);
            }
            return this;
        }
        public Item addYear(int e)
        {
            this.Year = e;
            return this;
        }
        public Item addRating(int e)
        {
            this.Rating = e;
            return this;
        }
    }
    
}
