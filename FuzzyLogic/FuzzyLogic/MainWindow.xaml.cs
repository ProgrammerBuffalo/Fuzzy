using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FuzzyLogic
{

    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private List<Rule> rules = new List<Rule>(); // база правил представленная в виде списка
        private Dictionary<string, int> deviations = new Dictionary<string, int>(); // словарь лингвистических переменных

        private int lastX = 0; // предыдущий угол смещения

        private int currentAngle = 0; // текущий угол крена

        public int CurrentAngle
        {
            get { return currentAngle; }
            set { currentAngle = value; OnPropertyChanged("CurrentAngle"); }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string propName) // данный метод предназначен для связывания поля сurrentAngle с элементом управления - TextBlock 
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propName));
        }

        public MainWindow()
        {
            InitializeComponent();

            deviations.Add("FL", -90); // Far Left
            deviations.Add("ML", -50); // Middle Left
            deviations.Add("LL", -25); // Low Left
            deviations.Add("ZI", 0); // Zero Impact
            deviations.Add("LR", 25); // Low Right
            deviations.Add("MR", 50); // Middle Right
            deviations.Add("FR", 90); // Far Right

            // Создание и добавление правил в базу (список)
            rules.Add(new Rule(deviations["FL"], Operation.OR, deviations["ZI"], deviations["FR"])); 
            rules.Add(new Rule(deviations["FR"], Operation.OR, deviations["ZI"], deviations["FL"]));
            rules.Add(new Rule(deviations["ML"], Operation.OR, deviations["ZI"], deviations["MR"])); 
            rules.Add(new Rule(deviations["MR"], Operation.OR, deviations["ZI"], deviations["ML"])); 
            rules.Add(new Rule(deviations["FL"], Operation.AND, deviations["ML"], deviations["FR"]));
            rules.Add(new Rule(deviations["FR"], Operation.AND, deviations["MR"], deviations["FL"]));
            rules.Add(new Rule(deviations["ML"], Operation.AND, deviations["FR"], deviations["MR"]));
            rules.Add(new Rule(deviations["MR"], Operation.AND, deviations["FR"], deviations["ML"]));
            rules.Add(new Rule(deviations["LL"], Operation.AND, deviations["ML"], deviations["LR"]));
            rules.Add(new Rule(deviations["LR"], Operation.AND, deviations["MR"], deviations["LL"]));
            rules.Add(new Rule(deviations["ZI"], Operation.OR, deviations["FL"], deviations["ZI"]));
            rules.Add(new Rule(deviations["ZI"], Operation.OR, deviations["FR"], deviations["ZI"]));
        }


        private double MuFunction(int fuzz_error, int rule_fuzz_error) // Функция принадлежности
        {
            return Math.Round(Math.Exp(-(Math.Pow(fuzz_error - rule_fuzz_error, 2) / (2 * Math.Pow(25, 2)))), 5);
        }

        private double MakeDecision(int fuzz_error, int previous_fuzz_error) // Функция принимающая решение
        {
            double sum_alpha = 0;
            double sum = 0;

            foreach (Rule rule in rules) // цикл пробегающий по всем правилам существующие в базе правил, происходит нечёткий вывод по каждому из правил по отдельности
            {
                double mu_e = MuFunction(fuzz_error, rule.FuzzifierError);
                double mu_de = MuFunction(fuzz_error - previous_fuzz_error, fuzz_error - rule.PreviousFuzzifierError);

                /* Проверка операции правила, если операция является коньюкцией, то берется минимальное среди Mu(текущей ошибки) и Mu(дельта ошибки)
                   Если операция является дизъюкцией, то берется максимальное среди Mu(текущей ошибки) и Mu(дельта ошибки)                
                 */
                double alpha = rule.Operation == Operation.AND ? Math.Min(mu_e, mu_de) : Math.Max(mu_e, mu_de);

                // сложение результирующих функций
                sum_alpha += alpha * rule.Defuzzifier;  
                sum += alpha;
            }

            return sum_alpha / sum; // Получение в виде числового значения четкое решение (Метод центра тяжести)
        }

        private void btn_make_Click(object sender, RoutedEventArgs e)
        {
            lastX = 0;
            CurrentAngle = 0;
            CurrentAngle = int.Parse(textBox_angle.Text); // преобразование введенного значения в целочисленный тип, и передача в свойство CurrentAngle
            BeginCalculation(); // вызов ассинхронного метода
        }

        private async void BeginCalculation()
        {
            var animation = new DoubleAnimation(); // Создание анимации
            btn_make.IsEnabled = false;
            // создание потока
            await Task.Factory.StartNew(() =>
            {
                while (currentAngle != 0)
                {
                    
                    int x = currentAngle;
                    CurrentAngle = (int)MakeDecision(x, x - lastX);
                    lastX = x;

                    // Инициализация и вызов анимации
                    Dispatcher.Invoke(() => {
                        animation.From = lastX;
                        animation.To = CurrentAngle;
                        animation.Duration = TimeSpan.FromMilliseconds(1000);
                        rotate.BeginAnimation(RotateTransform.AngleProperty, animation); });

                    Thread.Sleep(1000);
                }

                Dispatcher.Invoke(() => { btn_make.IsEnabled = true; });
            }
            );
        }
    }


}
