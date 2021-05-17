Public Class Sprite
    Public zHeight As Integer, sprImage As Bitmap
    Public Sub New(spriteImage As Bitmap)
        MyBase.New()
        Me.sprImage = New Bitmap(spriteImage)
    End Sub
    Public Sub Draw(ByVal gr As Graphics, ByVal x As Integer, ByVal y As Integer)
        gr.DrawImage(sprImage, x - y + CameraMain.X, (x + y) \ 2 + CameraMain.Y)
    End Sub
End Class
