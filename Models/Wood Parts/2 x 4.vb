Imports System.Threading
Imports System.Windows.Forms
Imports Inventor

Namespace TwoXFourDrawing
    Public Class TwoXFour
        Public Sub DrawTwoXFour(width As Double, height As Double, depth As Double)
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

                ' Ensure the ActiveView and Camera are properly instantiated before use
                Dim activeView As Inventor.View = inventorApp.ActiveView
                Dim camera As Camera = activeView.Camera
                camera.ViewOrientationType = ViewOrientationTypeEnum.kIsoTopRightViewOrientation
                camera.Apply()

                ' Create a new sketch on the X-Y work plane
                Dim sketch As PlanarSketch = compDef.Sketches.Add(compDef.WorkPlanes(2))

                ' Set a reference to the transient geometry object
                Dim transGeom As TransientGeometry = inventorApp.TransientGeometry

                ' Draw a rectangle based on user input
                Dim endPoint As Point2d = transGeom.CreatePoint2d(width, height)
                Dim rectangleLines As SketchEntitiesEnumerator = sketch.SketchLines.AddAsTwoPointRectangle(transGeom.CreatePoint2d(0, 0), endPoint)

                ' Create a profile
                Dim profile As Profile = sketch.Profiles.AddForSolid

                ' Create an extrusion based on user input
                Dim extrudeDef As ExtrudeDefinition = compDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(profile, PartFeatureOperationEnum.kJoinOperation)
                extrudeDef.SetDistanceExtent(depth, PartFeatureExtentDirectionEnum.kNegativeExtentDirection)
                Dim extrude As ExtrudeFeature = compDef.Features.ExtrudeFeatures.Add(extrudeDef)
                camera.Fit() ' Fits the object onto the screen
                camera.Apply()

                ' Make the sketch visible for better visualization
                sketch.Visible = False

                ' Create an EdgeCollection to store the edges for filleting
                Dim edgeCollection As EdgeCollection = inventorApp.TransientObjects.CreateEdgeCollection

                ' Add the first four edges of the extrusion to the collection
                For i As Integer = 2 To 4 Step 2
                    Dim edge As Edge = extrude.SideFaces.Item(1).Edges(i)
                    edgeCollection.Add(edge)
                Next

                For i As Integer = 2 To 4 Step 2
                    Dim edge As Edge = extrude.SideFaces.Item(3).Edges(i)
                    edgeCollection.Add(edge)
                Next

                ' Apply the fillet to the entire collection of edges
                Dim filletRadius As Double = (2 / 8)
                Dim filletFeature As FilletFeature = compDef.Features.FilletFeatures.AddSimple(edgeCollection, filletRadius)

                ' Apply "White Oak - Solid Natural Medium Gloss" appearance
                Dim renderStyles As RenderStyles = inventorDoc.RenderStyles
                Dim partStyle As RenderStyle = renderStyles("Walnut") ' Input something here or it wont work, I do not know why.

                ' Apply the style to the part's material
                Dim materials As Materials = inventorDoc.Materials
                Dim material As Material = materials("Wood (Cherry)") ' Assuming "Wood" is the material name for the part

                Dim materialStyle As RenderStyle = renderStyles("Walnut")

                ' Convert library material to local if needed
                Dim localMaterial As Material
                If material.StyleLocation = StyleLocationEnum.kLibraryStyleLocation Then
                    localMaterial = material.ConvertToLocal()
                Else
                    localMaterial = material
                End If

                localMaterial.RenderStyle = materialStyle

                ' Set the material for the part
                compDef.Material = localMaterial

                ' Save File
                Dim fileName As String = $"C:\Users\subto\Documents\InventorAuto\Parts\Wood Parts\2X4\2X4 - Length_{depth / 2.54}.ipt"
                inventorDoc.SaveAs(fileName, False)

                ' Debugging Information
                Console.WriteLine()
                Console.WriteLine("2X4 Search Curves:")
                Console.WriteLine($"2X4 Length: {depth / 2.54}")
                Console.WriteLine($"2X4 Width: 3.5")
                Console.WriteLine($"2X4 Height: 1.5")
                Console.WriteLine()

            Catch ex As Exception
                Console.WriteLine("An error occurred: " & ex.Message)
            End Try
        End Sub
    End Class
End Namespace