Imports System.Data.SqlClient
Imports System.Data
Imports System.IO
Public Class Form22
    Private Sub Button1_Click(sender As Object, e As EventArgs)

    End Sub

    Private Sub PrintDocument1_PrintPage(sender As Object, e As Printing.PrintPageEventArgs)

    End Sub
    Private Sub print()

    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)
        'conn = GetConnect()
        'conn.Open()
        'SQL = "select * from Order_List where Order_No = '" + TextBox1.Text + "' "
        'cmd = New SqlCommand(SQL, conn)
        'dt = New DataTable()
        'da = New SqlDataAdapter(cmd)
        'da.Fill(dt)

        ''Dim cr As New CrystalReport2
        ''cr.Database.Tables["Order_List"] setdataset
        'Dim objRpt As New CrystalReport2
        'objRpt.SetDataSource(ds.Tables)
        'CrystalReportViewer1.ReportSource = objRpt
        'CrystalReportViewer1.Refresh()
        'conn.Close()
    End Sub

    Private Sub Form22_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        'TextBox1.Text = "21/8/2"
    End Sub
End Class