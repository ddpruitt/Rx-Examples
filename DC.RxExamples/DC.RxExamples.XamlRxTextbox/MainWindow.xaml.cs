using System;
using System.Linq;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using System.Threading;
using System.Windows;

namespace DC.RxExamples.XamlRxTextbox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void RunRx_Click(object sender, RoutedEventArgs e)
        {
            
            var query = Enumerable.Range(0, 100).Select(DelayNumber);

            var observableQuery = query.ToObservable(ThreadPoolScheduler.Instance);

            observableQuery.ObserveOn(Dispatcher).Subscribe(AppendToTextBox);
            observableQuery.ObserveOn(Dispatcher).Subscribe(AppendToTextBox2);
        }

        private int DelayNumber(int number)
        {
            if(number<100) Thread.Sleep(100);
            if(number>=100 && number<500) Thread.Sleep(50);
            if(number>=500) Thread.Sleep(10);

            return number;
        }

        private void AppendToTextBox(int number)
        {
            RxTarget.AppendText($"{number:000}\n");
        }

        private void AppendToTextBox2(int number)
        {
            RxTarget2.AppendText($"{number:000}\n");
        }
    }
}
