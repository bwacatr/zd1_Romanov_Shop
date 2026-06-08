using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace zd1_Romanov_Shop
{
    class Playlist
    {
        private List<Song> list;
        private int currentIndex;

        public int CurrentIndex
        {
            get { return currentIndex; }
        }

        public Playlist()
        {
            list = new List<Song>();
            currentIndex = 0;
        }

        public Song CurrentSong() // получение текущей песни
        {
            if (list.Count > 0)
                return list[currentIndex];
            else
                throw new IndexOutOfRangeException("Невозможно получить текущую аудиозапись для пустого плейлиста!");
        }

        public void NextSong() // переход на следующую песню
        {
            if (currentIndex >= list.Count - 1)
                currentIndex = 0;
            else
                currentIndex++;
        }

        public void PreviousSong() // переход на предыдущую песню
        {
            if (currentIndex == 0)
                currentIndex = list.Count - 1;
            else
                currentIndex--;
        }

        public void ReturnToBeginning() // переход на предыдущую песню
        {
            currentIndex = 0;
        }

        public void ClearPlaylist() // очистка плейлиста
        {
            list.Clear();
        }

        public void DeleteSong() // удаление песни по индексу
        {
            if (list.Count > 0)
            {
                list.RemoveAt(currentIndex);
                ReturnToBeginning();
            }
            
        }

        public void DeleteSong(Song song) // удаление песни по самой песне
        {
            list.Remove(song);
        }

        public void SelectByIndex(int index) // выбор песни по индексу
        {
            
            if (index > list.Count - 1)
            {
                throw new IndexOutOfRangeException("Невозможно получить аудиозапись по индексу");
            }
            else if (index < 0)
            {
                throw new IndexOutOfRangeException("Невозможно получить аудиозапись по индексу");
            }
            else
            {
                currentIndex = index;
            }

        }



        public bool ContainsByFileName(string filename) // метод, который находит песню по пути к файлу
        {
            foreach (var item in list)
            {
                if (item.Filename == filename)
                {
                    return true;
                }
            }
            return false;
        }

        public int FindIndexByFileName(string filename) // метод, который находит индекс песню по пути к файлу
        {
            int index = 0;
            foreach (var item in list)
            {
                if (item.Filename == filename)
                {
                    return index;
                }
                index++;
            }
            return -1;
        }



        public void AddSong(string title, string authorName, string fileName) // создает и добавляет песню в список
        {
            if (File.Exists(fileName))
            {
                Song song = new Song { Title = title, Author = authorName, Filename = fileName };
                AddSong(song);
                
            }
            
        }

        public void AddSong(Song song) // добавляет песню в список
        {
            list.Add(song);
            
        }

        public List<Song> List
        {
            get { return list; }
        }
    }
}
