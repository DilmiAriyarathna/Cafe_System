Public Class Home_Page
    Private Sub Panel1_Paint(sender As Object, e As PaintEventArgs)

    End Sub

    Private Sub Home_Page_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'Cafe_SYSDataSet6.Products' table. You can move, or remove it, as needed.
        Me.ProductsTableAdapter.Fill(Me.Cafe_SYSDataSet6.Products)
        Timer1.Start()

        Label35.Text = Form1.setname
    End Sub

    Private Sub Panel2_Paint(sender As Object, e As PaintEventArgs) Handles Panel2.Paint

    End Sub

    Private Sub Label4_Click(sender As Object, e As EventArgs) Handles Label4.Click

    End Sub

    Private Sub BunifuThinButton21_Click(sender As Object, e As EventArgs) Handles BunifuThinButton21.Click
        Application.Exit()
    End Sub

    Private Sub Timer1_Tick(sender As Object, e As EventArgs) Handles Timer1.Tick
        Label14.Text = DateTime.Now.ToString("hh:mm:ss tt")
        Label13.Text = DateTime.Now.ToString("MM/dd/yyyy")
    End Sub

    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Form23.Show()
    End Sub
End Class