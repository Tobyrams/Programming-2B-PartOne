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
using System.Windows.Shapes;
using PARTONE;

namespace PARTONE
{
    /// <summary>
    /// Interaction logic for ListOfModules.xaml
    /// </summary>
    public partial class ListOfModules : Window
    {
        public ListOfModules()
        {
            InitializeComponent();
        }

        private void ListModuleBack_Click(object sender, RoutedEventArgs e)
        {
            this.Hide();
        }
    }
}
