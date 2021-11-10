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
    /// Interaction logic for DragMenu.xaml
    /// </summary>
    public partial class DragMenu : UserControl
    {

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty IsChildHitTestVisibleProperty =
            DependencyProperty.Register("MyProperty", typeof(bool), typeof(DragMenu), new PropertyMetadata(true));

        public bool IsChildHitTestVisible
        {
            get { return (bool)GetValue(IsChildHitTestVisibleProperty); }
            set { SetValue(IsChildHitTestVisibleProperty, value); }
        }

        public DragMenu()
        {
            InitializeComponent();
        }

        private void testRectangle_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
            {
                IsChildHitTestVisible = false; 
                DragDrop.DoDragDrop(testRectangle, new DataObject(DataFormats.Serializable, testRectangle), DragDropEffects.Move);
                IsChildHitTestVisible = true; 
            }

        }

        private void dragCanvas_DragLeave(object sender, DragEventArgs e)
        {
            object data = e.Data.GetData(DataFormats.Serializable);

            if (data is UIElement element)
            {
                dragCanvas.Children.Remove(element);
            }
        }

        private void dragCanvas_DragOver(object sender, DragEventArgs e)
        {
            object data = e.Data.GetData(DataFormats.Serializable);

            if (data is UIElement element)
            {
                Point dropPosition = e.GetPosition(dragCanvas);

                Canvas.SetLeft(element, dropPosition.X);
                Canvas.SetTop(element, dropPosition.Y);

                if (!dragCanvas.Children.Contains(element))
                {
                    dragCanvas.Children.Add(element);
                }

               
            }
        }

        private void dragCanvas_Drop(object sender, DragEventArgs e)
        {

        }
    }
}
