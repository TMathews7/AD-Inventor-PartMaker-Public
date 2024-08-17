Imports System.Windows.Forms
Imports Inventor

Namespace StandardTopDrawing
    Public Class StandardTop
        Public Sub DrawStandardTop(width As Double, height As Double, depth As Double)
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

                ' Create a new sketch on the X-Z work plane
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

                ' Fit the object onto the screen
                camera.Fit()
                camera.Apply()

                ' Make the sketch visible for better visualization
                sketch.Visible = False

                ' Apply Appearance and Material
                Dim renderStyles As RenderStyles = inventorDoc.RenderStyles
                Dim partStyle As RenderStyle = renderStyles("Red Oak - Natural Low Gloss")

                ' Apply the style to the part's material
                Dim materials As Materials = inventorDoc.Materials
                Dim material As Material = materials("Wood (Cherry)") ' Assuming "Wood (Cherry)" is the material name

                Dim materialStyle As RenderStyle = renderStyles("Red Oak - Natural Low Gloss")

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
                Dim fileName As String = $"C:\Users\subto\Documents\InventorAuto\Parts\Wood Parts\StandardTop\StandardTop - W{width / 2.54}_H{height / 2.54}_D{depth / 2.54}.ipt"
                inventorDoc.SaveAs(fileName, False)

            Catch ex As Exception
                Console.WriteLine("An error occurred: " & ex.Message)
            End Try
        End Sub
    End Class
End Namespace