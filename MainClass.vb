Imports System.Runtime.InteropServices
Imports System.Windows.Forms
Imports Inventor
Imports Microsoft.Win32
Imports WinFormsApp1
Imports MarshalHelper

Namespace InventorAuto
    <ProgIdAttribute("InventorAuto.StandardAddInServer"),
    GuidAttribute("da9fdfd2-9431-42e5-bf66-05f66eda6196")>
    Public Class StandardAddInServer
        Implements Inventor.ApplicationAddInServer

        Private WithEvents m_uiEvents As UserInterfaceEvents
        'Private WithEvents m_sampleButton As ButtonDefinition

#Region "ApplicationAddInServer Members"

        ' This method is called by Inventor when it loads the AddIn. The AddInSiteObject provides access  
        ' to the Inventor Application object. The FirstTime flag indicates if the AddIn is loaded for
        ' the first time. However, with the introduction of the ribbon this argument is always true.
        Public Sub Activate(ByVal addInSiteObject As Inventor.ApplicationAddInSite, ByVal firstTime As Boolean) Implements Inventor.ApplicationAddInServer.Activate
            ' Initialize AddIn members.
            g_inventorApplication = addInSiteObject.Application

            ' Connect to the user-interface events to handle a ribbon reset.
            m_uiEvents = g_inventorApplication.UserInterfaceManager.UserInterfaceEvents

            ' TODO: Add button definitions.

            ' Sample to illustrate creating a button definition.
            'Dim largeIcon As stdole.IPictureDisp = PictureDispConverter.ToIPictureDisp(My.Resources.YourBigImage)
            'Dim smallIcon As stdole.IPictureDisp = PictureDispConverter.ToIPictureDisp(My.Resources.YourSmallImage)
            'Dim controlDefs As Inventor.ControlDefinitions = g_inventorApplication.CommandManager.ControlDefinitions
            'm_sampleButton = controlDefs.AddButtonDefinition("Command Name", "Internal Name", CommandTypesEnum.kShapeEditCmdType, AddInClientID)

            ' Add to the user interface, if it's the first time.
            If firstTime Then
                AddToUserInterface()
            End If
        End Sub

        ' This method is called by Inventor when the AddIn is unloaded. The AddIn will be
        ' unloaded either manually by the user or when the Inventor session is terminated.
        Public Sub Deactivate() Implements Inventor.ApplicationAddInServer.Deactivate

            ' TODO:  Add ApplicationAddInServer.Deactivate implementation

            ' Release objects.
            m_uiEvents = Nothing
            g_inventorApplication = Nothing

            System.GC.Collect()
            System.GC.WaitForPendingFinalizers()
        End Sub

        ' This property is provided to allow the AddIn to expose an API of its own to other 
        ' programs. Typically, this  would be done by implementing the AddIn's API
        ' interface in a class and returning that class object through this property.
        Public ReadOnly Property Automation() As Object Implements Inventor.ApplicationAddInServer.Automation
            Get
                Return Nothing
            End Get
        End Property

        ' Note:this method is now obsolete, you should use the 
        ' ControlDefinition functionality for implementing commands.
        Public Sub ExecuteCommand(ByVal commandID As Integer) Implements Inventor.ApplicationAddInServer.ExecuteCommand
        End Sub

#End Region

#Region "User interface definition"
        ' Sub where the user-interface creation is done.  This is called when
        ' the add-in loaded and also if the user interface is reset.
        Private Sub AddToUserInterface()
            ' This is where you'll add code to add buttons to the ribbon.

            '** Sample to illustrate creating a button on a new panel of the Tools tab of the Part ribbon.

            '' Get the part ribbon.
            'Dim partRibbon As Ribbon = g_inventorApplication.UserInterfaceManager.Ribbons.Item("Part")

            '' Get the "Tools" tab.
            'Dim toolsTab As RibbonTab = partRibbon.RibbonTabs.Item("id_TabTools")

            '' Create a new panel.
            'Dim customPanel As RibbonPanel = toolsTab.RibbonPanels.Add("Sample", "MysSample", AddInClientID)

            '' Add a button.
            'customPanel.CommandControls.AddButton(m_sampleButton)
        End Sub

        Private Sub m_uiEvents_OnResetRibbonInterface(Context As NameValueMap) Handles m_uiEvents.OnResetRibbonInterface
            ' The ribbon was reset, so add back the add-ins user-interface.
            AddToUserInterface()
        End Sub

        ' Sample handler for the button.
        'Private Sub m_sampleButton_OnExecute(Context As NameValueMap) Handles m_sampleButton.OnExecute
        '    MsgBox("Button was clicked.")
        'End Sub
#End Region

    End Class
End Namespace
Public Module Globals
    ' Inventor application object.
    Public g_inventorApplication As Inventor.Application

#Region "Function to get the add-in client ID."
    ' This function uses reflection to get the GuidAttribute associated with the add-in.
    Public Function AddInClientID() As String
        Dim guid As String = ""
        Try
            Dim t As Type = GetType(InventorAuto.StandardAddInServer)
            Dim customAttributes() As Object = t.GetCustomAttributes(GetType(GuidAttribute), False)
            Dim guidAttribute As GuidAttribute = CType(customAttributes(0), GuidAttribute)
            guid = "{" + guidAttribute.Value.ToString() + "}"
        Catch
        End Try

        Return guid
    End Function
#End Region

#Region "hWnd Wrapper Class"
    ' This class is used to wrap a Win32 hWnd as a .Net IWind32Window class.
    ' This is primarily used for parenting a dialog to the Inventor window.
    '
    ' For example:
    ' myForm.Show(New WindowWrapper(g_inventorApplication.MainFrameHWND))
    '
    Public Class WindowWrapper
        Implements System.Windows.Forms.IWin32Window
        Public Sub New(ByVal handle As IntPtr)
            _hwnd = handle
        End Sub

        Public ReadOnly Property Handle() As IntPtr _
          Implements System.Windows.Forms.IWin32Window.Handle
            Get
                Return _hwnd
            End Get
        End Property

        Private _hwnd As IntPtr
    End Class
#End Region

#Region "Image Converter"
    ' Class used to convert bitmaps and icons from their .Net native types into
    ' an IPictureDisp object which is what the Inventor API requires. A typical
    ' usage is shown below where MyIcon is a bitmap or icon that's available
    ' as a resource of the project.
    '
    ' Dim smallIcon As stdole.IPictureDisp = PictureDispConverter.ToIPictureDisp(My.Resources.MyIcon)

    Public NotInheritable Class PictureDispConverter
        <DllImport("OleAut32.dll", EntryPoint:="OleCreatePictureIndirect", ExactSpelling:=True, PreserveSig:=False)>
        Private Shared Function OleCreatePictureIndirect(
            <MarshalAs(UnmanagedType.AsAny)> ByVal picdesc As Object,
            ByRef iid As Guid,
            <MarshalAs(UnmanagedType.Bool)> ByVal fOwn As Boolean) As stdole.IPictureDisp
        End Function

        Shared iPictureDispGuid As Guid = GetType(stdole.IPictureDisp).GUID

        Private NotInheritable Class PICTDESC
            Private Sub New()
            End Sub

            'Picture Types
            Public Const PICTYPE_BITMAP As Short = 1
            Public Const PICTYPE_ICON As Short = 3

            <StructLayout(LayoutKind.Sequential)>
            Public Class Icon
                Friend cbSizeOfStruct As Integer = Marshal.SizeOf(GetType(PICTDESC.Icon))
                Friend picType As Integer = PICTDESC.PICTYPE_ICON
                Friend hicon As IntPtr = IntPtr.Zero
                Friend unused1 As Integer
                Friend unused2 As Integer

                Friend Sub New(ByVal icon As System.Drawing.Icon)
                    Me.hicon = icon.ToBitmap().GetHicon()
                End Sub
            End Class

            <StructLayout(LayoutKind.Sequential)>
            Public Class Bitmap
                Friend cbSizeOfStruct As Integer = Marshal.SizeOf(GetType(PICTDESC.Bitmap))
                Friend picType As Integer = PICTDESC.PICTYPE_BITMAP
                Friend hbitmap As IntPtr = IntPtr.Zero
                Friend hpal As IntPtr = IntPtr.Zero
                Friend unused As Integer

                Friend Sub New(ByVal bitmap As System.Drawing.Bitmap)
                    Me.hbitmap = bitmap.GetHbitmap()
                End Sub
            End Class
        End Class

        Public Shared Function ToIPictureDisp(ByVal icon As System.Drawing.Icon) As stdole.IPictureDisp
            Dim pictIcon As New PICTDESC.Icon(icon)
            Return OleCreatePictureIndirect(pictIcon, iPictureDispGuid, True)
        End Function

        Public Shared Function ToIPictureDisp(ByVal bmp As System.Drawing.Bitmap) As stdole.IPictureDisp
            Dim pictBmp As New PICTDESC.Bitmap(bmp)
            Return OleCreatePictureIndirect(pictBmp, iPictureDispGuid, True)
        End Function
    End Class
#End Region

End Module

' Finally Create MainClass

Public Class MainClass
    Public Shared Sub Main(args As String())
        Dim inventorInstance As Inventor.Application
        Try
            Dim form As New Form1()
            form.ShowDialog()

            If form.DialogResult = DialogResult.OK Then
                Dim selectedOption As String = form.SelectedOption
                Console.WriteLine("Selected Option: " & selectedOption) ' For Debugging Purposes

                ' Check User Inputs
                Console.WriteLine("User Inputs:")
                Console.WriteLine()
                Console.WriteLine($"Width: {If(form.Width <> 0, form.Width.ToString(), "N/A")}")
                Console.WriteLine($"Height: {If(form.Height <> 0, form.Height.ToString(), "N/A")}")
                Console.WriteLine($"Depth: {If(form.Depth <> 0, form.Depth.ToString(), "N/A")}")
                Console.WriteLine($"Radius: {If(form.Radius <> 0, form.Radius.ToString(), "N/A")}")
                Console.WriteLine($"Wingspan: {If(form.Wingspan <> 0, form.Wingspan.ToString(), "N/A")}")
                Console.WriteLine($"numBlades: {If(form.numBlades <> 0, form.numBlades.ToString(), "N/A")}")
                Console.WriteLine($"HoleRadius: {If(form.HoleRadius <> 0, form.HoleRadius.ToString(), "N/A")}")
                Console.WriteLine($"Length: {If(form.Length <> 0, form.Length.ToString(), "N/A")}")
                Console.WriteLine($"Thickness: {If(form.Thickness <> 0, form.Thickness.ToString(), "N/A")}")
                Console.WriteLine($"numHoles: {If(form.numHoles <> 0, form.numHoles.ToString(), "N/A")}")
                Console.WriteLine()

                Select Case selectedOption

                                            ' Shapes '
                        ' ------------------------------------------------- '
                    Case "Cube"
                        Dim width As Double = (form.Width * form.Inch)
                        Dim height As Double = (form.Height * form.Inch)
                        Dim depth As Double = (form.Depth * form.Inch)

                        Dim cubeDrawer As New CubeDrawing.Cube()
                        cubeDrawer.DrawCube(width, height, depth)

                    Case "Cylinder"
                        Dim radius As Double = (form.Radius * 2.54)
                        Dim height As Double = (form.Height * 2.54)

                        Dim cylinderDrawer As New CylinderDrawing.Cylinder()
                        cylinderDrawer.CreateCylinder(radius, height)


                                             ' Parts '
                        ' ------------------------------------------------- '

                    Case "Propeller"
                        Dim radius As Double = (form.Radius * 2.54)
                        Dim height As Double = (form.Height * 2.54)
                        Dim wingspan As Double = (form.Wingspan * 2.54)
                        Dim holeRadius As Double = (form.HoleRadius * 2.54)
                        Dim numBlades As Double = (form.numBlades * 2.54)

                        Dim propellerDrawer As New PropellerDrawing.Propeller()
                        propellerDrawer.CreatePropellerBase(radius, height, holeRadius, wingspan, numBlades)

                    Case "L Beam"
                        Dim height As Double = (form.Height * 2.54)
                        Dim width As Double = (form.Width * 2.54)
                        Dim thickness As Double = (form.Thickness * 2.54)
                        Dim length As Double = (form.Length * 2.54)
                        Dim numHoles As Double = (form.numHoles * 2.54)
                        Dim holeRadius As Double = (form.HoleRadius * 2.54)

                        Dim lBeamDrawer As New LBeamDrawing.LBeam()
                        lBeamDrawer.DrawLBeam(height, width, thickness, length, numHoles, holeRadius)


                    Case "HSS - Circle"
                        Dim radius As Double = (form.Radius * 2.54)
                        Dim thickness As Double = (form.Thickness * 2.54)
                        Dim length As Double = (form.Length * 2.54)

                        Dim hssCircleDrawer As New HSSCircleDrawing.HSSCircle()
                        hssCircleDrawer.DrawHSSCircle(radius, thickness, length)

                    Case "HSS - Square"
                        Dim height As Double = (form.Height * 2.54)
                        Dim width As Double = (form.Width * 2.54)
                        Dim thickness As Double = (form.Thickness * 2.54)
                        Dim length As Double = (form.Length * 2.54)
                        Dim holeRadius As Double = (form.HoleRadius * 2.54)
                        Dim numHoles As Double = (form.numHoles * 2.54)


                        Dim hssSquareDrawer As New HSSSquareDrawing.HSSSquare()
                        hssSquareDrawer.DrawHSSSquare(height, width, thickness, length, holeRadius, numHoles)

                    Case "Channel"
                        Dim height As Double = (form.Height * 2.54)
                        Dim width As Double = (form.Width * 2.54)
                        Dim thickness As Double = (form.Thickness * 2.54)
                        Dim length As Double = (form.Length * 2.54)

                        Dim channelDrawer As New ChannelDrawing.Channel()
                        channelDrawer.DrawChannel(height, width, thickness, length)

                                           ' Wood Parts '
                        ' ------------------------------------------------- '

                    Case "2X4"
                        Dim width As Double = (1.5 * form.Inch)
                        Dim height As Double = (3.5 * form.Inch)
                        Dim depth As Double = (form.Depth * form.Inch)

                        Dim TwoXFourDrawer As New TwoXFourDrawing.TwoXFour()
                        TwoXFourDrawer.DrawTwoXFour(width, height, depth)

                    Case "4X4"
                        Dim width As Double = (3.5 * form.Inch)
                        Dim height As Double = (3.5 * form.Inch)
                        Dim depth As Double = (form.Depth * form.Inch)

                        Dim FourXFourDrawer As New FourXFourDrawing.FourXFour()
                        FourXFourDrawer.DrawFourXFour(width, height, depth)

                    Case "StandardTop"
                        Dim width As Double = (form.Width * form.Inch)
                        Dim height As Double = (form.Height * form.Inch)
                        Dim depth As Double = (form.Depth * form.Inch)

                        Console.WriteLine($"User Inputs - Height: {form.Height}, Width: {form.Width}, Depth: {form.Depth},")

                        Dim StandardTopDrawer As New StandardTopDrawing.StandardTop()
                        StandardTopDrawer.DrawStandardTop(width, height, depth)

                        '                ' Assembly Parts '
                        ' ------------------------------------------------- '
                    Case "BasicTable"
                        Dim width As Double = (1.5 * form.Inch)
                        Dim height As Double = (3.5 * form.Inch)
                        Dim depth As Double = ((form.Depth - form.Thickness) * form.Inch)
                        Dim Thickness As Double = ((form.Thickness) * form.Inch)

                        ' Assign Seperate values for first part
                        Dim widthA As Double = width
                        Dim heightA As Double = height
                        Dim depthA As Double = depth

                        Dim TwoXFourDrawer As New TwoXFourDrawing.TwoXFour()
                        TwoXFourDrawer.DrawTwoXFour(width, height, depth)

                        width = (form.Width * form.Inch)
                        height = (form.Height * form.Inch)
                        depth = (form.Thickness * form.Inch)

                        Dim StandardTopDrawer As New StandardTopDrawing.StandardTop()
                        StandardTopDrawer.DrawStandardTop(width, height, depth)

                        Dim BasicTableAssembler As New BasicTableAssembly.BasicTable()
                        BasicTableAssembler.AssembleBasicTable(width, height, depth, widthA, heightA, depthA, Thickness)
                    Case Else
                        Console.WriteLine("Option not recognized.")
                End Select
            Else
                Console.WriteLine("User canceled the operation.")
            End If
        Catch ex As Exception
            Console.WriteLine("An error occurred: " & ex.Message)
        End Try
    End Sub
End Class


