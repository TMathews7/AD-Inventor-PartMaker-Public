<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class Form1
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        MainTabControl = New TabControl()
        ShapesTabPage = New TabPage()
        ShapesPanel = New Panel()
        ShapesComboBox = New ComboBox()
        PartsTabPage = New TabPage()
        PartsPanel = New Panel()
        PartsComboBox = New ComboBox()
        MainTabControl.SuspendLayout()
        ShapesTabPage.SuspendLayout()
        PartsTabPage.SuspendLayout()
        SuspendLayout()
        ' 
        ' MainTabControl
        ' 
        MainTabControl.Controls.Add(ShapesTabPage)
        MainTabControl.Controls.Add(PartsTabPage)
        MainTabControl.Location = New Point(12, 12)
        MainTabControl.Name = "MainTabControl"
        MainTabControl.SelectedIndex = 0
        MainTabControl.Size = New Size(234, 377)
        MainTabControl.TabIndex = 0
        ' 
        ' ShapesTabPage
        ' 
        ShapesTabPage.Controls.Add(ShapesPanel)
        ShapesTabPage.Controls.Add(ShapesComboBox)
        ShapesTabPage.Location = New Point(4, 24)
        ShapesTabPage.Name = "ShapesTabPage"
        ShapesTabPage.Padding = New Padding(3)
        ShapesTabPage.Size = New Size(226, 349)
        ShapesTabPage.TabIndex = 0
        ShapesTabPage.Text = "Shapes"
        ShapesTabPage.UseVisualStyleBackColor = True
        ' 
        ' ShapesPanel
        ' 
        ShapesPanel.Location = New Point(6, 35)
        ShapesPanel.Name = "ShapesPanel"
        ShapesPanel.Size = New Size(214, 308)
        ShapesPanel.TabIndex = 1
        ' 
        ' ShapesComboBox
        ' 
        ShapesComboBox.FormattingEnabled = True
        ShapesComboBox.Items.AddRange(New Object() {"Cube", "Cylinder"})
        ShapesComboBox.Location = New Point(6, 6)
        ShapesComboBox.Name = "ShapesComboBox"
        ShapesComboBox.Size = New Size(214, 23)
        ShapesComboBox.TabIndex = 2
        ' 
        ' PartsTabPage
        ' 
        PartsTabPage.Controls.Add(PartsPanel)
        PartsTabPage.Controls.Add(PartsComboBox)
        PartsTabPage.Location = New Point(4, 24)
        PartsTabPage.Name = "PartsTabPage"
        PartsTabPage.Padding = New Padding(3)
        PartsTabPage.Size = New Size(226, 349)
        PartsTabPage.TabIndex = 1
        PartsTabPage.Text = "Parts"
        PartsTabPage.UseVisualStyleBackColor = True
        ' 
        ' PartsPanel
        ' 
        PartsPanel.Location = New Point(6, 35)
        PartsPanel.Name = "PartsPanel"
        PartsPanel.Size = New Size(214, 308)
        PartsPanel.TabIndex = 1
        ' 
        ' PartsComboBox
        ' 
        PartsComboBox.FormattingEnabled = True
        PartsComboBox.Items.AddRange(New Object() {"Propeller", "Channel", "L Beam", "HSS - Circle", "HSS - Square"})
        PartsComboBox.Location = New Point(6, 6)
        PartsComboBox.Name = "PartsComboBox"
        PartsComboBox.Size = New Size(214, 23)
        PartsComboBox.TabIndex = 0
        ' 
        ' Form1
        ' 
        AutoScaleDimensions = New SizeF(7.0F, 15.0F)
        AutoScaleMode = AutoScaleMode.Font
        BackColor = SystemColors.AppWorkspace
        ClientSize = New Size(253, 450)
        Controls.Add(MainTabControl)
        Name = "Form1"
        Text = "Inventor"
        MainTabControl.ResumeLayout(False)
        ShapesTabPage.ResumeLayout(False)
        PartsTabPage.ResumeLayout(False)
        ResumeLayout(False)
    End Sub

    Public WithEvents MainTabControl As TabControl
    Public WithEvents ShapesTabPage As TabPage
    Public WithEvents ShapesComboBox As ComboBox
    Public WithEvents ShapesPanel As Panel
    Public WithEvents PartsTabPage As TabPage
    Public WithEvents PartsComboBox As ComboBox
    Public WithEvents PartsPanel As Panel

End Class
