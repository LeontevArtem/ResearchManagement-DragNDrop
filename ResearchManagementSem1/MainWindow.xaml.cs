using System;
using System.Collections.Generic;
using System.Linq;
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

namespace ResearchManagementSem1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private object movingObject;
        private double firstXPos, firstYPos;
        private void MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            
            Image img = sender as Image;// получаем картинку, на которую мы кликнули
            Canvas canvas = img.Parent as Canvas;//Получаем канвас, на котром эта картинка

            firstXPos = e.GetPosition(img).X; //Получаем исходные координаты картинки
            firstYPos = e.GetPosition(img).Y;

            movingObject = sender; //Запоминаем выбранный объект

            int top = Canvas.GetZIndex(img); //Получаем слой картинки на канвасе
            foreach (Image child in canvas.Children)
                if (top < Canvas.GetZIndex(child))
                    top = Canvas.GetZIndex(child);
            Canvas.SetZIndex(img, top + 1);//Ставь картинку на самый верхний слой
        }
        
        private void MouseMove(object sender, MouseEventArgs e)
        {
            //Обработчик срабатывает если нажата кнопка мыши и ыбранный обработчиком объект совпадает с объектом,
            //на котором произошел клик мышкой
            if (e.LeftButton == MouseButtonState.Pressed && sender == movingObject)
            {
                Image img = sender as Image;
                Canvas canvas = img.Parent as Canvas;

                double newLeft = e.GetPosition(canvas).X - firstXPos;//Расчет новых координат относительно старых с учетом движения мыши
                img.SetValue(Canvas.LeftProperty, newLeft);//Установка новых координат

                double newTop = e.GetPosition(canvas).Y - firstYPos;
                img.SetValue(Canvas.TopProperty, newTop);
            }
        }
    }
}
