Imports System.Runtime.InteropServices
Imports Inventor

Public Class CurveDocumentor
    Public Function CurveDocumentor(baseView As Blank, Blank As Double) As List(Of CurveInfo)

        ' This code is not Available in the Public documents
          ' Email me if you want to learn about the drawing process at tmathews120@gmail.com

        Return curveDatabase
    End Function
End Class

' Below just defines information about each Curve to help make it accessible

Public Class CurveInfo
    Public Property Length As Double
    Public Property StartPoint As Point2d
    Public Property EndPoint As Point2d
    Public Property MidPoint As Point2d
    Public Property Angle As Double
    Public Property Curve As DrawingCurve
    Public Property index As Integer
End Class

