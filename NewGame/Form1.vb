Public Class Form1
    'Dim bmpTexture As Bitmap = New Bitmap(My.Resources.Tile_0)
    Dim bmpTexture As Bitmap = New Bitmap(My.Resources.tile_2)
    Dim Wyvern As PlayerModel
    Dim Grass(0 To 4) As Sprite
    Dim Water(0 To 4) As Sprite
    Dim rTile As Bitmap
    Dim Tile As Bitmap
    Dim Phase As Single
    Dim MouseKey As Boolean
    Dim picX As Integer
    Dim picY As Integer
    Dim mapWidth As Integer, mapHeight As Integer
    Dim fps As Integer, Counter1 As Integer, Counter2 As Integer
    Dim Warcraft As New Tileset(My.Resources.iso_64x64_outside)

    Public Structure Camera
        Public X As Integer
        Public Y As Integer
    End Structure

    WithEvents Timer1 As New Timer
    WithEvents TimerSeconds As New Timer

    Private Sub Form1_KeyDown(sender As Object, e As KeyEventArgs) Handles Me.KeyDown
        If e.KeyCode = Keys.Right Then
            Wyvern.Direction += 1
            If Wyvern.Direction > 7 Then Wyvern.Direction = 0
        End If
        If e.KeyCode = Keys.Left Then
            Wyvern.Direction -= 1
            If Wyvern.Direction < 0 Then Wyvern.Direction = 7
        End If
        If e.KeyCode = Keys.Up Then

            Counter1 += 1
            If Counter1 > 9 Then
                Counter1 = 0
                Counter2 += 1
                If Counter2 > 10 Then Counter2 = 10
            End If
        End If
        If e.KeyCode = Keys.Down Then
            Counter1 -= 1
            If Counter1 < 0 Then
                Counter1 = 9
                Counter2 -= 1
                If Counter2 < 0 Then Counter2 = 0
            End If
        End If
    End Sub
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim currentContext As BufferedGraphicsContext
        Dim myBuffer As BufferedGraphics
        currentContext = BufferedGraphicsManager.Current
        ' Creates a BufferedGraphics instance associated with Form1, and with 
        ' dimensions the same size as the drawing surface of Form1.
        myBuffer = currentContext.Allocate(Me.CreateGraphics,
           Me.DisplayRectangle)
        mapWidth = 640
        mapHeight = 640
        CameraMain.X = 0
        CameraMain.Y = 0
        Me.Top = 0
        Me.Left = 0
        Grass(0) = New Sprite(New Bitmap(My.Resources._0))
        With TimerSeconds
            .Interval = 1000
            .Enabled = True
        End With
        With Timer1
            .Interval = 25
            .Enabled = True
        End With

        Wyvern = New PlayerModel(0, 0, 256, 256, 0, 0, My.Resources.wyvern_composite)
        Tile = New Bitmap(mapWidth * 2, mapHeight * 2)
        rTile = New Bitmap(CInt(bmpTexture.Width * Math.Sqrt(2)), CInt(Math.Abs(bmpTexture.Height * Math.Sqrt(2))))
        Dim scr As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(Tile)
        Dim rscr As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(bmpTexture)
        For i = 0 To Math.Abs(mapWidth / Grass(0).sprImage.Width) + 20
            For j = 0 To Math.Abs(mapHeight / Grass(0).sprImage.Height) + 20
                Grass(0).Draw(scr, i * (40), j * (40))
            Next
        Next
        Me.Show()
        Do While True
            DrawBuffer(myBuffer.Graphics)
            myBuffer.Render(Me.CreateGraphics)
            'Me.Invalidate()
            Application.DoEvents()
        Loop
        currentContext.Dispose()
        myBuffer.Dispose()
    End Sub

    Private Sub Form1_MouseClick(sender As Object, e As MouseEventArgs) Handles Me.MouseClick
        If e.Button = Windows.Forms.MouseButtons.Right Then
            Wyvern.StartMove(New Point((picX - CameraMain.X + 2 * (picY - CameraMain.Y)) / 2, (-(picX - CameraMain.X) + 2 * (picY - CameraMain.Y)) / 2))
        End If
    End Sub
    Sub DrawBuffer(graf As Graphics)
        graf.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        graf.Clear(Color.Blue)
        For i = 0 To Math.Abs(mapWidth / 32) + 16
            For j = 0 To Math.Abs(mapHeight / 32) + 16
                'Grass(0).Draw(e.Graphics, i * (40), j * (40))
                Warcraft.Draw(graf, Counter2, Counter1, i * (32), j * (32))
            Next
        Next
        'Warcraft.Draw(e.Graphics, Counter2, Counter1, picX, picY)
        fps = fps + 1
        Wyvern.Draw(graf, Phase, CameraMain.X, CameraMain.Y)
    End Sub
    Private Sub Form1_MouseDown(sender As Object, e As MouseEventArgs) Handles Me.MouseDown
        MouseKey = True
        picX = e.X
        picY = e.Y
        Do While MouseKey = True
            Application.DoEvents()
            Me.Invalidate()
        Loop
    End Sub

    Private Sub Form1_MouseMove(sender As Object, e As MouseEventArgs) Handles Me.MouseMove
        If e.Button = Windows.Forms.MouseButtons.Left Then
            picX = picX - e.X
            CameraMain.X = CameraMain.X - picX
            picX = e.X
            picY = picY - e.Y
            CameraMain.Y = CameraMain.Y - picY
            picY = e.Y
        End If
    End Sub

    Private Sub Form1_MouseUp(sender As Object, e As MouseEventArgs) Handles Me.MouseUp
        MouseKey = False
    End Sub

    Private Sub Form1_Paint(sender As Object, e As PaintEventArgs) 'Handles Me.Paint
        e.Graphics.SmoothingMode = Drawing2D.SmoothingMode.HighQuality
        e.Graphics.Clear(Color.Blue)
        For i = 0 To Math.Abs(mapWidth / 32) + 16
            For j = 0 To Math.Abs(mapHeight / 32) + 16
                'Grass(0).Draw(e.Graphics, i * (40), j * (40))
                Warcraft.Draw(e.Graphics, Counter2, Counter1, i * (32), j * (32))
            Next
        Next
        'Warcraft.Draw(e.Graphics, Counter2, Counter1, picX, picY)
        fps = fps + 1
        Wyvern.Draw(e.Graphics, Phase, CameraMain.X, CameraMain.Y)
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick

        Phase = Math.Abs(Phase + 0.25)
        If Phase > 7 Then Phase = 0
    End Sub
    Private Sub TimeSeconds_Tick(sender As Object, e As EventArgs) Handles TimerSeconds.Tick
        Me.Text = fps
        fps = 0
    End Sub
End Class
