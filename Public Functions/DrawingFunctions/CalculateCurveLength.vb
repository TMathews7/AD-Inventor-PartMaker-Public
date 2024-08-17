Imports System.Runtime.InteropServices
Imports Inventor

Public Class CalculateCurveLength
    Public Function CalculateCurveLength(drawingCurve As DrawingCurve, scale As Double) As Double

        ' - Methods -
        ' 1. Gather all curves and add the information about them to a database
        ' 2. Find Curve Length and Compare to expected value
        '   ' A. This method uses Start and Endpoint so it will not work on Curved Curves until I implement a way to do so.
        ' 3. Further narrow down options by comparing Curves Angle relative to Y-Axis to expected value
        ' 4. To place the dimension on the correct line map midpoint of all remainings lines and choose based on needed value.

        ' This File takes care of part of 2.

        ' This code is not Available in the Public documents
          ' Email me if you want to learn about the drawing process at tmathews120@gmail.com

        Return length
    End Function
End Class
