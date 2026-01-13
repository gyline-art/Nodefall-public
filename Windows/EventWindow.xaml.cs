using Nodefall.GameLogic;
using Nodefall.GameLogic.Event;
using Nodefall.Windows.Components;
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

namespace Nodefall.Windows
{
    /// <summary>
    /// Interaction logic for EventWindow.xaml
    /// </summary>
    public partial class EventWindow : Window
    {
        private EventCard _card;
        private bool _locked = false;
        public EventCard SelectedCard 
        {
            get => _card; 
            set
            {
                if (!_locked)
                {
                    _card = value;
                }
            }
        }
        public GameManager Game { get; }
        public EventWindow(GameManager game, List<EventCard> cards)
        {
            Game = game;
            InitializeComponent();
            PresentCards(cards);
            DataContext = this;
        }

        private void PresentCards(List<EventCard> cards)
        {
            CardStack.Children.Clear();

            foreach (var card in cards)
            {
                var cardView = new EventCardView(card);

                cardView.CardSelected += c =>
                {
                    SelectedCard = card;
                    UpdateSelectionVisuals();
                };

                CardStack.Children.Add(cardView);
            }
        }

        private void UpdateSelectionVisuals()
        {
            var cards = CardStack.Children.OfType<EventCardView>().ToList();
            foreach (var card in cards)
            {
                card.IsSelected = SelectedCard == card.EventCard;
            }
        }

        private async void Confirm_Button_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedCard == null) return;

            _locked = true;
            SelectedCard.Apply(Game);

            await Task.Delay(1000);
            this.Close();
        }
    }
}
