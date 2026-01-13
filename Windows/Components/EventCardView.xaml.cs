using Nodefall.GameLogic.Event;
using Nodefall.Models;
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

namespace Nodefall.Windows.Components
{
    /// <summary>
    /// Interaction logic for EventCardView.xaml
    /// </summary>
    public partial class EventCardView : UserControl
    {
        public EventCard EventCard { get; }
        public event Action<EventCard>? CardSelected;

        public bool IsSelected
        {
            get => (bool)GetValue(IsSelectedProperty);
            set => SetValue(IsSelectedProperty, value);
        }

        public static readonly DependencyProperty IsSelectedProperty = DependencyProperty.Register(nameof(IsSelected),
                                                                                                   typeof(bool),
                                                                                                   typeof(EventCardView),
                                                                                                   new PropertyMetadata(null));

        public EventCardView(EventCard card)
        {
            EventCard = card;
            IsSelected = false;
            InitializeComponent();
            DataContext = this;
        }

        private void Card_Click(object sender, RoutedEventArgs e)
        {
            CardSelected?.Invoke(EventCard);
        }
    }
}
