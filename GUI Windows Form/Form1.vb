Imports System.Windows.Forms

Public Class Form1
    Inherits System.Windows.Forms.Form

    ' Properties for the dimensions of different objects
    Public Property Width As Double
    Public Property Height As Double
    Public Property Depth As Double
    Public Property Radius As Double
    Public Property Wingspan As Double
    Public Property SelectedOption As String

    ' Form1 initialization code
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ' Add items when the form loads
        ComboBox1.Items.Add("Cube")
        ComboBox1.Items.Add("Propeller")
        ComboBox1.Items.Add("???")

        ' Initialize OK and Cancel buttons
        Dim btnOK As New Button()
        btnOK.Text = "OK"
        btnOK.ForeColor = Color.White
        btnOK.DialogResult = DialogResult.OK
        btnOK.Location = New Point(10, Me.ClientSize.Height - btnOK.Height - 10) ' Align to bottom-left corner
        Me.Controls.Add(btnOK)

        Dim btnCancel As New Button()
        btnCancel.Text = "Cancel"
        btnCancel.ForeColor = Color.White
        btnCancel.DialogResult = DialogResult.Cancel
        btnCancel.Location = New Point(Me.ClientSize.Width - btnCancel.Width - 10, Me.ClientSize.Height - btnCancel.Height - 10) ' Align to bottom-right corner
        Me.Controls.Add(btnCancel)

        ' Add event handlers for OK and Cancel buttons
        AddHandler btnOK.Click, AddressOf btnOK_Click
        AddHandler btnCancel.Click, AddressOf btnCancel_Click
    End Sub

    ' Event handler for OK button
    Private Sub btnOK_Click(sender As Object, e As EventArgs)
        ' Set the SelectedOption property to the selected item in the ComboBox
        SelectedOption = ComboBox1.SelectedItem.ToString()

        ' Set the DialogResult to OK to indicate that the user clicked OK
        Me.DialogResult = DialogResult.OK
        Me.Close() ' Close the form immediately after setting DialogResult
    End Sub

    ' Event handler for Cancel button
    Private Sub btnCancel_Click(sender As Object, e As EventArgs)
        ' Set the DialogResult to Cancel to indicate that the user clicked Cancel
        Me.DialogResult = DialogResult.Cancel
        Me.Close() ' Close the form immediately after setting DialogResult
    End Sub

    ' Event handler for ComboBox selection change
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        ' Clear existing controls
        Panel1.Controls.Clear()

        ' Add input controls based on the selected item
        Select Case ComboBox1.SelectedItem.ToString()
            Case "Cube"
                Dim labelWidth As New Label()
                labelWidth.Text = "Width (CM)"
                labelWidth.ForeColor = Color.White
                labelWidth.Location = New Point(100, 10)
                Panel1.Controls.Add(labelWidth)

                Dim textBoxWidth As New TextBox()
                textBoxWidth.Location = New Point(10 - 15, 10)
                textBoxWidth.Font = New Font(textBoxWidth.Font, FontStyle.Bold)
                textBoxWidth.TextAlign = HorizontalAlignment.Right
                Panel1.Controls.Add(textBoxWidth)
                AddHandler textBoxWidth.TextChanged, AddressOf textBoxWidth_TextChanged

                Dim labelHeight As New Label()
                labelHeight.Text = "Height (CM)"
                labelHeight.ForeColor = Color.White
                labelHeight.Location = New Point(100, 40)
                Panel1.Controls.Add(labelHeight)

                Dim textBoxHeight As New TextBox()
                textBoxHeight.Location = New Point(10 - 15, 40)
                textBoxHeight.Font = New Font(textBoxWidth.Font, FontStyle.Bold)
                textBoxHeight.TextAlign = HorizontalAlignment.Right
                Panel1.Controls.Add(textBoxHeight)
                AddHandler textBoxHeight.TextChanged, AddressOf textBoxHeight_TextChanged

                Dim labelDepth As New Label()
                labelDepth.Text = "Depth (CM)"
                labelDepth.ForeColor = Color.White
                labelDepth.Location = New Point(100, 70)
                Panel1.Controls.Add(labelDepth)

                Dim textBoxDepth As New TextBox()
                textBoxDepth.Location = New Point(10 - 15, 70)
                textBoxDepth.Font = New Font(textBoxWidth.Font, FontStyle.Bold)
                textBoxDepth.TextAlign = HorizontalAlignment.Right
                Panel1.Controls.Add(textBoxDepth)
                AddHandler textBoxDepth.TextChanged, AddressOf textBoxDepth_TextChanged

            Case "Propeller"
                Dim labelHeight As New Label()
                labelHeight.Text = "Height (CM)"
                labelHeight.ForeColor = Color.White
                labelHeight.Location = New Point(100, 10)
                Panel1.Controls.Add(labelHeight)

                Dim textBoxHeight As New TextBox()
                textBoxHeight.Location = New Point(10 - 15, 10)
                textBoxHeight.Font = New Font(textBoxHeight.Font, FontStyle.Bold)
                textBoxHeight.TextAlign = HorizontalAlignment.Right
                Panel1.Controls.Add(textBoxHeight)
                AddHandler textBoxHeight.TextChanged, AddressOf textBoxHeight_TextChanged

                Dim labelRadius As New Label()
                labelRadius.Text = "Radius (CM)"
                labelRadius.ForeColor = Color.White
                labelRadius.Location = New Point(100, 40)
                Panel1.Controls.Add(labelRadius)

                Dim textBoxRadius As New TextBox()
                textBoxRadius.Location = New Point(10 - 15, 40)
                textBoxRadius.Font = New Font(textBoxRadius.Font, FontStyle.Bold)
                textBoxRadius.TextAlign = HorizontalAlignment.Right
                Panel1.Controls.Add(textBoxRadius)
                AddHandler textBoxRadius.TextChanged, AddressOf textBoxRadius_TextChanged

                Dim labelWingspan As New Label()
                labelWingspan.Text = "Wingspan (CM)"
                labelWingspan.ForeColor = Color.White
                labelWingspan.Location = New Point(100, 70)
                Panel1.Controls.Add(labelWingspan)

                Dim textBoxWingspan As New TextBox()
                textBoxWingspan.Location = New Point(10 - 15, 70)
                textBoxWingspan.Font = New Font(textBoxWingspan.Font, FontStyle.Bold)
                textBoxWingspan.TextAlign = HorizontalAlignment.Right
                Panel1.Controls.Add(textBoxWingspan)
                AddHandler textBoxWingspan.TextChanged, AddressOf textBoxWingspan_TextChanged


            Case "???"
                ' Add input controls
                ' Add your code
        End Select
    End Sub
    Private Sub textBoxWidth_TextChanged(sender As Object, e As EventArgs)
        Dim widthValue As Double
        If Double.TryParse(CType(sender, TextBox).Text, widthValue) Then
            Width = widthValue
        End If
    End Sub
    Private Sub textBoxHeight_TextChanged(sender As Object, e As EventArgs)
        Dim heightValue As Double
        If Double.TryParse(CType(sender, TextBox).Text, heightValue) Then
            Height = heightValue
        End If
    End Sub
    Private Sub textBoxDepth_TextChanged(sender As Object, e As EventArgs)
        Dim depthValue As Double
        If Double.TryParse(CType(sender, TextBox).Text, depthValue) Then
            Depth = depthValue
        End If
    End Sub
    Private Sub textBoxRadius_TextChanged(sender As Object, e As EventArgs)
        Dim radiusValue As Double
        If Double.TryParse(CType(sender, TextBox).Text, radiusValue) Then
            Radius = radiusValue
        End If
    End Sub
    Private Sub textBoxWingspan_TextChanged(sender As Object, e As EventArgs)
        Dim wingspanValue As Double
        If Double.TryParse(CType(sender, TextBox).Text, wingspanValue) Then
            Wingspan = wingspanValue
        End If
    End Sub
End Class
