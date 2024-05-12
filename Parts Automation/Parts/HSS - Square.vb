Imports System
Imports System.Net
Imports System.Runtime.InteropServices
Imports Inventor

Namespace HSSSquareDrawing
    Public Class HSSSquare
        Public Shared Sub DrawHSSSquare(height As Double, width As Double, thickness As Double, length As Double, HoleRadius As Double, numHoles As Double)
            Try
                ' Connect to the running instance of Inventor
                Dim inventorApp As Inventor.Application = MarshalHelper.GetActiveObject("Inventor.Application")

                If inventorApp Is Nothing Then
                    Console.WriteLine("Failed to connect to the running instance of Inventor.")
                    Return
                End If

                Console.WriteLine("Hole radius: " & HoleRadius)

                thickness *= 0.1 'Convert CM to MM
                HoleRadius *= 0.1

                ' Create a new part document
                Dim inventorDoc As PartDocument = inventorApp.Documents.Add(DocumentTypeEnum.kPartDocumentObject)

                ' Set a reference to the component definition
                Dim compDef As PartComponentDefinition = inventorDoc.ComponentDefinition

                ' Create a new sketch on the X-Y work plane
                Dim sketch As PlanarSketch = compDef.Sketches.Add(compDef.WorkPlanes(3))

                ' Set a reference to the transient geometry object
                Dim transGeom As TransientGeometry = inventorApp.TransientGeometry

                ' Draw a rectangle based on user input
                Dim endPoint As Point2d = transGeom.CreatePoint2d(width + (thickness * 2), height + (thickness * 2)) ' Use user input for rectangle dimensions
                Dim rectangleLines As SketchEntitiesEnumerator = sketch.SketchLines.AddAsTwoPointRectangle(transGeom.CreatePoint2d(0, 0), endPoint)

                ' Create a profile
                Dim profile As Profile = sketch.Profiles.AddForSolid

                ' Create an extrusion based on user input
                Dim extrudeDef As ExtrudeDefinition = compDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(profile, PartFeatureOperationEnum.kJoinOperation)
                extrudeDef.SetDistanceExtent(length, PartFeatureExtentDirectionEnum.kPositiveExtentDirection) ' Use user input for extrusion length
                Dim extrude As ExtrudeFeature = compDef.Features.ExtrudeFeatures.Add(extrudeDef)

                ' Sketch another square on the same plane
                Dim sketch2 As PlanarSketch = compDef.Sketches.Add(compDef.WorkPlanes(3))
                Dim endPoint2 As Point2d = transGeom.CreatePoint2d(width + thickness, height + thickness) ' Offset by thickness
                Dim rectangleLines2 As SketchEntitiesEnumerator = sketch2.SketchLines.AddAsTwoPointRectangle(transGeom.CreatePoint2d(thickness, thickness), endPoint2)

                ' Update the part document
                inventorDoc.Update()

                ' Create a profile for the clearance hole.
                Dim clearanceProfile As Profile = sketch2.Profiles.AddForSolid

                ' Create a solid extrusion for the clearance hole.
                Dim clearanceExtrudeDef As ExtrudeDefinition = compDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(clearanceProfile, PartFeatureOperationEnum.kCutOperation)
                clearanceExtrudeDef.SetDistanceExtent(length + 1, PartFeatureExtentDirectionEnum.kPositiveExtentDirection) ' Adds 1 to ensure it goes all the way through
                Dim clearanceExtrusion As ExtrudeFeature = compDef.Features.ExtrudeFeatures.Add(clearanceExtrudeDef)

                ' Update the part document
                inventorDoc.Update()

                ' Calculate the spacing between each hole

                Dim position As Double
                Dim spacing As Double

                For i As Integer = 1 To numHoles
                    Dim holeY As Double
                    Dim holeZ As Double

                    spacing = length / (numHoles + 1)
                    position = width
                    holeY = (i * spacing)
                    holeZ = (position / 2)

                    ' Create a point for the hole center
                    Dim holeCenter As Point2d = transGeom.CreatePoint2d(holeZ, holeY)

                    ' Create a sketch on the YZ plane
                    Dim sketch3 As PlanarSketch = compDef.Sketches.Add(compDef.WorkPlanes(1))

                    ' Create a circle for the hole
                    Dim holeCircle As SketchCircle = sketch3.SketchCircles.AddByCenterRadius(holeCenter, HoleRadius) ' Adjust radius as needed

                    ' Create a profile for the clearance hole.
                    Dim clearanceProfile2 As Profile = sketch3.Profiles.AddForSolid

                    ' Create a solid extrusion for the clearance hole.
                    Dim clearanceExtrudeDef2 As ExtrudeDefinition = compDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(clearanceProfile2, PartFeatureOperationEnum.kCutOperation)
                    clearanceExtrudeDef2.SetDistanceExtent((thickness + width) + 1, PartFeatureExtentDirectionEnum.kPositiveExtentDirection) ' Adds 1 to ensure it goes all the way through
                    Dim clearanceExtrusion2 As ExtrudeFeature = compDef.Features.ExtrudeFeatures.Add(clearanceExtrudeDef2)

                    ' Update the part document
                    inventorDoc.Update()
                Next

                For i As Integer = 1 To numHoles
                    Dim holeY As Double
                    Dim holeZ As Double

                    spacing = length / (numHoles + 1)
                    position = width
                    holeY = (i * spacing)
                    holeZ = -(position / 2)

                    ' Create a point for the hole center
                    Dim holeCenter As Point2d = transGeom.CreatePoint2d(holeZ, holeY)

                    ' Create a sketch on the YZ plane
                    Dim sketch3 As PlanarSketch = compDef.Sketches.Add(compDef.WorkPlanes(2))

                    ' Create a circle for the hole
                    Dim holeCircle As SketchCircle = sketch3.SketchCircles.AddByCenterRadius(holeCenter, HoleRadius) ' Adjust radius as needed

                    ' Create a profile for the clearance hole.
                    Dim clearanceProfile2 As Profile = sketch3.Profiles.AddForSolid

                    ' Create a solid extrusion for the clearance hole.
                    Dim clearanceExtrudeDef2 As ExtrudeDefinition = compDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(clearanceProfile2, PartFeatureOperationEnum.kCutOperation)
                    clearanceExtrudeDef2.SetDistanceExtent((thickness + height) + 1, PartFeatureExtentDirectionEnum.kPositiveExtentDirection) ' Adds 1 to ensure it goes all the way through
                    Dim clearanceExtrusion2 As ExtrudeFeature = compDef.Features.ExtrudeFeatures.Add(clearanceExtrudeDef2)

                    ' Update the part document
                    inventorDoc.Update()
                Next

                Console.WriteLine("L Beam created successfully.")
            Catch ex As Exception
                Console.WriteLine("An error occurred: " & ex.Message)
            End Try
        End Sub
    End Class
End Namespace
