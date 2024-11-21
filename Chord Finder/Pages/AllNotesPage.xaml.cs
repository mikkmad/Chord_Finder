using System.Windows.Controls;
using Chord_Finder.Utils;

namespace Chord_Finder.Pages;

public partial class AllNotesPage : Page
{
    public AllNotesPage()
    {
        InitializeComponent();
        GenerateFretboard.DrawFretboard(FretboardCanvas, 0, 16);
    }
}