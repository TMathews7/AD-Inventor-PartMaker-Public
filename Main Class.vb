Imports Gui
Imports System.Windows.Forms

Public Class MainClass
    Public Shared Sub Main(args As String())
        Try
            ' Create an instance of Form1
            Dim form As New Form1()

            ' Show Form1
            form.ShowDialog()

            ' Check if the user clicked OK
            If form.DialogResult = DialogResult.OK Then
                ' Retrieve the selected option from Form1's ComboBox
                Dim selectedOption As String = form.SelectedOption

                ' Depending on the selected option, execute the corresponding functionality
                Select Case selectedOption
                    Case "Cube"
                        Dim cube As New CubeDrawing.Cube()
                        cube.DrawCube(form) ' Pass the Form1 instance to the DrawCube method
                    Case "Propeller"
                        ' Retrieve user inputs from Form1
                        Dim radius As Double = form.Radius
                        Dim height As Double = form.Height
                        Dim wingspan As Double = form.Wingspan
                        Dim holeRadius As Double = form.HoleRadius

                        ' Create a fan using the provided dimensions
                        Propeller.CreatePropeller(radius, height, wingspan, holeRadius)
                    Case "???"
                        ' Add code to execute the functionality for L Steel here
                End Select
            Else
                Console.WriteLine("User canceled the operation.")
            End If
        Catch ex As Exception
            Console.WriteLine("An error occurred: " & ex.Message)
        End Try
    End Sub
End Class
