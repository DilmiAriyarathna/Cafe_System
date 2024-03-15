Public Class Form_Main
    Private Sub Label5_Click(sender As Object, e As EventArgs)
        ' Form2.Show()
    End Sub

    Private Sub BunifuThinButton21_Click(sender As Object, e As EventArgs) Handles BunifuThinButton21.Click
        Dim result As DialogResult = MsgBox("Do you want exit ?", vbYesNo, "The Garden Cafe")
        If (result = vbYes) Then
            Application.Exit()
        End If
    End Sub

    Private Sub Form_Main_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TODO: This line of code loads data into the 'Cafe_SYSDataSet5.Products' table. You can move, or remove it, as needed.
        Me.ProductsTableAdapter1.Fill(Me.Cafe_SYSDataSet5.Products)
        'TODO: This line of code loads data into the 'Cafe_SYSDataSet4.Products' table. You can move, or remove it, as needed.
        '//     Me.ProductsTableAdapter.Fill(Me.Cafe_SYSDataSet4.Products)

        Label3.Text = Form1.setname
    End Sub

    Private Sub BunifuDropdown1_onItemSelected(sender As Object, e As EventArgs)

    End Sub

    Private Sub BunifuFlatButton1_Click(sender As Object, e As EventArgs) 

    End Sub

    Private Sub BunifuSeparator1_Load(sender As Object, e As EventArgs) Handles BunifuSeparator1.Load

    End Sub
End Class