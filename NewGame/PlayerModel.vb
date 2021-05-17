Public Class PlayerModel
    Public x As Integer, y As Integer, SprCnt As Long, addX As Integer, addY As Integer, Direction As Integer, _
        isMoving As Boolean, Destination As Point, Speed As Integer
    Dim spr(,) As Bitmap
    Public Sub New(ByVal CntX As Integer, ByVal CntY As Integer, ByVal Width As Integer, ByVal Height As Integer, ByVal aX As Integer, ByVal aY As Integer, fName As Bitmap, Optional MinKoef As Double = 1, Optional CCKey As Long = 0)
        MyBase.New()
        SprCnt = CntY
        addX = aX
        addY = aY
        Speed = 5
        x = 20
        y = 20
        ReDim spr(7, 15)
        For i = 0 To 7
            For j = 0 To 15
                spr(i, j) = New Bitmap(350, 350)
                Dim g As Graphics = Graphics.FromImage(spr(i, j))
                g.DrawImage(fName, 0, 0, New Rectangle(j * Width, i * Height, Width, Height), GraphicsUnit.Pixel)
            Next
        Next
    End Sub
    Public Sub Draw(ByVal g As Graphics, ByVal Phase As Single, destX As Integer, destY As Integer)
        'If Phase > spr.GetLength(1) - 1 Then MsgBox("err") : Exit Sub
        If isMoving = True Then
            Phase = Phase + 8
            If Math.Abs(Me.x - Destination.X) < 5 Then
                Me.x = Destination.X
            End If
            If Me.x < Destination.X Then
                Me.x += Me.Speed
                Direction = 5
            ElseIf Me.x > Destination.X Then
                Me.x -= Me.Speed
                Direction = 1
            End If
            If Math.Abs(Me.y - Destination.Y) < 5 Then
                Me.y = Destination.Y
            End If
            If Me.x = Destination.X Then
                If Me.y < Destination.Y Then
                    Me.y += Me.Speed
                    Direction = 7
                ElseIf Me.y > Destination.Y Then
                    Me.y -= Me.Speed
                    Direction = 3
                End If
            End If
            If Me.x = Destination.X And Me.y = Destination.Y Then isMoving = False
        End If
        g.DrawImage(spr(Direction, Phase), New Point(x - y + destX - 180, (x + y) / 2 + destY - 225))
        'g.DrawImage(spr(0, 0), New Point(destX - destY, (destX + destY) / 2))
    End Sub
    Public Sub StartMove(ByVal Dest As Point)
        Destination = Dest
        isMoving = True
    End Sub
End Class
