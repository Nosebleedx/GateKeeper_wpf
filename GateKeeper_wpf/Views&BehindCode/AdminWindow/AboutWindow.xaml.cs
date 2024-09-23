using System.Windows.Controls;

namespace GateKeeper_wpf.Views_BehindCode.AdminWindow
{
    /// <summary>
    /// Логика взаимодействия для AboutWindow.xaml
    /// </summary>
    public partial class AboutWindow : Page
    {
        public AboutWindow()
        {
            InitializeComponent();
            txtAboutAuthor.Text = "Силаков Дмитрий";
            txtAboutQuest.Text = "ЛАБОРАТОРНАЯ РАБОТА 1. РАЗРАБОТКА ПО РАЗГРАНИЧЕНИЯ ПОЛНОМОЧИЙ ПОЛЬЗОВАТЕЛЕЙ";
        }
    }
}
