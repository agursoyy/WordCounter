using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Word
    {
        private string wordName;
        private int count;

        public Word(String text, int count)
        {
            wordName = text;
            this.count = count;
        }

        public string getWordName() { return wordName; }
        public int getCount() { return count; }
    }
    
}
