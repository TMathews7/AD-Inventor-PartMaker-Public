Imports System.Windows.Forms
Imports Gui
Imports Inventor

Namespace CubeDrawing
    Public Class Cube
        Public Sub DrawCube(form As Form1)
            Try
                ' Show Form1
                form.ShowDialog()

                ' Check if the user clicked OK
                If form.DialogResult = DialogResult.OK Then
                    ' Retrieve user inputs from Form1
                    Dim width As Double = form.Width
                    Dim height As Double = form.Height
                    Dim depth As Double = form.Depth

                    ' Initialize Inventor application
                    Dim inventorApp As Inventor.Application = TryCast(MarshalHelper.GetActiveObject("Inventor.Application"), Inventor.Application)

                    If inventorApp Is Nothing Then
                        Console.WriteLine("Autodesk Inventor is not running or accessible.")
                        Return
                    End If

                    ' Create a new part document
                    Dim inventorDoc As PartDocument = inventorApp.Documents.Add(DocumentTypeEnum.kPartDocumentObject)

                    ' Set a reference to the component definition
                    Dim compDef As PartComponentDefinition = inventorDoc.ComponentDefinition

                    ' Create a new sketch on the X-Y work plane
                    Dim sketch As PlanarSketch = compDef.Sketches.Add(compDef.WorkPlanes(3))

                    ' Set a reference to the transient geometry object
                    Dim transGeom As TransientGeometry = inventorApp.TransientGeometry

                    ' Draw a rectangle based on user input
                    Dim endPoint As Point2d = transGeom.CreatePoint2d(width, height) ' Use user input for rectangle dimensions
                    Dim rectangleLines As SketchEntitiesEnumerator = sketch.SketchLines.AddAsTwoPointRectangle(transGeom.CreatePoint2d(0, 0), endPoint)

                    ' Create a profile
                    Dim profile As Profile = sketch.Profiles.AddForSolid

                    ' Create an extrusion based on user input
                    Dim extrudeDef As ExtrudeDefinition = compDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(profile, PartFeatureOperationEnum.kJoinOperation)
                    extrudeDef.SetDistanceExtent(depth, PartFeatureExtentDirectionEnum.kNegativeExtentDirection) ' Use user input for extrusion thickness
                    Dim extrude As ExtrudeFeature = compDef.Features.ExtrudeFeatures.Add(extrudeDef)

                    ' Make the sketch visible for better visualization
                    sketch.Visible = True

                    Console.WriteLine("Extrusion created successfully.")
                Else
                    Console.WriteLine("User canceled the operation.")
                End If
            Catch ex As Exception
                Console.WriteLine("An error occurred: " & ex.Message)
            End Try
        End Sub
    End Class
End Namespace
