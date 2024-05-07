ublic Class GUI
    Public Property Length As Double
    Public Property Height As Double
    Public Property Width As Double
    Public Property Depth As Double

    Public Sub PromptUser()
        Console.WriteLine("Please enter the dimensions of the cube:")
        Console.Write("Length: ")
        Length = Convert.ToDouble(Console.ReadLine())
        Console.Write("Height: ")
        Height = Convert.ToDouble(Console.ReadLine())
        Console.Write("Width: ")
        Width = Convert.ToDouble(Console.ReadLine())
        Console.Write("Depth: ")
        Depth = Convert.ToDouble(Console.ReadLine())
    End Sub
End Class
