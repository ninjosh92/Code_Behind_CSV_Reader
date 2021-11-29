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

namespace Code_Behind_CSV_Reader.User_Controls
{
    /// <summary>
    /// Interaction logic for DropCanvas.xaml
    /// </summary>
    public partial class DropCanvas : UserControl
    {
        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsChildHitTestVisibleProperty =
            DependencyProperty.Register("MyProperty", typeof(bool), typeof(DropCanvas), new PropertyMetadata(true));

        public bool IsChildHitTestVisible
        {

            get { return (bool)GetValue(IsChildHitTestVisibleProperty); }
            set { SetValue(IsChildHitTestVisibleProperty, value); }

        }

        public DropCanvas()
        {
            InitializeComponent();
        }

        private void dropCanvas_DragLeave(object sender, DragEventArgs e)
        {
            object data = e.Data.GetData(DataFormats.Serializable);

            if (data is UIElement element)
            {
                dropCanvas.Children.Remove(element);
            }
        }

        private void dropCanvas_Drop(object sender, DragEventArgs e)
        {

        }

        private void dropCanvas_DragOver(object sender, DragEventArgs e)
        {
            object data = e.Data.GetData(DataFormats.Serializable);

            if (data is UIElement element)
            {
                Point dropPosition = e.GetPosition(dropCanvas);

                Canvas.SetLeft(element, dropPosition.X);
                Canvas.SetTop(element, dropPosition.Y);

                if (!dropCanvas.Children.Contains(element))
                {
                    dropCanvas.Children.Add(element);
                }

            }
        }
    }
}
