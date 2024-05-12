Imports System.Windows.Forms
Imports System.Windows.Forms.VisualStyles.VisualStyleElement

Public Class Form1
    Inherits System.Windows.Forms.Form
    Public Property SelectedOption As String 'Crucial this remains
    Public Property Width As Double
    Public Property Height As Double
    Public Property Depth As Double
    Public Property Radius As Double
    Public Property Wingspan As Double
    Public Property numBlades As Double
    Public Property HoleRadius As Double
    Public Property Length As Double
    Public Property Thickness As Double
    Public Property numHoles As Double
    Public Property rotateHolePlacement As Boolean

    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Initialize Start button
        Dim btnOK As New System.Windows.Forms.Button()
        btnOK.Text = "Start"
        btnOK.ForeColor = Color.White
        btnOK.DialogResult = DialogResult.OK
        btnOK.Location = New Point(22, 395)
        btnOK.Size = New Size(219, 43)
        btnOK.UseVisualStyleBackColor = True

        ' Add event handler for Start button
        AddHandler btnOK.Click, AddressOf btnOK_Click
        Me.Controls.Add(btnOK)
    End Sub
    Private Sub btnOK_Click(sender As Object, e As EventArgs)
        ' Check which tab is currently selected
        If MainTabControl.SelectedTab Is ShapesTabPage Then
            ' If Shapes tab is selected, set SelectedOption to the selected item in ShapesComboBox
            SelectedOption = ShapesComboBox.SelectedItem?.ToString()
        ElseIf MainTabControl.SelectedTab Is PartsTabPage Then
            ' If Parts tab is selected, set SelectedOption to the selected item in PartsComboBox
            SelectedOption = PartsComboBox.SelectedItem?.ToString()
        Else
            ' Handle other tabs if needed
        End If

        ' Set the DialogResult to OK to indicate that the user clicked OK
        Me.DialogResult = DialogResult.OK
        Me.Close() ' Close the form immediately after setting DialogResult
    End Sub
    Private Sub textBoxWidth_TextChanged(sender As Object, e As EventArgs)
        Dim widthValue As Double
        If Double.TryParse(CType(sender, System.Windows.Forms.TextBox).Text, widthValue) Then
            Width = widthValue
        End If
    End Sub

    Private Sub textBoxHeight_TextChanged(sender As Object, e As EventArgs)
        Dim heightValue As Double
        If Double.TryParse(CType(sender, System.Windows.Forms.TextBox).Text, heightValue) Then
            Height = heightValue
        End If
    End Sub

    Private Sub textBoxDepth_TextChanged(sender As Object, e As EventArgs)
        Dim depthValue As Double
        If Double.TryParse(CType(sender, System.Windows.Forms.TextBox).Text, depthValue) Then
            Depth = depthValue
        End If
    End Sub

    Private Sub textBoxRadius_TextChanged(sender As Object, e As EventArgs)
        Dim radiusValue As Double
        If Double.TryParse(CType(sender, System.Windows.Forms.TextBox).Text, radiusValue) Then
            Radius = radiusValue
        End If
    End Sub

    Private Sub textBoxWingspan_TextChanged(sender As Object, e As EventArgs)
        Dim wingspanValue As Double
        If Double.TryParse(CType(sender, System.Windows.Forms.TextBox).Text, wingspanValue) Then
            Wingspan = wingspanValue
        End If
    End Sub

    Private Sub textBoxHoleRadius_TextChanged(sender As Object, e As EventArgs)
        Dim holeRadiusValue As Double
        If Double.TryParse(CType(sender, System.Windows.Forms.TextBox).Text, holeRadiusValue) Then
            HoleRadius = holeRadiusValue
        End If
    End Sub
    Private Sub textBoxBlades_TextChanged(sender As Object, e As EventArgs)
        Dim bladesValue As Integer
        If Integer.TryParse(CType(sender, System.Windows.Forms.TextBox).Text, bladesValue) Then
            numBlades = bladesValue
        End If
    End Sub
    Private Sub textBoxLength_TextChanged(sender As Object, e As EventArgs)
        Dim lengthValue As Integer
        If Integer.TryParse(CType(sender, System.Windows.Forms.TextBox).Text, lengthValue) Then
            Length = lengthValue
        End If
    End Sub

    Private Sub textBoxThickness_TextChanged(sender As Object, e As EventArgs)
        Dim thicknessValue As Integer
        If Integer.TryParse(CType(sender, System.Windows.Forms.TextBox).Text, thicknessValue) Then
            Thickness = thicknessValue
        End If
    End Sub
    Private Sub textBoxNumHoles_TextChanged(sender As Object, e As EventArgs)
        Dim NumHolesvalue As Integer
        If Integer.TryParse(CType(sender, System.Windows.Forms.TextBox).Text, NumHolesvalue) Then
            numHoles = NumHolesvalue
        End If
    End Sub
    Private Sub StartButton_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub PartsComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles PartsComboBox.SelectedIndexChanged
        ' Clear existing controls
        PartsPanel.Controls.Clear()

        ' Add input controls based on the selected item
        Select Case PartsComboBox.SelectedItem.ToString()
            Case "Propeller"
                Dim labelHeight As New Label()
                labelHeight.Text = "Height (CM)"
                labelHeight.ForeColor = Color.Black
                labelHeight.Location = New Point(100, 10)
                PartsPanel.Controls.Add(labelHeight)

                Dim textBoxHeight As New System.Windows.Forms.TextBox()
                textBoxHeight.Location = New Point(0, 10)
                textBoxHeight.Font = New Font(textBoxHeight.Font, FontStyle.Bold)
                textBoxHeight.TextAlign = HorizontalAlignment.Right
                PartsPanel.Controls.Add(textBoxHeight)
                AddHandler textBoxHeight.TextChanged, AddressOf textBoxHeight_TextChanged

                Dim labelRadius As New Label()
                labelRadius.Text = "Radius (CM)"
                labelRadius.ForeColor = Color.Black
                labelRadius.Location = New Point(100, 40)
                PartsPanel.Controls.Add(labelRadius)

                Dim textBoxRadius As New System.Windows.Forms.TextBox()
                textBoxRadius.Location = New Point(0, 40)
                textBoxRadius.Font = New Font(textBoxRadius.Font, FontStyle.Bold)
                textBoxRadius.TextAlign = HorizontalAlignment.Right
                PartsPanel.Controls.Add(textBoxRadius)
                AddHandler textBoxRadius.TextChanged, AddressOf textBoxRadius_TextChanged

                Dim labelWingspan As New Label()
                labelWingspan.Text = "Wingspan (CM)"
                labelWingspan.ForeColor = Color.Black
                labelWingspan.Location = New Point(100, 70)
                PartsPanel.Controls.Add(labelWingspan)

                Dim textBoxWingspan As New System.Windows.Forms.TextBox()
                textBoxWingspan.Location = New Point(0, 70)
                textBoxWingspan.Font = New Font(textBoxWingspan.Font, FontStyle.Bold)
                textBoxWingspan.TextAlign = HorizontalAlignment.Right
                PartsPanel.Controls.Add(textBoxWingspan)
                AddHandler textBoxWingspan.TextChanged, AddressOf textBoxWingspan_TextChanged

                Dim labelHoleRadius As New Label()
                labelHoleRadius.Text = "Hole Radius (CM)"
                labelHoleRadius.ForeColor = Color.Black
                labelHoleRadius.Location = New Point(100, 100)
                PartsPanel.Controls.Add(labelHoleRadius)

                Dim textBoxHoleRadius As New System.Windows.Forms.TextBox()
                textBoxHoleRadius.Location = New Point(0, 100)
                textBoxHoleRadius.Font = New Font(textBoxHoleRadius.Font, FontStyle.Bold)
                textBoxHoleRadius.TextAlign = HorizontalAlignment.Right
                PartsPanel.Controls.Add(textBoxHoleRadius)
                AddHandler textBoxHoleRadius.TextChanged, AddressOf textBoxHoleRadius_TextChanged


                Dim labelBlades As New Label()
                labelBlades.Text = "# of Blades"
                labelBlades.ForeColor = Color.Black
                labelBlades.Location = New Point(100, 130)
                PartsPanel.Controls.Add(labelBlades)

                Dim textBoxBlades As New System.Windows.Forms.TextBox()
                textBoxBlades.Location = New Point(0, 130)
                textBoxBlades.Font = New Font(textBoxBlades.Font, FontStyle.Bold)
                textBoxBlades.TextAlign = HorizontalAlignment.Right
                PartsPanel.Controls.Add(textBoxBlades)
                AddHandler textBoxBlades.TextChanged, AddressOf textBoxBlades_TextChanged

            Case "Channel"
                Dim labelHeightChannel As New Label()
                labelHeightChannel.Text = "Height (CM)"
                labelHeightChannel.ForeColor = Color.Black
                labelHeightChannel.Location = New Point(100, 10)
                PartsPanel.Controls.Add(labelHeightChannel)

                Dim textBoxHeightChannel As New System.Windows.Forms.TextBox()
                textBoxHeightChannel.Location = New Point(0, 10)
                textBoxHeightChannel.Font = New Font(textBoxHeightChannel.Font, FontStyle.Bold)
                textBoxHeightChannel.TextAlign = HorizontalAlignment.Right
                PartsPanel.Controls.Add(textBoxHeightChannel)
                AddHandler textBoxHeightChannel.TextChanged, AddressOf textBoxHeight_TextChanged

                Dim labelWidthChannel As New Label()
                labelWidthChannel.Text = "Width (CM)"
                labelWidthChannel.ForeColor = Color.Black
                labelWidthChannel.Location = New Point(100, 40)
                PartsPanel.Controls.Add(labelWidthChannel)

                Dim textBoxWidthChannel As New System.Windows.Forms.TextBox()
                textBoxWidthChannel.Location = New Point(0, 40)
                textBoxWidthChannel.Font = New Font(textBoxWidthChannel.Font, FontStyle.Bold)
                textBoxWidthChannel.TextAlign = HorizontalAlignment.Right
                PartsPanel.Controls.Add(textBoxWidthChannel)
                AddHandler textBoxWidthChannel.TextChanged, AddressOf textBoxWidth_TextChanged

                Dim labelThicknessChannel As New Label()
                labelThicknessChannel.Text = "Thickness (MM)"
                labelThicknessChannel.ForeColor = Color.Black
                labelThicknessChannel.Location = New Point(100, 70)
                PartsPanel.Controls.Add(labelThicknessChannel)

                Dim textBoxThicknessChannel As New System.Windows.Forms.TextBox()
                textBoxThicknessChannel.Location = New Point(0, 70)
                textBoxThicknessChannel.Font = New Font(textBoxThicknessChannel.Font, FontStyle.Bold)
                textBoxThicknessChannel.TextAlign = HorizontalAlignment.Right
                PartsPanel.Controls.Add(textBoxThicknessChannel)
                AddHandler textBoxThicknessChannel.TextChanged, AddressOf textBoxThickness_TextChanged

                Dim labelLengthChannel As New Label()
                labelLengthChannel.Text = "Length (CM)"
                labelLengthChannel.ForeColor = Color.Black
                labelLengthChannel.Location = New Point(100, 100)
                PartsPanel.Controls.Add(labelLengthChannel)

                Dim textBoxLengthChannel As New System.Windows.Forms.TextBox()
                textBoxLengthChannel.Location = New Point(0, 100)
                textBoxLengthChannel.Font = New Font(textBoxLengthChannel.Font, FontStyle.Bold)
                textBoxLengthChannel.TextAlign = HorizontalAlignment.Right
                PartsPanel.Controls.Add(textBoxLengthChannel)
                AddHandler textBoxLengthChannel.TextChanged, AddressOf textBoxLength_TextChanged

            Case "L Beam"
                Dim labelHeightLBeam As New Label()
                labelHeightLBeam.Text = "Height (CM)"
                labelHeightLBeam.ForeColor = Color.Black
                labelHeightLBeam.Location = New Point(100, 10)
                PartsPanel.Controls.Add(labelHeightLBeam)

                Dim textBoxHeightLBeam As New System.Windows.Forms.TextBox()
                textBoxHeightLBeam.Location = New Point(0, 10)
                textBoxHeightLBeam.Font = New Font(textBoxHeightLBeam.Font, FontStyle.Bold)
                textBoxHeightLBeam.TextAlign = HorizontalAlignment.Right
                PartsPanel.Controls.Add(textBoxHeightLBeam)
                AddHandler textBoxHeightLBeam.TextChanged, AddressOf textBoxHeight_TextChanged

                Dim labelWidthLBeam As New Label()
                labelWidthLBeam.Text = "Width (CM)"
                labelWidthLBeam.ForeColor = Color.Black
                labelWidthLBeam.Location = New Point(100, 40)
                PartsPanel.Controls.Add(labelWidthLBeam)

                Dim textBoxWidthLBeam As New System.Windows.Forms.TextBox()
                textBoxWidthLBeam.Location = New Point(0, 40)
                textBoxWidthLBeam.Font = New Font(textBoxWidthLBeam.Font, FontStyle.Bold)
                textBoxWidthLBeam.TextAlign = HorizontalAlignment.Right
                PartsPanel.Controls.Add(textBoxWidthLBeam)
                AddHandler textBoxWidthLBeam.TextChanged, AddressOf textBoxWidth_TextChanged

                Dim labelThicknessLBeam As New Label()
                labelThicknessLBeam.Text = "Thickness (MM)"
                labelThicknessLBeam.ForeColor = Color.Black
                labelThicknessLBeam.Location = New Point(100, 70)
                PartsPanel.Controls.Add(labelThicknessLBeam)

                Dim textBoxThicknessLBeam As New System.Windows.Forms.TextBox()
                textBoxThicknessLBeam.Location = New Point(0, 70)
                textBoxThicknessLBeam.Font = New Font(textBoxThicknessLBeam.Font, FontStyle.Bold)
                textBoxThicknessLBeam.TextAlign = HorizontalAlignment.Right
                PartsPanel.Controls.Add(textBoxThicknessLBeam)
                AddHandler textBoxThicknessLBeam.TextChanged, AddressOf textBoxThickness_TextChanged

                Dim labelLengthLBeam As New Label()
                labelLengthLBeam.Text = "Length (CM)"
                labelLengthLBeam.ForeColor = Color.Black
                labelLengthLBeam.Location = New Point(100, 100)
                PartsPanel.Controls.Add(labelLengthLBeam)

                Dim textBoxLengthLBeam As New System.Windows.Forms.TextBox()
                textBoxLengthLBeam.Location = New Point(0, 100)
                textBoxLengthLBeam.Font = New Font(textBoxLengthLBeam.Font, FontStyle.Bold)
                textBoxLengthLBeam.TextAlign = HorizontalAlignment.Right
                PartsPanel.Controls.Add(textBoxLengthLBeam)
                AddHandler textBoxLengthLBeam.TextChanged, AddressOf textBoxLength_TextChanged

                Dim labelNumHoles As New Label()
                labelNumHoles.Text = "# of Holes"
                labelNumHoles.ForeColor = Color.Black
                labelNumHoles.Location = New Point(100, 130)
                PartsPanel.Controls.Add(labelNumHoles)

                Dim textBoxNumHoles As New System.Windows.Forms.TextBox()
                textBoxNumHoles.Location = New Point(0, 130)
                textBoxNumHoles.Font = New Font(textBoxNumHoles.Font, FontStyle.Bold)
                textBoxNumHoles.TextAlign = HorizontalAlignment.Right
                PartsPanel.Controls.Add(textBoxNumHoles)
                AddHandler textBoxNumHoles.TextChanged, AddressOf textBoxNumHoles_TextChanged

                Dim labelHoleRadius As New Label()
                labelHoleRadius.Text = "HoleRadius"
                labelHoleRadius.ForeColor = Color.Black
                labelHoleRadius.Location = New Point(100, 160)
                PartsPanel.Controls.Add(labelHoleRadius)

                Dim textBoxHoleRadius As New System.Windows.Forms.TextBox()
                textBoxHoleRadius.Location = New Point(0, 160)
                textBoxHoleRadius.Font = New Font(textBoxHoleRadius.Font, FontStyle.Bold)
                textBoxHoleRadius.TextAlign = HorizontalAlignment.Right
                PartsPanel.Controls.Add(textBoxHoleRadius)
                AddHandler textBoxHoleRadius.TextChanged, AddressOf textBoxHoleRadius_TextChanged

            Case "HSS - Circle"
                Dim labelRadiusCircle As New Label()
                labelRadiusCircle.Text = "Radius (CM)"
                labelRadiusCircle.ForeColor = Color.Black
                labelRadiusCircle.Location = New Point(100, 10)
                PartsPanel.Controls.Add(labelRadiusCircle)

                Dim textBoxRadiusCircle As New System.Windows.Forms.TextBox()
                textBoxRadiusCircle.Location = New Point(0, 10)
                textBoxRadiusCircle.Font = New Font(textBoxRadiusCircle.Font, FontStyle.Bold)
                textBoxRadiusCircle.TextAlign = HorizontalAlignment.Right
                PartsPanel.Controls.Add(textBoxRadiusCircle)
                AddHandler textBoxRadiusCircle.TextChanged, AddressOf textBoxRadius_TextChanged

                Dim labelThicknessCircle As New Label()
                labelThicknessCircle.Text = "Thickness (MM)"
                labelThicknessCircle.ForeColor = Color.Black
                labelThicknessCircle.Location = New Point(100, 40)
                PartsPanel.Controls.Add(labelThicknessCircle)

                Dim textBoxThicknessCircle As New System.Windows.Forms.TextBox()
                textBoxThicknessCircle.Location = New Point(0, 40)
                textBoxThicknessCircle.Font = New Font(textBoxThicknessCircle.Font, FontStyle.Bold)
                textBoxThicknessCircle.TextAlign = HorizontalAlignment.Right
                PartsPanel.Controls.Add(textBoxThicknessCircle)
                AddHandler textBoxThicknessCircle.TextChanged, AddressOf textBoxThickness_TextChanged

                Dim labelLengthCircle As New Label()
                labelLengthCircle.Text = "Length (CM)"
                labelLengthCircle.ForeColor = Color.Black
                labelLengthCircle.Location = New Point(100, 70)
                PartsPanel.Controls.Add(labelLengthCircle)

                Dim textBoxLengthCircle As New System.Windows.Forms.TextBox()
                textBoxLengthCircle.Location = New Point(0, 70)
                textBoxLengthCircle.Font = New Font(textBoxLengthCircle.Font, FontStyle.Bold)
                textBoxLengthCircle.TextAlign = HorizontalAlignment.Right
                PartsPanel.Controls.Add(textBoxLengthCircle)
                AddHandler textBoxLengthCircle.TextChanged, AddressOf textBoxLength_TextChanged



            Case "HSS - Square"
                Dim labelHeightSquare As New Label()
                labelHeightSquare.Text = "Height (CM)"
                labelHeightSquare.ForeColor = Color.Black
                labelHeightSquare.Location = New Point(100, 10)
                PartsPanel.Controls.Add(labelHeightSquare)

                Dim textBoxHeightSquare As New System.Windows.Forms.TextBox()
                textBoxHeightSquare.Location = New Point(0, 10)
                textBoxHeightSquare.Font = New Font(textBoxHeightSquare.Font, FontStyle.Bold)
                textBoxHeightSquare.TextAlign = HorizontalAlignment.Right
                PartsPanel.Controls.Add(textBoxHeightSquare)
                AddHandler textBoxHeightSquare.TextChanged, AddressOf textBoxHeight_TextChanged

                Dim labelWidthSquare As New Label()
                labelWidthSquare.Text = "Width (CM)"
                labelWidthSquare.ForeColor = Color.Black
                labelWidthSquare.Location = New Point(100, 40)
                PartsPanel.Controls.Add(labelWidthSquare)

                Dim textBoxWidthSquare As New System.Windows.Forms.TextBox()
                textBoxWidthSquare.Location = New Point(0, 40)
                textBoxWidthSquare.Font = New Font(textBoxWidthSquare.Font, FontStyle.Bold)
                textBoxWidthSquare.TextAlign = HorizontalAlignment.Right
                PartsPanel.Controls.Add(textBoxWidthSquare)
                AddHandler textBoxWidthSquare.TextChanged, AddressOf textBoxWidth_TextChanged

                Dim labelThicknessSquare As New Label()
                labelThicknessSquare.Text = "Thickness (MM)"
                labelThicknessSquare.ForeColor = Color.Black
                labelThicknessSquare.Location = New Point(100, 70)
                PartsPanel.Controls.Add(labelThicknessSquare)

                Dim textBoxThicknessSquare As New System.Windows.Forms.TextBox()
                textBoxThicknessSquare.Location = New Point(0, 70)
                textBoxThicknessSquare.Font = New Font(textBoxThicknessSquare.Font, FontStyle.Bold)
                textBoxThicknessSquare.TextAlign = HorizontalAlignment.Right
                PartsPanel.Controls.Add(textBoxThicknessSquare)
                AddHandler textBoxThicknessSquare.TextChanged, AddressOf textBoxThickness_TextChanged

                Dim labelLengthSquare As New Label()
                labelLengthSquare.Text = "Length (CM)"
                labelLengthSquare.ForeColor = Color.Black
                labelLengthSquare.Location = New Point(100, 100)
                PartsPanel.Controls.Add(labelLengthSquare)

                Dim textBoxLengthSquare As New System.Windows.Forms.TextBox()
                textBoxLengthSquare.Location = New Point(0, 100)
                textBoxLengthSquare.Font = New Font(textBoxLengthSquare.Font, FontStyle.Bold)
                textBoxLengthSquare.TextAlign = HorizontalAlignment.Right
                PartsPanel.Controls.Add(textBoxLengthSquare)
                AddHandler textBoxLengthSquare.TextChanged, AddressOf textBoxLength_TextChanged

            Case "???"
                ' Add input controls
                ' Add your code
        End Select
    End Sub
    Private Sub ShapesComboBox_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ShapesComboBox.SelectedIndexChanged
        ' Clear existing controls
        ShapesPanel.Controls.Clear()

        ' Add input controls based on the selected item
        Select Case ShapesComboBox.SelectedItem.ToString()
            Case "Cube"
                Dim labelWidth As New Label()
                labelWidth.Text = "Width (CM)"
                labelWidth.ForeColor = Color.Black
                labelWidth.Location = New Point(100, 10)
                ShapesPanel.Controls.Add(labelWidth)

                Dim textBoxWidth As New System.Windows.Forms.TextBox()
                textBoxWidth.Location = New Point(0, 10)
                textBoxWidth.Font = New Font(textBoxWidth.Font, FontStyle.Bold)
                textBoxWidth.TextAlign = HorizontalAlignment.Right
                ShapesPanel.Controls.Add(textBoxWidth)
                AddHandler textBoxWidth.TextChanged, AddressOf textBoxWidth_TextChanged

                Dim labelHeight As New Label()
                labelHeight.Text = "Height (CM)"
                labelHeight.ForeColor = Color.Black
                labelHeight.Location = New Point(100, 40)
                ShapesPanel.Controls.Add(labelHeight)

                Dim textBoxHeight As New System.Windows.Forms.TextBox()
                textBoxHeight.Location = New Point(0, 40)
                textBoxHeight.Font = New Font(textBoxWidth.Font, FontStyle.Bold)
                textBoxHeight.TextAlign = HorizontalAlignment.Right
                ShapesPanel.Controls.Add(textBoxHeight)
                AddHandler textBoxHeight.TextChanged, AddressOf textBoxHeight_TextChanged

                Dim labelDepth As New Label()
                labelDepth.Text = "Depth (CM)"
                labelDepth.ForeColor = Color.Black
                labelDepth.Location = New Point(100, 70)
                ShapesPanel.Controls.Add(labelDepth)

                Dim textBoxDepth As New System.Windows.Forms.TextBox()
                textBoxDepth.Location = New Point(0, 70)
                textBoxDepth.Font = New Font(textBoxWidth.Font, FontStyle.Bold)
                textBoxDepth.TextAlign = HorizontalAlignment.Right
                ShapesPanel.Controls.Add(textBoxDepth)
                AddHandler textBoxDepth.TextChanged, AddressOf textBoxDepth_TextChanged

            Case "Cylinder"
                Dim labelHeight As New Label()
                labelHeight.Text = "Height (CM)"
                labelHeight.ForeColor = Color.Black
                labelHeight.Location = New Point(100, 10)
                ShapesPanel.Controls.Add(labelHeight)

                Dim textBoxHeight As New System.Windows.Forms.TextBox()
                textBoxHeight.Location = New Point(0, 10)
                textBoxHeight.Font = New Font(textBoxHeight.Font, FontStyle.Bold)
                textBoxHeight.TextAlign = HorizontalAlignment.Right
                ShapesPanel.Controls.Add(textBoxHeight)
                AddHandler textBoxHeight.TextChanged, AddressOf textBoxHeight_TextChanged

                Dim labelRadius As New Label()
                labelRadius.Text = "Radius (CM)"
                labelRadius.ForeColor = Color.Black
                labelRadius.Location = New Point(100, 40)
                ShapesPanel.Controls.Add(labelRadius)

                Dim textBoxRadius As New System.Windows.Forms.TextBox()
                textBoxRadius.Location = New Point(0, 40)
                textBoxRadius.Font = New Font(textBoxRadius.Font, FontStyle.Bold)
                textBoxRadius.TextAlign = HorizontalAlignment.Right
                ShapesPanel.Controls.Add(textBoxRadius)
                AddHandler textBoxRadius.TextChanged, AddressOf textBoxRadius_TextChanged
        End Select
    End Sub
End Class
