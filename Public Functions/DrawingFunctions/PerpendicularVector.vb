Imports System.Runtime.InteropServices
Imports Inventor

Public Class PerpendicularVectorAssignment
    Public Function CalculatePlacementPoint(drawingCurve As DrawingCurve, offsetDistance As Double) As Point2d
        ' Access INVAPP
        Dim inventorApp As Inventor.Application = TryCast(MarshalHelper.GetActiveObject("Inventor.Application"), Inventor.Application)

        If drawingCurve Is Nothing Then
            Throw New ArgumentNullException(NameOf(drawingCurve), "DrawingCurve cannot be null.")
        End If

        Dim startPoint As Point2d = drawingCurve.StartPoint
        Dim endPoint As Point2d = drawingCurve.EndPoint

        ' Calculate midpoint
        Dim midpoint As Point2d = inventorApp.TransientGeometry.CreatePoint2d(
            (startPoint.X + endPoint.X) / 2,
            (startPoint.Y + endPoint.Y) / 2
        )

        ' Calculate direction vector
        Dim directionVector As Vector2d = inventorApp.TransientGeometry.CreateVector2d(
            endPoint.X - startPoint.X,
            endPoint.Y - startPoint.Y
        )

        ' Calculate perpendicular vector
        Dim perpendicularVector As Vector2d = inventorApp.TransientGeometry.CreateVector2d(
            -directionVector.Y,
            directionVector.X
        )

        ' Normalize the perpendicular vector
        Dim length As Double = perpendicularVector.Length
        perpendicularVector = inventorApp.TransientGeometry.CreateVector2d(
            perpendicularVector.X / length,
            perpendicularVector.Y / length
        )

        ' Ensure the offset vector always goes in the positive direction
        Dim offsetVector As Vector2d = inventorApp.TransientGeometry.CreateVector2d(
            If(perpendicularVector.X >= 0, perpendicularVector.X * offsetDistance, -perpendicularVector.X * offsetDistance),
            If(perpendicularVector.Y >= 0, perpendicularVector.Y * offsetDistance, -perpendicularVector.Y * offsetDistance)
        )

        ' Calculate and return the placement point
        Dim placementPoint As Point2d = inventorApp.TransientGeometry.CreatePoint2d(
            midpoint.X + offsetVector.X,
            midpoint.Y + offsetVector.Y
        )

        Console.WriteLine("")
        Console.WriteLine($"Placement Point: X={placementPoint.X}, Y={placementPoint.Y}")
        Console.WriteLine("")

        Return placementPoint
    End Function
End Class
