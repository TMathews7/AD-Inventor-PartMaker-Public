Imports Inventor

Namespace CubeDrawing
    Class Program
        Shared Sub Main(args As String())
            Try
                ' Initialize Inventor application
                Dim inventorApp As Inventor.Application = TryCast(MarshalHelper.GetActiveObject("Inventor.Application"), Inventor.Application)

                If inventorApp Is Nothing Then
                    Console.WriteLine("Autodesk Inventor is not running or accessible.")
                    Return
                End If

                ' Create a new GUI instance to prompt the user for cube dimensions
                Dim gui As New GUI()
                gui.PromptUser()

                ' Create a new part document
                Dim inventorDoc As PartDocument = inventorApp.Documents.Add(DocumentTypeEnum.kPartDocumentObject)

                ' Set a reference to the component definition
                Dim compDef As PartComponentDefinition = inventorDoc.ComponentDefinition

                ' Create a new sketch on the X-Y work plane
                Dim sketch As PlanarSketch = compDef.Sketches.Add(compDef.WorkPlanes(3))

                ' Set a reference to the transient geometry object
                Dim transGeom As TransientGeometry = inventorApp.TransientGeometry

                ' Draw a rectangle based on user input
                Dim cornerPoint As Point2d = transGeom.CreatePoint2d(0, 0)
                Dim endPoint As Point2d = transGeom.CreatePoint2d(gui.Length, gui.Width) ' Use user input for rectangle dimensions
                Dim rectangleLines As SketchEntitiesEnumerator = sketch.SketchLines.AddAsTwoPointRectangle(cornerPoint, endPoint)

                ' Create a profile
                Dim profile As Profile = sketch.Profiles.AddForSolid

                ' Create an extrusion based on user input
                Dim extrudeDef As ExtrudeDefinition = compDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(profile, PartFeatureOperationEnum.kJoinOperation)
                extrudeDef.SetDistanceExtent(gui.Height, PartFeatureExtentDirectionEnum.kNegativeExtentDirection) ' Use user input for extrusion thickness
                Dim extrude As ExtrudeFeature = compDef.Features.ExtrudeFeatures.Add(extrudeDef)

                ' Make the sketch visible for better visualization
                sketch.Visible = True

                Console.WriteLine("Extrusion created successfully.")
            Catch ex As Exception
                Console.WriteLine("An error occurred: " & ex.Message)
            End Try
        End Sub
    End Class
End Namespace
