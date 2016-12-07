using System.Windows;

namespace BooyerMooreGUI
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void confirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (patternTextBox.Text.Trim()==string.Empty || haystackTextBox.Text.Trim() == string.Empty)
            {
                return;
            }

            BoyerMoore.BoyerMoore bm = new BoyerMoore.BoyerMoore(patternTextBox.Text, haystackTextBox.Text);

            if (bm.IndexOf == -1) resultInformationTextBlock.Text = "Nije pronađen uzorak";
            else resultInformationTextBlock.Text = "Uzorak je pronađen s početnim indeksom "+bm.IndexOf +". \n Broj usporedbi: ="+bm.numberOfComparisons;
        }
    }
}
