using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Security;


namespace zd1_Romanov_Shop
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Shop shop;
        Playlist playlist;
        public MainWindow()
        {
            InitializeComponent();
            shop = new Shop();
            playlist = new Playlist();

        }
        private void updateGrid() // Обновляет Grid.
        {
            Grid.Items.Clear();
            foreach (var item in shop.products.Keys)
            {
                if (item.Amount != 0)
                {
                    Grid.Items.Add(item);
                }

            }
            gains_label.Content = $"Прибыль: {shop.Gain}";
        }

        private void Button_Click(object sender, RoutedEventArgs e) // Кнопка Добавить. Добавляет в Grid новый продукт.
        {

            if (!string.IsNullOrWhiteSpace(ProductName.Text))
            {
                if (decimal.TryParse(ProductPrice.Text, out decimal price))
                {
                    if (price > 0)
                    {
                        if (int.TryParse(ProductAmount.Text, out int amount))
                        {
                            if (amount > 0)
                            {
                                if (shop.FindByName(ProductName.Text) == null)
                                {
                                    shop.CreateProduct(ProductName.Text, price, amount);

                                    updateGrid();
                                }
                                else
                                {
                                    MessageBox.Show("Указанный продукт уже существует");
                                }
                                



                            }
                            else
                            {
                                MessageBox.Show("Введено некорректное кол-во");
                            }

                        }
                        else
                        {
                            MessageBox.Show("Введено некорректное кол-во");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Введено некорректная цена");
                    }


                }
                else
                {
                    MessageBox.Show("Введено некорректная цена");
                }
            }
            else
            {
                MessageBox.Show("Заполните поле с названием товара");
            }

        }

        private void Task1_Click(object sender, RoutedEventArgs e) // скрывает или показывает панель магазина
        {
            if (Task1_Panel.Visibility == Visibility.Collapsed)
            {
                Task1_Panel.Visibility = Visibility.Visible;
            }
            else
            {
                Task1_Panel.Visibility = Visibility.Collapsed;
            }

        }

        private void Task2_Click(object sender, RoutedEventArgs e) // скрывает или показывает прибыль
        {
            if (Task1_Gains_Panel.Visibility == Visibility.Collapsed)
            {
                Task1_Gains_Panel.Visibility = Visibility.Visible;
            }
            else
            {
                Task1_Gains_Panel.Visibility = Visibility.Collapsed;
            }
        }

        private void Task3_Click(object sender, RoutedEventArgs e) // скрывает или показывает плейлист
        {
            if (Task3_Panel.Visibility == Visibility.Collapsed)
            {
                Task3_Panel.Visibility = Visibility.Visible;
            }
            else
            {
                Task3_Panel.Visibility = Visibility.Collapsed;
            }
        }

        private void Sell_Click(object sender, RoutedEventArgs e) // Кнопка Продать. Продает выбранные продукты и обновляет Grid
        {
            foreach (var item in Grid.SelectedItems)
            {
                shop.Sell((Product)item);
            }

            updateGrid();

        }

        private void Search_Click(object sender, RoutedEventArgs e) // Кнопка Поиск. Очищает Grid, а после добавляет в Grid все найденные элементы shop.products по названию
        {
            if (!string.IsNullOrWhiteSpace(ProductName.Text))
            {
                Grid.Items.Clear();
                foreach (var item in shop.FindMultipleByName(ProductName.Text))
                {
                    Grid.Items.Add(item);
                }
            }
            else
            {
                updateGrid();
            }


        }

        public void updatePlaylistGrid() // обновление плейлиста
        {
            Grid_Playlist.Items.Clear();
            foreach (var item in playlist.List)
            {
                Grid_Playlist.Items.Add(item);
            }
        }

        private void Add_Song_Click(object sender, RoutedEventArgs e) // добавление песни с проверкой на наличие песни с этим же файлом
        {
            if (!string.IsNullOrWhiteSpace(SongTitle.Text))
            {
                if (!string.IsNullOrWhiteSpace(AuthorName.Text))
                {
                    if (!string.IsNullOrWhiteSpace(FileName.Text))
                    {
                        if (playlist.ContainsByFileName(FileName.Text) == false)
                        {
                            playlist.AddSong(SongTitle.Text, AuthorName.Text, FileName.Text);
                            updatePlaylistGrid();

                            
                        }
                        else
                        {
                            MessageBox.Show("Данный файл уже находится в плейлисте");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Введите путь к файлу");
                    }
                }
                else
                {
                    MessageBox.Show("Введите имя автора");
                }
            }
            else
            {
                MessageBox.Show("Введите название песни");
            }
        }

        private void Search_Song_Click(object sender, RoutedEventArgs e) // переход к песне по индексу
        {
            try
            {
                if (playlist != null)
                {
                    playlist.SelectByIndex(int.Parse(Index_TextBox.Text));
                    Grid_Playlist.SelectedIndex = playlist.CurrentIndex;
                }
            }
            catch (Exception)
            {
                MessageBox.Show("Некорректный индекс");
            }
            
        }

        private void Previous_Click(object sender, RoutedEventArgs e) // переход к предыдущей песне
        {
            if (playlist != null)
            {
                playlist.PreviousSong();
                Grid_Playlist.SelectedIndex = playlist.CurrentIndex;
            }
        }

        private void Next_Click(object sender, RoutedEventArgs e) // переход к следующей песне
        {
            if (playlist != null)
            {
                playlist.NextSong();
                Grid_Playlist.SelectedIndex = playlist.CurrentIndex;
            }
        }

        private void Return_Click(object sender, RoutedEventArgs e) // возвращение в начало плейлиста
        {
            if (playlist != null)
            {
                playlist.ReturnToBeginning();
                Grid_Playlist.SelectedIndex = playlist.CurrentIndex;
            }
        }

        private void Clear_Playlist_Click(object sender, RoutedEventArgs e) // кнопка очистки плейлиста
        {
            if (playlist != null)
            {
                playlist.ClearPlaylist();
                updatePlaylistGrid();
            }
        }

        private void Delete_Song_Click(object sender, RoutedEventArgs e) // кнопка удаления песни
        {
            if (playlist != null)
            {
                playlist.DeleteSong();
                updatePlaylistGrid();
            }
        }

        
    }
}
