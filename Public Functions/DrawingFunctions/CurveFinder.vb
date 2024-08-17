Imports System.Diagnostics.CodeAnalysis
Imports System.Runtime.InteropServices
Imports Inventor

Public Class CurveFinder
    Public Function CurveFinder(EPL As Double, EPA As Double, AssemblyViewScale As Double, baseView As DrawingView, curves As List(Of CurveInfo), CornerDecider As Double) As DrawingCurve
        ' Access INVAPP
        Dim inventorApp As Inventor.Application = TryCast(MarshalHelper.GetActiveObject("Inventor.Application"), Inventor.Application)

        ' - Methods -
        ' 1. Gather all curves and add the information about them to a database
        ' 2. Find Curve Length and Compare to expected value
        '   ' A. This method uses Start and Endpoint so it will not work on Curved Curves until I implement a way to do so.
        ' 3. Further narrow down options by comparing Curves Angle relative to Y-Axis to expected value
        ' 4. To place the dimension on the correct line map midpoint of all remaining lines and choose based on needed value.

        ' This File takes care of 2. 3. 4.

        ' This code is not Available in the Public documents
          ' Email me if you want to learn about the drawing process at tmathews120@gmail.com


        End Select
        Return BestCurve
    End Function
End Class

Public Class CorrectLineInfo
    Public Property MidPointX As Double
    Public Property MidPointY As Double

    Public Property Curve As DrawingCurve
End Class
