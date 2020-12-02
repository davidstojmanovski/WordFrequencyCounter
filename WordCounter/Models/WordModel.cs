using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WordCounter.Models
{
    public class WordModel : IComparable
    {

        public WordModel(string word,int wordCount)
        {
            this.word = word;
            this.wordCount = wordCount;
        }
        public string word{get; set;}
        public int wordCount { get; set; }

     

        public int CompareTo(object obj)
        {
            WordModel m = (WordModel)obj;


            if (wordCount < m.wordCount) return -1;
            else if (wordCount >m.wordCount) return 1;
            else return 0;
        }
    }
}