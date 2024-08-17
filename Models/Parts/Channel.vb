Imports System
Imports System.Net
Imports System.Runtime.InteropServices
Imports Inventor

Namespace ChannelDrawing
    Public Class Channel
        Public Shared Sub DrawChannel(height As Double, width As Double, thickness As Double, length As Double)
            ' Connect to the running instance of Inventor
            Dim inventorApp As Inventor.Application = MarshalHelper.GetActiveObject("Inventor.Application")

            If inventorApp Is Nothing Then
                Console.WriteLine("Failed to connect to the running instance of Inventor.")
                Return
            End If

            thickness *= 0.1 'Convert CM to MM

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
            extrudeDef.SetDistanceExtent(length, PartFeatureExtentDirectionEnum.kPositiveExtentDirection) ' Use user input for extrusion length
            Dim extrude As ExtrudeFeature = compDef.Features.ExtrudeFeatures.Add(extrudeDef)

            ' Sketch another square on the same plane
            Dim sketch2 As PlanarSketch = compDef.Sketches.Add(compDef.WorkPlanes(3))
            Dim endPoint2 As Point2d = transGeom.CreatePoint2d(width - thickness, height) ' Offset by thickness
            Dim rectangleLines2 As SketchEntitiesEnumerator = sketch2.SketchLines.AddAsTwoPointRectangle(transGeom.CreatePoint2d(thickness, thickness), endPoint2)

            ' Update the part document
            inventorDoc.Update()

            ' Create a profile for the clearance hole.
            Dim clearanceProfile As Profile = sketch2.Profiles.AddForSolid

            ' Create a solid extrusion for the clearance hole.
            Dim clearanceExtrudeDef As ExtrudeDefinition = compDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(clearanceProfile, PartFeatureOperationEnum.kCutOperation)
            clearanceExtrudeDef.SetDistanceExtent(length + 1, PartFeatureExtentDirectionEnum.kPositiveExtentDirection) ' Adds 1 to ensure it goes all the way through
            Dim clearanceExtrusion As ExtrudeFeature = compDef.Features.ExtrudeFeatures.Add(clearanceExtrudeDef)
        End Sub
    End Class
End Namespace


