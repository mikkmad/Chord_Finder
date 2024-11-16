using System.Windows.Controls;
using System.Windows.Media;
using Chord_Finder.Pages;

namespace Chord_Finder;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow
{
    
    public MainWindow()
    {
        InitializeComponent();
        
        // navigate to the Welcome page whenever the application launches
        ContentFrame.Navigate(new WelcomePage());
    }
    
    private void NavigationListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {
        if (NavigationListBox.SelectedItem is ListBoxItem selectedItem)
        {
            switch (selectedItem.Content.ToString())
            {
                case "Favourites": ContentFrame.Navigate(new FavouritesPage()); 
                    break; 
                case "All Notes": ContentFrame.Navigate(new AllNotesPage()); 
                    break; 
                case "Chords": ContentFrame.Navigate(new ChordsPage()); 
                    break; 
                case "Find Chord": ContentFrame.Navigate(new FindChordPage()); 
                    break; 
                case "Scales": ContentFrame.Navigate(new ScalesPage()); 
                    break;
                case "Welcome Page": ContentFrame.Navigate(new WelcomePage());
                    break;
            }
        }
    }
}