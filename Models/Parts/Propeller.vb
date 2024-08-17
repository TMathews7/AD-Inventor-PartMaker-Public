Imports System
Imports System.Drawing
Imports System.Net
Imports System.Runtime.InteropServices
Imports Inventor

Namespace PropellerDrawing
    Public Class Propeller
        Public Shared Sub CreatePropellerBase(radius As Double, height As Double, HoleRadius As Double, Wingspan As Double, numBlades As Double)
            Try
                ' Connect to the running instance of Inventor
                Dim inventorApp As Inventor.Application = MarshalHelper.GetActiveObject("Inventor.Application")

                If inventorApp Is Nothing Then
                    Console.WriteLine("Failed to connect to the running instance of Inventor.")
                    Return
                End If

                ' Define the constant for the part document type
                Const kPartDocumentObject As DocumentTypeEnum = DocumentTypeEnum.kPartDocumentObject

                ' Set a reference to the component definition
                Dim inventorDoc As PartDocument = inventorApp.Documents.Add(DocumentTypeEnum.kPartDocumentObject)
                Dim compDef As PartComponentDefinition = inventorDoc.ComponentDefinition

                ' Create a new part document using the default part template.
                Dim oPartDoc As PartDocument
                oPartDoc = inventorApp.Documents.Add(kPartDocumentObject, inventorApp.FileManager.GetTemplateFile(kPartDocumentObject))

                ' Create a new sketch on the X-Y work plane
                Dim sketch As PlanarSketch = compDef.Sketches.Add(compDef.WorkPlanes(3))

                Dim oTG As TransientGeometry = inventorApp.TransientGeometry

                Dim oPartDocA As PartDocument = inventorApp.ActiveDocument
                Dim oCompDef As PartComponentDefinition = oPartDocA.ComponentDefinition

                Dim origin As Point2d = oTG.CreatePoint2d(0, 0)
                Dim rt As Point2d = oTG.CreatePoint2d(radius / 2, 0)

                Dim oSketch As PlanarSketch = oCompDef.Sketches.Add(oCompDef.WorkPlanes(1))
                Dim pgon As SketchEntitiesEnumerator = oSketch.SketchLines.AddAsPolygon(numBlades, origin, rt, False)

                Dim oProfile As Profile = oSketch.Profiles.AddForSolid
                Dim oExtrude As ExtrudeFeature
                oExtrude = oCompDef.Features.ExtrudeFeatures.AddByDistanceExtent(oProfile, height, PartFeatureExtentDirectionEnum.kPositiveExtentDirection, PartFeatureOperationEnum.kJoinOperation, 0)

                Dim targetBodies As ObjectCollection = inventorApp.TransientObjects.CreateObjectCollection()

                For i As Integer = 1 To numBlades
                    Dim f As Face = oCompDef.Features.ExtrudeFeatures.Item(1).Faces.Item(i)
                    Dim s As Sketch = oCompDef.Sketches.Add(f, False)

                    ' Define the major axis vector
                    Dim ellipseMajorAxisVector As UnitVector2d = oTG.CreateUnitVector2d(height / 2, 0) ' Major axis vector

                    ' Define the rotation angle in radians (45 degrees)
                    Dim angleInRadians As Double = Math.PI / 4  ' 45 degrees in radians

                    ' Rotate the major axis vector
                    Dim rotatedX As Double = Math.Cos(angleInRadians) * ellipseMajorAxisVector.X - Math.Sin(angleInRadians) * ellipseMajorAxisVector.Y
                    Dim rotatedY As Double = Math.Sin(angleInRadians) * ellipseMajorAxisVector.X + Math.Cos(angleInRadians) * ellipseMajorAxisVector.Y

                    ' Draw an ellipse on the sketch with the rotated major axis vector
                    Dim ellipseCenter As Point2d = oTG.CreatePoint2d((height / 2), 0)
                    Dim rotatedEllipseMajorAxisVector As UnitVector2d = oTG.CreateUnitVector2d(rotatedX, rotatedY)
                    Dim ellipse As SketchEllipse = s.SketchEllipses.Add(ellipseCenter, rotatedEllipseMajorAxisVector, 1, height / 2)

                    ' Extrude the ellipse
                    Dim profileForExtrude As Profile = s.Profiles.AddForSolid()
                    Dim extrudeDef As ExtrudeDefinition = oCompDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(profileForExtrude, PartFeatureOperationEnum.kNewBodyOperation)
                    extrudeDef.SetDistanceExtent(Wingspan, PartFeatureExtentDirectionEnum.kPositiveExtentDirection)
                    Dim extrusion As ExtrudeFeature = oCompDef.Features.ExtrudeFeatures.Add(extrudeDef)

                    targetBodies.Add(extrusion.SurfaceBody())
                Next i


                ' Add the extrude feature for the polygon to the targetBodies collection
                Dim polygonExtrusion As ExtrudeFeature = oCompDef.Features.ExtrudeFeatures.AddByDistanceExtent(oProfile, height, PartFeatureExtentDirectionEnum.kPositiveExtentDirection, PartFeatureOperationEnum.kJoinOperation, 0)
                targetBodies.Add(polygonExtrusion.SurfaceBody())

                ' Set a reference to the part component definition.
                oCompDef = oPartDoc.ComponentDefinition

                ' Create a new sketch on the X-Y work plane for the cylinder.
                Dim oSketch1 As PlanarSketch
                oSketch1 = oCompDef.Sketches.Add(oCompDef.WorkPlanes.Item(1))

                ' Set a reference to the transient geometry object.
                Dim oTransGeom As TransientGeometry
                oTransGeom = inventorApp.TransientGeometry

                ' Draw a circle on the sketch for the cylinder.
                Dim oCircle As SketchCircle
                oCircle = oSketch1.SketchCircles.AddByCenterRadius(oTransGeom.CreatePoint2d(0, 0), radius)

                ' Create a profile for the cylinder.
                Dim oProfileA As Profile
                oProfileA = oSketch1.Profiles.AddForSolid

                ' Create a solid extrusion for the cylinder.
                Dim circleExtrusionDef As ExtrudeDefinition
                circleExtrusionDef = oCompDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(oProfileA, PartFeatureOperationEnum.kNewBodyOperation)
                circleExtrusionDef.SetDistanceExtent(height, PartFeatureExtentDirectionEnum.kPositiveExtentDirection)
                Dim circleExtrusion As ExtrudeFeature
                circleExtrusion = oCompDef.Features.ExtrudeFeatures.Add(circleExtrusionDef)
                targetBodies.Add(circleExtrusion.SurfaceBody())

                ' Get the center point of the circle
                Dim centerPoint As Point2d = oCircle.CenterSketchPoint.Geometry

                ' Create a new sketch on the same work plane as the circle for the clearance hole.
                Dim holeCutter As PlanarSketch
                holeCutter = oCompDef.Sketches.Add(oCompDef.WorkPlanes.Item(1))

                ' Draw a circle on the sketch for the clearance hole.
                Dim clearanceCircle As SketchCircle
                clearanceCircle = holeCutter.SketchCircles.AddByCenterRadius(centerPoint, HoleRadius)

                ' Create a profile for the clearance hole.
                Dim clearanceProfile As Profile
                clearanceProfile = holeCutter.Profiles.AddForSolid

                ' Create a solid extrusion for the clearance hole.
                Dim clearanceExtrudeDef As ExtrudeDefinition
                clearanceExtrudeDef = oCompDef.Features.ExtrudeFeatures.CreateExtrudeDefinition(clearanceProfile, PartFeatureOperationEnum.kCutOperation)

                ' Apply the cut operation to the target bodies
                clearanceExtrudeDef.AffectedBodies = targetBodies
                clearanceExtrudeDef.SetDistanceExtent(height + 1, PartFeatureExtentDirectionEnum.kPositiveExtentDirection) ' Adds 1 to ensure it goes all the way through,
                Dim clearanceExtrusion As ExtrudeFeature
                clearanceExtrusion = oCompDef.Features.ExtrudeFeatures.Add(clearanceExtrudeDef)

                Console.WriteLine("Propeller with clearance hole and wings created successfully.")
            Catch ex As Exception
                Console.WriteLine("An error occurred: " & ex.Message)
            End Try
        End Sub
    End Class
End Namespace