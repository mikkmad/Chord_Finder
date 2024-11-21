using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Chord_Finder.Utils;

public static class GenerateFretboard
{
    private static void DrawFret(Canvas canvas, int startingFretNumber, int endingFretNumber, double fretSpacing, double stringSpacing)
    {
        for (int i = startingFretNumber; i <= endingFretNumber; i++)
        {
            // if starting fret is 0, show open strings
            if (startingFretNumber == 0 && i == 0) { continue; }
            
            double xPosition = (i - startingFretNumber) * fretSpacing;
            
            Line fretLine = new Line
            {
                X1 = xPosition,
                Y1 = 0,
                X2 = xPosition,
                Y2 = canvas.Height,
                Stroke = Brushes.DimGray,
                StrokeThickness = 2
            };

            if (i == 1)
            {
                fretLine.Stroke = Brushes.Black;
                fretLine.StrokeThickness = 10;
            }
            
            canvas.Children.Add(fretLine);
            
            DrawFretMarkers(canvas, i, stringSpacing, fretSpacing, startingFretNumber, endingFretNumber);
        }
    }

    private static void DrawStrings(Canvas canvas, double stringSpacing)
    {
        for (int i = 0; i < 6; i++)
        {
            Line stringLine = new Line
            {
                X1 = 0,
                Y1 = i * stringSpacing,
                X2 = canvas.Width,
                Y2 = i * stringSpacing,
                Stroke = Brushes.Black,
                StrokeThickness = 4
            };
            canvas.Children.Add(stringLine);
        }
    }

    private static void DrawFretMarkers(Canvas canvas, int currentFret, double stringSpacing, double fretSpacing, int startingFretNumber, int endingFretNumber)
    {
        // fret markers
        int[] fretMarkers = { 1, 3, 5, 7, 9, 12, 15, 17, 19, 21, 24 };

        const int ellipseDiameter = 25;

        if (currentFret >= startingFretNumber && currentFret < endingFretNumber && fretMarkers.Contains(currentFret))
        {
            // fixed multiplier, based on the number of visible frets
            // used for centering the fret marker dots and text horizontally
            double multiplier;
        
            int numberOfVisibleFrets = endingFretNumber - startingFretNumber;
        
            // set multipliers based on number of visible frets
            if (numberOfVisibleFrets <= 5)
            {
                multiplier = 2.2;
            }
            else if (numberOfVisibleFrets <= 10)
            {
                multiplier = 2.6;
            }
            else if (numberOfVisibleFrets <= 16)
            {
                multiplier = 3; 
            }
            else
            {
                multiplier = 4;
            }
            
            var dotMarker = new Ellipse
            {
                Width = ellipseDiameter,
                Height = ellipseDiameter,
                Fill = Brushes.Black,
                Stroke = Brushes.Black,
                StrokeThickness = 1
            };
            
            // Calculate the xPosition for the marker based on fret spacing
            double markerXPosition = (currentFret - startingFretNumber) * fretSpacing;

            // Center the marker
            markerXPosition += fretSpacing / multiplier;
            
            // Position the circle (vertically centered in the canvas)
            double markerYPosition = (canvas.Height / 2) - (ellipseDiameter / 2);  // Center vertically in the string positions
            

            if (currentFret is 12 or 24)
            {
                var dotMarker2 = new Ellipse
                {
                    Width = ellipseDiameter,
                    Height = ellipseDiameter,
                    Fill = Brushes.Black,
                    Stroke = Brushes.Black,
                    StrokeThickness = 1
                };
                
                // circle for string 4
                Canvas.SetLeft(dotMarker, markerXPosition);
                Canvas.SetTop(dotMarker, stringSpacing * 4 - 45);
                
                // circle for string 2
                Canvas.SetLeft(dotMarker2, markerXPosition);
                Canvas.SetTop(dotMarker2, stringSpacing * 1 + 25);
                
                canvas.Children.Add(dotMarker);
                canvas.Children.Add(dotMarker2);
            }
            else
            {
                Canvas.SetLeft(dotMarker, markerXPosition);
                Canvas.SetTop(dotMarker, markerYPosition);
                
                canvas.Children.Add(dotMarker);
            }
            
            // add fret number above string
            var fretNumber = new TextBlock
            {
                Text = currentFret.ToString(),
                FontSize = 24,
                Foreground = Brushes.Black,
                FontWeight = FontWeights.Bold,
            };
            
            // center fret text 
            if (currentFret > 10)
            {
                // fret text above 10 does not align perfectly, as it is left-centered rather than centered
                // therefore different xPosition is needed than if fret text is less than 10
                Canvas.SetLeft(fretNumber, markerXPosition);
                Canvas.SetTop(fretNumber, markerYPosition - 200);
            }
            else
            {
                Canvas.SetLeft(fretNumber, markerXPosition + 8);
                Canvas.SetTop(fretNumber, markerYPosition - 200);
            }
            
            canvas.Children.Add(fretNumber);
        }
    }
    
    public static void DrawFretboard(Canvas canvas, int startingFretNumber, int endingFretNumber)
    {
        canvas.Children.Clear();
        
        // number of visible frets
        int numberOfFrets = endingFretNumber - startingFretNumber;
        
        // spacing between visible frets
        double fretSpacing = canvas.Width / (numberOfFrets == 0 ? 1 : numberOfFrets);
        
        // string spacing (fixed for a 6-string guitar)
        double stringSpacing = canvas.Height / 5;

        // draw frets & fret-markers
        DrawFret(canvas, startingFretNumber, endingFretNumber, fretSpacing, stringSpacing);
        
        // draw the strings
        DrawStrings(canvas, stringSpacing);
    }
}