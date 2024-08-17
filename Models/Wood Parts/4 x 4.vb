Imports System.Windows.Forms
Imports Inventor


Namespace FourXFourDrawing
    Public Class FourXFour
        Public Sub DrawFourXFour(width As Double, height As Double, depth As Double)
            Try
                ' Initialize Inventor application
                Dim inventorApp As Inventor.Application = TryCast(MarshalHelper.GetActiveObject("Inventor.Application"), Inventor.Application)

                If inventorApp Is Nothing Then
                    Console.WriteLine("Autodesk Inventor is not running or accessible.")
                    Return
                End If

                ' Create a new part document
                Dim inventorDoc As PartDocument = inventorApp.Documents.Add(DocumentTypeEnum.kPartDocumentObject)
                Dim unitsOfMeasure As UnitsOfMeasure = inventorDoc.UnitsOfMeasure
                unitsOfMeasure.LengthUnits = UnitsTypeEnum.kInchLengthUnits

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
                sketch.Visible = False

                Dim fileName As String = $"C:\Users\subto\Documents\InventorAuto\Parts\Wood Parts\4X4\4X4 - Length_{depth / 2.54}.ipt"
                inventorDoc.SaveAs(fileName, False)

                Console.WriteLine("Extrusion created successfully.")
            Catch ex As Exception
                Console.WriteLine("An error occurred: " & ex.Message)
            End Try
        End Sub
    End Class
End Namespace