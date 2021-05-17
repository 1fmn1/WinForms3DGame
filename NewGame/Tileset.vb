Public Class Tileset
    Private zHeight As Integer, sprImage(,) As Bitmap
    Public Sub New(spriteImage As Bitmap)
        MyBase.New()
        ReDim sprImage(9, 10)
        For i = 0 To 9
            For j = 0 To 10
                sprImage(i, j) = New Bitmap(64, 64)
                Dim g As Graphics = Graphics.FromImage(sprImage(i, j))
                g.DrawImage(spriteImage, 0, 0, New Rectangle(j * 64, i * 64, 64, 64), GraphicsUnit.Pixel)
            Next
        Next
    End Sub
    Public Sub Draw(ByVal gr As Graphics, i As Integer, j As Integer, ByVal x As Integer, ByVal y As Integer)
        gr.DrawImage(sprImage(i, j), x - y + CameraMain.X - 32, (x + y) \ 2 + CameraMain.Y)
    End Sub
End Class
