Imports Microsoft.Office.Interop

Namespace CubeDrawing
    Public Class ExcelHandler
        Public Shared Function GetCubeLength() As Double
            Dim cubeLength As Double = 0.0
            Try
                Dim excelFilePath As String = "||EXCEL FILE||"
                Dim excelApp As New Excel.Application()
                Dim excelWorkbook As Excel.Workbook = excelApp.Workbooks.Open(excelFilePath)

                Dim excelWorksheet As Excel.Worksheet = excelWorkbook.Sheets(1) ' Assuming data is in the first sheet
                cubeLength = CDbl(excelWorksheet.Cells(1, 1).Value) ' Assuming the length is in cell A1
                excelWorkbook.Close(False)
                excelApp.Quit()
            Catch ex As Exception
                Console.WriteLine("An error occurred while reading cube length from Excel: " & ex.Message)
            End Try
            Return cubeLength
        End Function
    End Class
End Namespace
