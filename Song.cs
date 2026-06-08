using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zd1_Romanov_Shop
{
    struct Song
    {
        
        private string author;
        private string title;
        private string filename;

        public string Author
        {
            get { return author; }
            set { author = value; }
        }
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        public string Filename
        {
            get { return filename; }
            set { filename = value; }
        }
    }
}
